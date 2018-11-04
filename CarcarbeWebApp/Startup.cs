using Carcarbe.Shared.Logging;
using Carcarbe.Shared.Messages;
using CarcarbeWebApp.Handlers;
using CarcarbeWebApp.HostedService;
using CarcarbeWebApp.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;

namespace CarcarbeWebApp
{
    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;
        //const Rebus.Logging.LogLevel MinimumLogLevel = Rebus.Logging.LogLevel.Warn; 
        const string ConnectionString = "amqp://localhost";
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
            _loggerFactory.AddDebug(); // logs to the debug output window in VS.
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register handlers 
            services.AutoRegisterHandlersFromAssemblyOf<Handler1>();
           
            // Configure and register Rebus
            //services.AddRebus(configure => configure
            //        .Logging(l => l.Use(new MSLoggerFactoryAdapter(_loggerFactory)))
            //        .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672/", "messages-queue"))
            //        .Start());
            services.AddRebus(configure => configure
        .Logging(l => l.Use(new MSLoggerFactoryAdapter(_loggerFactory)))
        .Transport(t => t.UseRabbitMq("amqp://pklfurgc:4YJosxjltR4AntkkvVignFH-TKW16c9k@raven.rmq.cloudamqp.com/pklfurgc", "messages-queue"))
        .Routing(r => r.TypeBased().MapAssemblyOf<MeterMessage>("messages-queue")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "redis-19221.c92.us-east-1-3.ec2.cloud.redislabs.com:19221,password=6MFI4IqLgNP9TyCcv6BjFKNQMXoHtIZc";
                option.InstanceName = "carcarbe-redis-db";
             
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            services.AddHostedService<TimedHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            
            app.UseRebus();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

        }


    }
}
