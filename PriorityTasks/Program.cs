using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using PriorityTasks.Data;
using Microsoft.Extensions.DependencyInjection;

namespace PriorityTasks
{
    public class Program
    {
        /// <summary>
        /// Main entry point for the web application.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            CreateDatabaseIfNotExists(host);

            host.Run();
        }

        /// <summary>
        /// Create the database and model contexts if they do not already exist.
        /// </summary>
        /// <param name="host">Applications configuration, database, and logging resources.</param>
        private static void CreateDatabaseIfNotExists(IHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;

                // try to initialize the contexts
                try
                {
                    TaskContext context = services.GetRequiredService<TaskContext>();

                    DatabaseInitializer.Initialize(context);
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    logger.LogError(e, "An error occurred creating the database.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
