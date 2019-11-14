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
                        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                        {
                            options.ListenLocalhost(5001, listenOptions =>
                            {
                                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                                listenOptions.UseHttps("carcarbe.pfx", Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Password"));
                            });
                        }
                        else
                        {
                            options.Listen(IPAddress.Any, 5001, listenOptions =>
                            {
                                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                                listenOptions.UseHttps("carcarbe.pfx", Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Password"));
                            });
                        }
                    })
                .UseStartup<Startup>();
    }
}
