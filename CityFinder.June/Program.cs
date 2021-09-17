using System;
using System.IO;
using System.Threading.Tasks;
using CityFinder.June.Menus;
using CityFinder.June.Models;
using CityFinder.June.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

namespace CityFinder.June
{
    public class Program
    {
        static async Task Main()
        {
            var serviceProvider = ConfigureServices().BuildServiceProvider();
            await serviceProvider.GetRequiredService<ZipCodeMenu>().RunAsync();
        }

        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            var config = ConfigureBuild();

            services.AddSingleton<ZipCodeMenu>();
            services.AddTransient<IZipCodeService, ZipCodeService>();
            services.AddLogging(opt =>
            {
                opt.AddConsole();
            });

            services.AddHttpClient("ZipCodeBase", client =>
            {
                var options = config.GetSection(nameof(ZipCodeApiOptions)).Get<ZipCodeApiOptions>();

                client.BaseAddress = new Uri(options.Uri);
                client.DefaultRequestHeaders.Add("x-rapidapi-key", options.RapidApiKey);
                client.DefaultRequestHeaders.Add("x-rapidapi-host", options.RapidApiHost);

            }).AddTransientHttpErrorPolicy(policy => policy.RetryAsync(2));

            return services;
        }

        private static IConfigurationRoot ConfigureBuild()
        {
            return new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                  .Build();
        }
    }
}
