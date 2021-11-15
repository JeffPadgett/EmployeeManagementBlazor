﻿using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WebflowCore.Sample01.Steps
{
    //our workflow step with inputs and outputs
    public class AddNumbers : StepBody
    {
        public int Input1 { get; set; }

        public int Input2 { get; set; }

        public int Output { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Output = (Input1 + Input2);
            return ExecutionResult.Next();
        }
    }
}
