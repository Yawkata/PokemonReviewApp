using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Helper;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using AutoMapper;
using PokemonReviewApp.Dto.AuthenticationDTOs;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OwnerRepository(DataContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public OwnerResponse GetOwners()
        {
            OwnerResponse response = new();

            var owners = _context.Owners.OrderBy(c => c.Id).ToList();

            response.Owners = _mapper.Map<List<OwnerDto>>(owners);
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public List<Owner> GetOwnersForLogIn()
        {
            return _context.Owners.ToList();
        }

        public OwnerResponse GetOwner(int ownerId)
        {
            OwnerResponse response = new();

            if (!OwnerExists(ownerId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Owner");
                return response;
            }

            var owner = _context.Owners.FirstOrDefault(c => c.Id == ownerId);
            response.Owners.Add(_mapper.Map<OwnerDto>(owner));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public PokemonResponse GetPokemonsByOwner(int ownerId)
        {
            PokemonResponse response = new();

            if (!OwnerExists(ownerId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Owner");
                return response;
            }

            var pokemons = _context.PokemonOwners.Where(o => o.OwnerId == ownerId).Select(p => p.Pokemon).ToList();

            response.Pokemons = _mapper.Map<List<PokemonDto>>(pokemons);
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public OwnerResponse CreateOwner(int countryId, OwnerRegisterDTO ownerCreate)
        {
            OwnerResponse response = new();

            if (!_context.Countries.Any(c => c.Id == countryId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Country");
                return response;
            }

            if (OwnerExists(ownerCreate.Nickname))
            {
                response.ServerMessage = String.Format(GlobalConstants.AlreadyExists, "Owner");
                return response;
            }

            var ownerMap = _mapper.Map<Owner>(ownerCreate);
            ownerMap.Role = "RegularUser";
            ownerMap.Country = _context.Countries.FirstOrDefault(c => c.Id == countryId);

            _context.Add(ownerMap);
            Save();

            var owner = _context.Owners.FirstOrDefault(x => x.Nickname == ownerCreate.Nickname);
            response.Owners.Add(_mapper.Map<OwnerDto>(owner));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public OwnerResponse UpdateOwner(int ownerId, int countryId, OwnerRequest ownerUpdate)
        {
            OwnerResponse response = new();

            if (!OwnerExists(ownerId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Owner");
                return response;
            }

            var newCountry = _context.Countries.FirstOrDefault(x => x.Id == countryId);
            if (newCountry == null)
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Country");
                return response;
            }
            var owner = _context.Owners.FirstOrDefault(o => o.Id == ownerId);

            var updatedOwner = _mapper.Map<Owner>(ownerUpdate);
            updatedOwner.Password = owner.Password;
            updatedOwner.Id = ownerId;
            updatedOwner.Country = newCountry;
            updatedOwner.CountryId = countryId;

            _context.Update(updatedOwner);
            Save();

            response.Owners.Add(_mapper.Map<OwnerDto>(updatedOwner));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public OwnerResponse DeleteOwner(int ownerId) 
        {
            OwnerResponse response = new();

            if (!OwnerExists(ownerId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Owner");
                return response;
            }

            var ownerToDelete = _context.Owners.FirstOrDefault(c => c.Id == ownerId);

            _context.Remove(ownerToDelete);
            Save();

            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public Owner GetOwner(string ownerNickname)
        {
            return _context.Owners.Where(o => o.Nickname == ownerNickname).FirstOrDefault();
        }


        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }

        public bool OwnerExists(string nikname)
        {
            return _context.Owners.Any(o => o.Nickname.ToUpper() == nikname.ToUpper());
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
