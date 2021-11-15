using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WebflowCore.Sample01.Models
{
    //This class is just used to define the internal data of our workflow
    public class MyDataClass
    {
        public int Value1 { get; set; }
        public int Value2 { get; set; }
        public int Answer { get; set; }
    }
}
