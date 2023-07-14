using Microsoft.IdentityModel.Tokens;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Dto.RequestDTOs;
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
        private readonly DataContext context;

        public JWTManagerRepository(IConfiguration iconfiguration, DataContext context)
        {
            this.iconfiguration = iconfiguration;
            this.context = context;
        }
        public Tokens Authenticate(OwnerAuthenticationRequestDTO user)
        {
            var users = context.Owners.ToList();
            if (!users.Any(x => x.Nickname == user.Nickname && x.Password == user.Password))
            {
                return null;
            }

            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var securityKey = new SymmetricSecurityKey(tokenKey);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Nickname),
                    // new Claim(ClaimTypes.Role, users.Role)}),
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