using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(OwnerDto user);
    }
}
