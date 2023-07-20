using PokemonReviewApp.Dto;
using PokemonReviewApp.Dto.RequestDTOs;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        CountryResponse GetCountries();

        CountryResponse GetCountry(int countryId);

        CountryResponse GetCountryByOwner(int id);

        CountryResponse CreateCountry(CountryRequest countryCreate);

        CountryResponse UpdateCountry(int countryId, CountryRequest country);

        CountryResponse DeleteCountry(int countryId);

        bool CountryExists(int id);

        bool CountryExists(string name);

        bool Save();
    }
}
 