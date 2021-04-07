using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

using static System.Console;

namespace MsBlazorServerPlayGround
{
    public class Program
    {
        private static IDictionary<string, string> inMemoryConfigurationData;

        public static void Main(string[] args)
        {
            inMemoryConfigurationData = new Dictionary<string, string>
            {
                 { "InMemorySampleA", "A" }
                ,{ "InMemorySampleB", "B" }
                ,{ "InMemorySampleC", "C" }
            };

            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((host, configuration) =>
                {

                    var memoryConfiguration = new MemoryConfigurationSource { InitialData = inMemoryConfigurationData };
                    configuration.Add(memoryConfiguration);

                    //Added before AddUserSecrets or AzureAppConfiguration to let user secrets override environment variables
                    configuration.AddEnvironmentVariables();

                    var environment = host.HostingEnvironment;
                    if (environment.IsEnvironment("Local") || environment.IsDevelopment())
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
