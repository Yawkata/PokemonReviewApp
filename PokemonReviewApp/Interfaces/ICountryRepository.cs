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

        bool CountryExists(int id);

        bool UpdateCountry(Country country);

        bool DeleteCountry(Country country);

        bool Save();
    }
}
 