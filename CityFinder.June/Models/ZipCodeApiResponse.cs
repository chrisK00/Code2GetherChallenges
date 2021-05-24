using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CityFinder.June.Models
{
    public class ZipCodeApiResponse
    {
        public Dictionary<string, List<Location>> Results { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("City: ");
            sb.Append(City);
            sb.Append("\t\tCountry code: ");
            sb.Append(CountryCode);
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
