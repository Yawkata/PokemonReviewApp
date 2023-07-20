using AutoMapper;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Dto.RequestDTOs;
using PokemonReviewApp.Helper;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CountryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CountryResponse GetCountries()
        {
            CountryResponse response = new();

            var countries = _context.Countries.OrderBy(c => c.Id).ToList();


            response.Countries = _mapper.Map<List<CountryDto>>(countries);
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public CountryResponse GetCountry(int countryId)
        {
            CountryResponse response = new();

            if (!CountryExists(countryId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Country");
                return response;
            }

            var country = _context.Countries.FirstOrDefault(c => c.Id == countryId);

            response.Countries.Add(_mapper.Map<CountryDto>(country));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public CountryResponse GetCountryByOwner(int ownerId)
        {
            CountryResponse response = new();

            if(!_context.Owners.Any(o => o.Id == ownerId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Owner");
                return response;
            }

            var country = _context.Owners.Where(o => o.Id == ownerId).Select(o => o.Country).FirstOrDefault();

            response.Countries.Add(_mapper.Map<CountryDto>(country));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public CountryResponse CreateCountry(CountryRequest countryCreate)
        {
            CountryResponse response = new();

            if (CountryExists(countryCreate.Name))
            {
                response.ServerMessage = String.Format(GlobalConstants.AlreadyExists, "Country");
                return response;
            }

            var countryMap = _mapper.Map<Country>(countryCreate);

            _context.Add(countryMap);
            Save();

            var country = _context.Countries.FirstOrDefault(x => x.Name == countryCreate.Name);

            response.Countries.Add(_mapper.Map<CountryDto>(country));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public CountryResponse UpdateCountry(int countryId, CountryRequest countryUpdate)
        {
            CountryResponse response = new();

            if (!CountryExists(countryId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Country");
                return response;
            }

            if (CountryExists(countryUpdate.Name))
            {
                response.ServerMessage = String.Format(GlobalConstants.AlreadyExists, "Country");
                return response;
            }

            var country = _mapper.Map<Country>(countryUpdate);
            country.Id = countryId;

            _context.Update(country);
            Save();
            
            response.Countries.Add(_mapper.Map<CountryDto>(country));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public CountryResponse DeleteCountry(int countryId)
        {
            CountryResponse response = new();

            if (!CountryExists(countryId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Country");
                return response;
            }

            var countryToDelete = _context.Countries.FirstOrDefault(c => c.Id == countryId);

            _context.Remove(countryToDelete);
            Save();

            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public bool CountryExists(int id)
        {
            return _context.Countries.Any(c => c.Id == id);
        }

        public bool CountryExists(string name)
        {
            return _context.Countries.Any(c => c.Name.ToUpper() == name.ToUpper());
        }

        public bool Save()
        {
            return _context.SaveChanges() != 0;
        }
    }
}
