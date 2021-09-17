using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CityFinder.June.Models;
using Microsoft.Extensions.Logging;

namespace CityFinder.June.Services
{
    public class ZipCodeService : IZipCodeService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ZipCodeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ZipCodeApiResponse> FindCityByZipCodeAsync(string zipCode, string country)
        {
            var client = _httpClientFactory.CreateClient("ZipCodeBase");

            var sb = new StringBuilder()
            .Append(client.BaseAddress)
            .Append("codes=").Append(zipCode)
            .Append("&country=").Append(country);

            var response = await client.GetAsync(sb.ToString());
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ZipCodeApiResponse>();
        }
    }
}
