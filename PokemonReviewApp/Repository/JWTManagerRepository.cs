using Microsoft.IdentityModel.Tokens;
using PokemonReviewApp.Dto.AuthenticationDTOs;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PokemonReviewApp.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration iconfiguration;
        private readonly IOwnerRepository ownerRepository;

        public JWTManagerRepository(IConfiguration iconfiguration, IOwnerRepository ownerRepository)
        {
            this.iconfiguration = iconfiguration;
            this.ownerRepository = ownerRepository;
        }
        public Tokens Authenticate(OwnerLoginDTO userCredentials)
        {
            var users = ownerRepository.GetOwnersForLogIn();
            

            if (!users.Any(x => x.Nickname == userCredentials.Nickname && x.Password == userCredentials.Password))
            {
                return null;
            }

            var user = ownerRepository.GetOwner(userCredentials.Nickname);

            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var securityKey = new SymmetricSecurityKey(tokenKey);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Nickname),
                    new Claim(ClaimTypes.Role, user.Role),
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return new Tokens { Token = jwtToken };
        }
    }
}