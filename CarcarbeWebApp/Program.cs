using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;
using System.Net;

namespace CarcarbeWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>

            WebHost.CreateDefaultBuilder(args)
                    .ConfigureKestrel(options =>
                    {
                        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                        var isDevelopment = environment == EnvironmentName.Development;
                        if (isDevelopment)
                        {
                            options.ListenLocalhost(Convert.ToInt32( Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_PORT")), listenOptions =>
                            {
                                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                                listenOptions.UseHttps(Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path"),
                                    Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Password"));
                            });
                        }
                        else
                        {
                            options.Listen(IPAddress.Loopback, Convert.ToInt32(Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_PORT")), listenOptions =>
                            {
                                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                                listenOptions.UseHttps(Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path"),
                                    Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Password"));
                            });
                        }

                    })
                .UseStartup<Startup>();


    }
}
