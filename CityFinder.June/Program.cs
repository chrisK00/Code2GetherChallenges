﻿using System;
using System.Threading.Tasks;
using CityFinder.June.Menus;
using CityFinder.June.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;

namespace CityFinder.June
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await host.Services.GetRequiredService<ZipCodeMenu>().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            //auto adds json file appsettings
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddSingleton<ZipCodeMenu>();
                    services.AddTransient<IZipCodeService, ZipCodeService>();
                    //  services.Configure<ZipCodeApiOptions>(hostingContext.Configuration.GetSection("ZipCodeApiOptions"));

                    services.AddHttpClient("ZipCodeBase", client =>
                    {
                        client.BaseAddress = new Uri(hostingContext.Configuration["ZipCodeApiOptions:Uri"]);
                        client.DefaultRequestHeaders.Add("x-rapidapi-key", hostingContext.Configuration["ZipCodeApiOptions:RapidApiKey"]);
                        client.DefaultRequestHeaders.Add("x-rapidapi-host", hostingContext.Configuration["ZipCodeApiOptions:RapidApiHost"]);
                    }).AddTransientHttpErrorPolicy(policy => policy.RetryAsync(2));
                });
        }
    }
}
