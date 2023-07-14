using PokemonReviewApp.Dto;
using PokemonReviewApp.Dto.RequestDTOs;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(OwnerAuthenticationRequestDTO user);
    }
}
