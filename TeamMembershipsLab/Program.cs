using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeamMembershipsLab.Data;
using TeamMembershipsLab.Models;

namespace TeamMembershipsLab
{
    public class Program
    {
        /// <summary>
        /// Programs main method.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            //Initialize secrets to use in DBInitializer and Program Startup.
            var configuration = host.Services.GetService<IConfiguration>();
            var secrets = configuration.GetSection("Secrets").Get<AppSecrets>();
            DbInitializer.AppSecret = secrets;
            using (var scope = host.Services.CreateScope())
            {
                DbInitializer.SeedUsersAndRoles(scope.ServiceProvider).Wait();
                host.Run();
            }

        }

        /// <summary>
        /// Initalizes Host builder for app.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
