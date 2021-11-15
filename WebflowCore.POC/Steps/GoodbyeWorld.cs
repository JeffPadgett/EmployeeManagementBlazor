using Microsoft.Extensions.Logging;
using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WebflowCore.Sample01.Steps
{
    public class GoodbyeWorld : StepBody
    {

        private readonly ILogger _logger;

        public GoodbyeWorld(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GoodbyeWorld>();
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Goodbye world");
            _logger.LogInformation("This is a log from the goodbye world log body.");
            return ExecutionResult.Next();
        }
    }
}
