using PokemonReviewApp.Dto.AuthenticationDTOs;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(OwnerLoginDTO user);
    }
}
