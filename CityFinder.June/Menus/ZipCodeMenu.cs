using System;
using System.Threading.Tasks;
using CityFinder.June.Helpers;
using CityFinder.June.Models;
using CityFinder.June.Services;
using static System.Console;

namespace CityFinder.June.Menus
{
    public class ZipCodeMenu
    {
        private readonly IZipCodeService _zipCodeService;

        public ZipCodeMenu(IZipCodeService zipCodeService)
        {
            _zipCodeService = zipCodeService;
        }

        public async Task RunAsync()
        {
            char exit = ' ';
            while (exit != 'n')
            {
                Write("Enter a zipcode: ");
                var zipCode = ReadLine();
                var countryCode = InputHelpers.GetCountryCodeInput();
                ZipCodeApiResponse response = null;

                try
                {
                    response = await _zipCodeService.FindCityByZipCodeAsync(zipCode, countryCode);

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
                    WriteLine("An error occured: ", ex.Message);
                }

                exit = InputHelpers.GetCharInput(new char[] { 'y', 'n' }, "Continue? y/n: ");
            }
        }
    }
}
