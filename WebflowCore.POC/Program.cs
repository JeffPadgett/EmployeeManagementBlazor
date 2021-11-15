using Microsoft.Extensions.DependencyInjection;
using System;
using WebflowCore.Sample01.Models;
using WebflowCore.Sample01.Services;
using WebflowCore.Sample01.Steps;
using WorkflowCore.Interface;

namespace WebflowCore.Sample01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider serviceProvider = ConfigureServices();

            //start the workflow host
            var host = serviceProvider.GetService<IWorkflowHost>();
            host.RegisterWorkflow<PassingDataWorkflow, MyDataClass>();
            host.Start();

            var initialData = new MyDataClass
            {
                Value1 = 5,
                Value2 = 10
            };

            host.StartWorkflow("PassingDataWorkflow", version: 1, initialData);
            
            Console.ReadLine();
            host.Stop();
        }

        private static IServiceProvider ConfigureServices()
        {
            //setup dependency injection
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow();

            services.AddTransient<DoSomething>();
            services.AddTransient<IMyService, MyService>();
            services.AddTransient<GoodbyeWorld>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
