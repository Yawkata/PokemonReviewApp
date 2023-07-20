using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using Microsoft.AspNetCore.Authorization;
using PokemonReviewApp.Dto.AuthenticationDTOs;
using PokemonReviewApp.Helper;
using PokemonReviewApp.Migrations;

namespace PokemonReviewApp.Controllers
{
    [Authorize(Roles = "Administrator, RegularUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository ownerRepository;
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;
        private readonly IJWTManagerRepository jWTManager;

        public OwnerController(IOwnerRepository ownerRepository, 
            ICountryRepository countryRepository, IMapper mapper, IJWTManagerRepository jWTManager)
        {
            this.ownerRepository = ownerRepository;
            this.countryRepository = countryRepository;
            this.mapper = mapper;
            this.jWTManager = jWTManager;
        }
        

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwners()
        {
            var response = ownerRepository.GetOwners();

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int ownerId)
        {
            var response = ownerRepository.GetOwner(ownerId);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{ownerId}/pokemons")]
        [ProducesResponseType(200, Type = typeof(List<Pokemon>))]
        [ProducesResponseType(400)] 
        public IActionResult GetPokemonsByOwner(int ownerId)
        {
            var response = ownerRepository.GetPokemonsByOwner(ownerId);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] OwnerRegisterDTO ownerCreate)
        {
            var response = ownerRepository.CreateOwner(countryId, ownerCreate);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(String.Format(GlobalConstants.SuccessfulCreate, "onwer"));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Authenticate(OwnerLoginDTO userdata)
        {
            var token = jWTManager.Authenticate(userdata);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{ownerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateOwner(int ownerId, [FromQuery] int countryId, [FromBody] OwnerRequest ownerUpdate)
        {
            var response = ownerRepository.UpdateOwner(ownerId, countryId, ownerUpdate);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(String.Format(GlobalConstants.SuccessfulUpdate, "onwer"));
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{ownerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteOwner(int ownerId)
        {
            var response = ownerRepository.DeleteOwner(ownerId);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(String.Format(GlobalConstants.SuccessfulDelete, "onwer"));
        }
    }
}
