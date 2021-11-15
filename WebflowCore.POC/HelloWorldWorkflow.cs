using System;
using System.Collections.Generic;
using System.Text;
using WebflowCore.Sample01.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WebflowCore.Sample01
{
    public class HelloWorldWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .UseDefaultErrorBehavior(WorkflowErrorHandling.Suspend)
                .StartWith<HelloWorld>()
                .Then<GoodbyeWorld>();
        }

        public string Id => "HelloWorldWorkflow";

        public int Version => 1;

    }
}
