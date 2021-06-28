using System;
using System.Globalization;
using System.Linq;
using static System.Console;
namespace CityFinder.June.Helpers
{
    public static class InputHelpers
    {
        public static string GetCountryCodeInput()
        {
            while (true)
            {
                Write("Enter a 2 letter country code: ");
                var input = ReadLine();

                if (input.Length > 2 || string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                try
                {
                    var regionCode = new RegionInfo(input);
                    return regionCode.TwoLetterISORegionName;
                }
                catch (ArgumentException)
                {
                    WriteLine("Invalid country code, please try again");
                }
            }
        }

        public static char GetCharInput(char[] charsToMatch, string message = "Only enter one letter")
        {
            while (true)
            {
                Write(message);
                if (char.TryParse(ReadLine().Trim().ToLower(), out char input) && charsToMatch.Contains(input))
                {
                    return input;
                }
            }
        }
    }
}
