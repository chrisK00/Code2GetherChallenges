using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CityFinder.June.Models;
using Microsoft.Extensions.Options;
using static System.Console;

namespace CityFinder.June.Services
{
    public class ZipCodeService : IZipCodeService
    {
        private readonly HttpClient _client = new();

        public ZipCodeService(IOptions<ZipCodeApiOptions> options)
        {
            _client.BaseAddress = new Uri(options.Value.Uri);
            _client.DefaultRequestHeaders.Add("x-rapidapi-key", options.Value.RapidApiKey);
            _client.DefaultRequestHeaders.Add("x-rapidapi-host", options.Value.RapidApiHost);
        }

        public async Task<ZipCodeApiResponse> FindCityByZipCodeAsync(string zipCode, string country)
        {
            var sb = new StringBuilder();
            sb.Append(_client.BaseAddress);
            sb.Append("codes=");
            sb.Append(zipCode);
            sb.Append("&country=");
            sb.Append(country);
            HttpResponseMessage response = null;

            try
            {
                response = await _client.GetAsync(sb.ToString());
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                WriteLine("Bad request", ex.Message);
            }
            catch (Exception ex)
            {
                WriteLine("Probably Pobiegas fault", ex.Message);
            }

            return await response.Content.ReadFromJsonAsync<ZipCodeApiResponse>();
        }
    }
}
