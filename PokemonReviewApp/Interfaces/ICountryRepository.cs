using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        List<Country> GetCountries();

        Country GetCountry(int id);

        Country GetCountryByOwner(int id);
        
        List<Owner> GetOwnersFromCountry(int id);

        bool CreateCountry(Country country);

        bool Save();

        bool CountryExists(int id);

    }
}
 