using System;
using System.Collections.Generic;
using System.Text;
using WebflowCore.Sample01.Services;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WebflowCore.Sample01.Steps
{
    public class DoSomething : StepBody
    {
        private readonly IMyService  _myService;

        public DoSomething(IMyService myService)
        {
            _myService = myService;
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            _myService.DoTheThings();
            return ExecutionResult.Next();
        }
    }
}
