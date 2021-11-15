using System;
using System.Collections.Generic;
using System.Text;
using WebflowCore.Sample01.Models;
using WebflowCore.Sample01.Steps;
using WorkflowCore.Interface;

namespace WebflowCore.Sample01
{
    //Our workflow definition with strongly typed internal data and mapped inputs & outputs
    public class PassingDataWorkflow : IWorkflow<MyDataClass>
    {
        public string Id => "PassingDataWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<MyDataClass> builder)
        {
            builder
                .StartWith<AddNumbers>()
                    .Input(step => step.Input1, data => data.Value1)
                    .Input(step => step.Input2, data => data.Value2)
                    .Output(data => data.Answer, step => step.Output)
                .Then<CustomMessage>()
                    .Input(step => step.Message, data => "The answer is " + data.Answer.ToString());
        }
    }
}
