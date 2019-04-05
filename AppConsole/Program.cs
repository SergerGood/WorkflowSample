using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;

namespace AppConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow();

            var serviceProvider = services.BuildServiceProvider();

            var host = serviceProvider.GetService<IWorkflowHost>();
            host.RegisterWorkflow<AppWorkflow>();
            host.Start();

            await host.StartWorkflow("HelloWord", 1, null);

            Console.ReadLine();
            host.Stop();
        }
    }
}
