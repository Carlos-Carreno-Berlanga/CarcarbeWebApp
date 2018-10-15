using CarcarbeWebApp.Handlers;
using CarcarbeWebApp.Logging;
using CarcarbeWebApp.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.Transport.InMem;
using System.Linq;
using System.Threading.Tasks;

namespace CarcarbeWebApp
{
    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;
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
            services.AddRebus(configure => configure
                    .Logging(l => l.Use(new MSLoggerFactoryAdapter(_loggerFactory)))
                    .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "Messages"))
                    .Routing(r => r.TypeBased().MapAssemblyOf<Message1>("Messages")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
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

            // or optionally act on the bus:
            // .UseRebus(async bus => await bus.Subscribe<Message1>())
            //.Run(async (context) =>
            //{
            //    var bus = app.ApplicationServices.GetRequiredService<IBus>();

            //    await Task.WhenAll(
            //        Enumerable.Range(0, 10)
            //            .Select(i => new Message1())
            //            .Select(message => bus.Send(message)));

            //    await context.Response.WriteAsync("Rebus sent another 10 messages!");
            //});
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
