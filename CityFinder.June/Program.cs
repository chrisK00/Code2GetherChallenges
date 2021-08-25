using System;
using System.Threading.Tasks;
using CityFinder.June.Menus;
using CityFinder.June.Models;
using CityFinder.June.Services;
using Microsoft.Extensions.Configuration;
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

                    services.AddHttpClient("ZipCodeBase", client =>
                    {

                        var options = hostingContext.Configuration.GetSection(nameof(ZipCodeApiOptions)).Get<ZipCodeApiOptions>();

                        client.BaseAddress = new Uri(options.Uri);
                        client.DefaultRequestHeaders.Add("x-rapidapi-key", options.RapidApiKey);
                        client.DefaultRequestHeaders.Add("x-rapidapi-host", options.RapidApiHost);
=======
                        //  client.BaseAddress = new Uri(hostingContext.Configuration["ZipCodeApiOptions:Uri"]);
                 //       client.BaseAddress = new Uri(hostingContext.Configuration["ZipCodeBaseApiUri"]);
                   //     client.DefaultRequestHeaders.Add("x-rapidapi-key", hostingContext.Configuration["RapidApiKey"]);
                     //   client.DefaultRequestHeaders.Add("x-rapidapi-host", hostingContext.Configuration["RapidApiHost"]);

                    }).AddTransientHttpErrorPolicy(policy => policy.RetryAsync(2));
                });
        }
    }
}
