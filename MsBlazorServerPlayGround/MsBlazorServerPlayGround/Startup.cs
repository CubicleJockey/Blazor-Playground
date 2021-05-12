using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MsBlazorServerPlayGround.Data;
using MsBlazorServerPlayGround.Interfaces;
using MsBlazorServerPlayGround.Objects;
using MsBlazorServerPlayGround.SignalR.Hubs;

namespace MsBlazorServerPlayGround
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages()
                    .AddRazorRuntimeCompilation();
            services.AddServerSideBlazor()
                    .AddHubOptions(options => options.MaximumReceiveMessageSize = 64 * 1024); //Increases message max for interop C# called from JavaScript


            services.AddSingleton<IWeatherForecastService, WeatherForecastService>();

            services.AddTransient<JsInAClass>();

            services.AddScoped<StateManager>();

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {"application/octet-stream"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application.UseResponseCompression();

            if (environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }
            else
            {
                application.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                application.UseHsts();
            }

            application.UseHttpsRedirection();
            application.UseStaticFiles();

            application.UseRouting();

            application.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapHub<BotChatHub>("/botChatHub");
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
