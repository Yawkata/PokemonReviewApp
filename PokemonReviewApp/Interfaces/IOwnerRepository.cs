using PokemonReviewApp.Dto;
using PokemonReviewApp.Dto.AuthenticationDTOs;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IOwnerRepository
    {
        OwnerResponse GetOwners();

        public List<Owner> GetOwnersForLogIn();

        OwnerResponse GetOwner(int id);

        Owner GetOwner(string ownerNickname);

        PokemonResponse GetPokemonsByOwner(int id);

        OwnerResponse CreateOwner(int countryId, OwnerRegisterDTO owner);

        OwnerResponse UpdateOwner(int ownerId, int countryId, OwnerRequest owner);

        OwnerResponse DeleteOwner(int ownerId);

        bool OwnerExists(int id);

        bool OwnerExists(string nikname);

        bool Save();

    }
}
 