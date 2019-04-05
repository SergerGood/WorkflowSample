using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace AppConsole
{
    public class AppWorkflow : IWorkflow
    {
        public string Id => "HelloWord";
        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder.StartWith<InitStep>()
                .Then(context =>
                {
                    Console.WriteLine("Stop");
                    return ExecutionResult.Next();
                });
        }
    }
}