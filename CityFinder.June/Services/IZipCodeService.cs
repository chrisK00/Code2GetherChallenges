using System.Threading.Tasks;
using CityFinder.June.Models;

namespace CityFinder.June.Services
{
    public interface IZipCodeService
    {
        Task<ZipCodeApiResponse> FindCityByZipCodeAsync(string zipCode, string country);
    }
}
