using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

using static System.Console;

namespace MsBlazorServerPlayGround
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((host, configuration) =>
                {
                    //Added before AddUserSecrets or AzureAppConfiguration to let user secrets override environment variables
                    configuration.AddEnvironmentVariables();

                    var environment = host.HostingEnvironment;
                    if (environment.IsEnvironment("Local"))
                    {
                        configuration.AddUserSecrets<Program>();
                    }
                    else //Develop, QA, & Production Azure App Configurations
                    {
                        const string AZURE_APP_CONFIGURATION_ENVIRONMENT_VARIABLE = "ASPNETCORE_CONFIGURATION";
                        var appConfigurationEndpoint = Environment.GetEnvironmentVariable(AZURE_APP_CONFIGURATION_ENVIRONMENT_VARIABLE);
                        if (!string.IsNullOrWhiteSpace(appConfigurationEndpoint))
                        {
                            configuration.AddAzureAppConfiguration(options => options.Connect(appConfigurationEndpoint));
                        }
                        else
                        {
                            Error.WriteLine($"Failed to load Azure App Configurations for environment [{environment.EnvironmentName}]. Environment Variable {AZURE_APP_CONFIGURATION_ENVIRONMENT_VARIABLE} is empty.");
                        }
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
