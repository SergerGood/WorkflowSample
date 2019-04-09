using System;
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

            await host.StartWorkflow("HelloWord", 1, new Data {Value = 1});
            await host.StartWorkflow("HelloWord", 1, new Data { Value = 2 });

            Console.ReadLine();
            host.Stop();
        }
    }
}