using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace AppConsole
{
    public class AppWorkflow : IWorkflow<Data>
    {
        public string Id => Consts.AppWorkflowId;
        public int Version => 1;

        public void Build(IWorkflowBuilder<Data> builder)
        {
            builder
                .StartWith<InitStep>()
                .Input(initStep => initStep.Input1, data => data.Value)
                .Input(initStep => initStep.Input2, data => data.Value)
                .Output(data => data.Result, step => step.Output)
                .WaitFor(Consts.WaitingEvent, (data, context) => "0", data => new DateTime(1500, 1, 1))
                .Then(context =>
                {
                    Console.WriteLine("Stop");
                    return ExecutionResult.Next();
                })
                .When(data => data.Result == 2)
                .Do(x => Console.WriteLine("2"))
                .When(data => data.Result == 4)
                .Do(x => Console.WriteLine("4"))
                .Output((body, data) => Console.WriteLine($"Output: {data.Result}"));
        }
    }
}