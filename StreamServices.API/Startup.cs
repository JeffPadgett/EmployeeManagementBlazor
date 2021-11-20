using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using StreamServices;
using StreamServices.API;

[assembly: FunctionsStartup(typeof(Startup))]
namespace StreamServices.API
{
    public sealed class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddAutoMapper(typeof(Startup).Assembly);
        }
    }

}