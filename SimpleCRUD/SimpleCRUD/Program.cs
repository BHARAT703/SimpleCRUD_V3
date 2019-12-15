using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleCRUD.Infrastructure.DatabaseContext;
using SimpleCRUD.Infrastructure.Seeders;

namespace SimpleCRUD
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args).Migrate();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
                   .UseContentRoot(Directory.GetCurrentDirectory()) //This method configures the root directory for the application, which is used for loading configuration files and delivering static content such as images,JavaScript, and CSS.
                   .UseIISIntegration() //This method enables integration with IIS and IIS Express.
                     .UseStartup<Startup>().Build(); //This method specifies the class that will be used to configure ASP.NET, as described in the “Understanding the Startup Class” section

        public static IWebHost Migrate(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var logger = services.GetRequiredService<ILogger<Startup>>();

                    var context = services.GetRequiredService<ApplicationContext>();
                    //context.Database.Migrate();

                    // Requires using RazorPagesMovie.Models;
                    SeedData.EnsurePopulated(context: context).Wait();
                    logger.LogError("SeedData Populated.");
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Startup>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            return host;
        }
    }
}
