using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        public readonly DataContext context;

        public CountryRepository(DataContext context)
        {
            this.context = context;
        }

        public bool CountryExists(int id)
        {
            return context.Countries.Any(c => c.Id == id);
        }

        public bool CreateCountry(Country country)
        {
            context.Add(country);
            return Save();
        }

        public List<Country> GetCountries()
        {
            return context.Countries.OrderBy(c => c.Id).ToList();
        }

        public Country GetCountry(int id)
        {
            return context.Countries.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            //return context.Owners.Include(x => x.Country).Where(o => o.Id == ownerId).FirstOrDefault().Country;
            return context.Owners.Where(o => o.Id == ownerId).Select(o => o.Country).FirstOrDefault();
        }

        public List<Owner> GetOwnersFromCountry(int countryId)
        {
            //return context.Countries.Where(c => c.Id == countryId).FirstOrDefault().Owners.ToList();
            return context.Owners.Where(o => o.Country.Id == countryId).ToList();
        }

        public bool UpdateCountry(Country country)
        {
            context.Update(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            context.Remove(country);
            return Save();
        }

        public bool Save()
        {
            return context.SaveChanges() != 0;
        }
    }
}
