using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace AppConsole
{
    public class AppWorkflow : IWorkflow<Data>
    {
        public string Id => "HelloWord";
        public int Version => 1;

        public void Build(IWorkflowBuilder<Data> builder)
        {
            builder.StartWith<InitStep>()
                .Input(initStep => initStep.Input1, data => data.Value)
                .Input(initStep => initStep.Input2, data => data.Value)
                .Output(data => data.Result, step => step.Output)
                .Then(context =>
                {
                    Console.WriteLine("Stop");
                    return ExecutionResult.Next();
                })
                .Input((body, data) => Console.WriteLine($"Output: {data.Result}"));
        }
    }
}