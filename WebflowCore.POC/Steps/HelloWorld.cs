using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WebflowCore.Sample01
{
    public class HelloWorld : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Hello world");
            return ExecutionResult.Next();
        }
    }
}
