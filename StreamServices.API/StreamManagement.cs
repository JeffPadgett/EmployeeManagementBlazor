using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StreamServices.Core;
using StreamServices.Core.Models;
using StreamServices.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StreamServices.API
{
    public class StreamManagement : BaseFunction
    {
        private readonly IMapper _mapper;
        public StreamManagement(IHttpClientFactory httpClientFactory, IMapper mapper) : base(httpClientFactory)
        {
            _mapper = mapper;
        }

        //http://localhost:7071/api/Subscribe?userName=brokenswordx
        //Function is meant to pass the userId in and subtype
        [FunctionName("Subscribe")]
        public async Task<IActionResult> Subscribe([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Table("Tokens", Connection = "AzureWebJobsStorage")] CloudTable cloudTable,
            [Table("Tokens", "Twitch", "1", Connection = "AzureWebJobsStorage")] AppAccessToken appAccessToken,
            ILogger log)
        {
            var baseTwitchEndpoint = Environment.GetEnvironmentVariable("BaseTwitchUrl");
            string user = req.Query["userName"].ToString();
            string subType = req.Query["subType"].ToString();

            if (string.IsNullOrWhiteSpace(user))
            {
                return new BadRequestObjectResult("Please pass a user name into the query string paramter. Like ?userName = coolStreamer or ?subType=channel.follow. ");
            }

            appAccessToken = await VerifyAccessToken(cloudTable, appAccessToken, log);

            log.LogInformation($"Subscribeing {user}");
            var channelToSubscribeTo = await IdentifyUser(user, appAccessToken);
            TwitchSubscriptionInitalPost subObject = new TwitchSubscriptionInitalPost(await GetChannelIdForUserName(channelToSubscribeTo, appAccessToken), subType);
            string subPayLoad = JsonConvert.SerializeObject(subObject);
            var postRequestContent = new StringContent(subPayLoad, Encoding.UTF8, "application/json");

            string responseBody;
            string namedUser = char.IsDigit(user[0]) ? await GetUserNameForChannelId(user, appAccessToken) : user;
            using (var client = GetHttpClient(baseTwitchEndpoint))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + appAccessToken.AccessToken);
                var responseMessage = await client.PostAsync("eventsub/subscriptions", postRequestContent);

                if (!responseMessage.IsSuccessStatusCode)
                {
                    responseBody = await responseMessage.Content.ReadAsStringAsync();
                    log.Log(LogLevel.Error, $"Error response body {responseBody}");
                }
                else
                {
                    log.LogInformation($"Subscribed to {namedUser}'s stream");
                    return new OkObjectResult($"Notifications will now be sent when {subType} on stream {namedUser}");
                }
            }
            return new BadRequestObjectResult(responseBody + $" When attempting to subscribe {namedUser}");
        }


        [FunctionName("GetSubscriptions")]
        public async Task<IActionResult> GetSubscriptions(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Table("Tokens", Connection = "AzureWebJobsStorage")] CloudTable cloudTable,
            [Table("Tokens", "Twitch", "1", Connection = "AzureWebJobsStorage")] AppAccessToken appAccessToken,
            ILogger log)
        {
            appAccessToken = await VerifyAccessToken(cloudTable, appAccessToken, log);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + appAccessToken.AccessToken);
                client.DefaultRequestHeaders.Add("Client-ID", Environment.GetEnvironmentVariable("ClientId"));
                var response = await client.GetAsync("https://api.twitch.tv/helix/eventsub/subscriptions");
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();

                return await GetEventSubscriptions(appAccessToken, resp);
            }
        }

        private async Task<IActionResult> GetEventSubscriptions(AppAccessToken appAccessToken, string resp)
        {
            SubscriptionList twitchList = JsonConvert.DeserializeObject<SubscriptionList>(resp);
            var formatedList = _mapper.Map<List<SubscriptionDto>>(twitchList.Subscriptions);
            formatedList.RemoveAll(x => x.Status != "enabled");
            foreach (var sub in formatedList)
            {
                sub.Name = (await GetUserNameForChannelId(sub.BroadcasterUserId, appAccessToken));
            }

            return new OkObjectResult(JsonConvert.SerializeObject(formatedList));
        }
    }
}
