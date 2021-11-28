using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo
            .File("logs/logs.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
            rollingInterval:RollingInterval.Day).WriteTo.File("logs/errorlog.txt", 
            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error).CreateLogger();
            Log.Information("startubg service");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
