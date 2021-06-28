using System;
using System.Threading.Tasks;
using CityFinder.June.Helpers;
using CityFinder.June.Services;
using Microsoft.Extensions.Logging;
using static System.Console;

namespace CityFinder.June.Menus
{
    public class ZipCodeMenu
    {
        private readonly IZipCodeService _zipCodeService;
        private readonly ILogger<ZipCodeMenu> _logger;

        public ZipCodeMenu(IZipCodeService zipCodeService, ILogger<ZipCodeMenu> logger)
        {
            _zipCodeService = zipCodeService;
            _logger = logger;
        }

        public async Task RunAsync()
        {
            char exit = ' ';
            while (exit != 'n')
            {
                Write("Enter a zipcode: ");
                var zipCode = ReadLine();
                var countryCode = InputHelpers.GetCountryCodeInput();
                try
                {
                    var response = await _zipCodeService.FindCityByZipCodeAsync(zipCode, countryCode);
                    foreach (var locations in response.Results.Values)
                    {
                        foreach (var location in locations)
                        {
                            WriteLine(location.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occured: ", ex.Message);
                }

                exit = InputHelpers.GetCharInput(new char[] { 'y', 'n' }, "Continue? y/n: ");
            }
        }
    }
}
