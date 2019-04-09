using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;

namespace AppConsole
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow();

            var serviceProvider = services.BuildServiceProvider();

            var host = serviceProvider.GetService<IWorkflowHost>();
            host.RegisterWorkflow<AppWorkflow, Data>();
            host.Start();

            await host.StartWorkflow(Consts.AppWorkflowId, 1, new Data {Value = 1});
            await host.StartWorkflow(Consts.AppWorkflowId, 1, new Data {Value = 2});

            Thread.Sleep(3_000);
            await host.PublishEvent(Consts.WaitingEvent, "0", "hello", new DateTime(1000, 1, 1));

            Thread.Sleep(3_000);
            await host.PublishEvent(Consts.WaitingEvent, "0", "hello", new DateTime(2000, 1, 1));

            Console.ReadLine();
            host.Stop();
        }
    }
}