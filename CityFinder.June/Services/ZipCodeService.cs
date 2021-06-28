using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CityFinder.June.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static System.Console;

namespace CityFinder.June.Services
{
    public class ZipCodeService : IZipCodeService
    {
        // private readonly HttpClient _client = new();
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ZipCodeService> _logger;

        public ZipCodeService(IHttpClientFactory httpClientFactory, ILogger<ZipCodeService> logger)
        {
            /* without httpclientfactory add this to ctor IOptions<ZipCodeApiOptions> options
             _client.BaseAddress = new Uri(options.Value.Uri);
             _client.DefaultRequestHeaders.Add("x-rapidapi-key", options.Value.RapidApiKey);
             _client.DefaultRequestHeaders.Add("x-rapidapi-host", options.Value.RapidApiHost);
            */

            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<ZipCodeApiResponse> FindCityByZipCodeAsync(string zipCode, string country)
        {
            var client = _httpClientFactory.CreateClient("ZipCodeBase");

            var sb = new StringBuilder();
            sb.Append(client.BaseAddress);
            sb.Append("codes=");
            sb.Append(zipCode);
            sb.Append("&country=");
            sb.Append(country);
            HttpResponseMessage response = null;

            try
            {
                // response = await _client.GetAsync(sb.ToString());
                response = await client.GetAsync(sb.ToString());
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is Exception)
            {
                _logger.LogError("Bad request", ex.Message);
                throw;
            }

            return await response.Content.ReadFromJsonAsync<ZipCodeApiResponse>();
        }
    }
}
