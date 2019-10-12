using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace SqApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
