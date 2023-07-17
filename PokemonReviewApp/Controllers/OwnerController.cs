using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using Microsoft.AspNetCore.Authorization;
using PokemonReviewApp.Dto.AuthenticationDTOs;

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
            var owners = mapper.Map<List<OwnerDto>>(ownerRepository.GetOwners());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(owners);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int ownerId)
        {
            if (!ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }

            var owner = mapper.Map<OwnerDto>(ownerRepository.GetOwner(ownerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(owner);
        }

        [HttpGet("{ownerId}/pokemons")]
        [ProducesResponseType(200, Type = typeof(List<Pokemon>))]
        [ProducesResponseType(400)] 
        public IActionResult GetPokemonsByOwner(int ownerId)
        {
            if (!ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }

            var pokemons = mapper.Map<List<PokemonDto>>(ownerRepository.GetPokemonsByOwner(ownerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] OwnerRegisterDTO ownerCreate)
        {
            if (ownerCreate == null)
            {
                return BadRequest(ModelState);
            }

            var owner = ownerRepository.GetOwners()
                .Where(o => o.Nickname.ToUpper() == ownerCreate.Nickname.ToUpper())
                .FirstOrDefault();

            if (owner != null)
            {
                ModelState.AddModelError("", "Owner Already Exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ownerMap = mapper.Map<Owner>(ownerCreate);
            ownerMap.Country = countryRepository.GetCountry(countryId);

            if (!ownerRepository.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Error while saving owner");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created new owner!");
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
        public IActionResult UpdateOwner(int ownerId, [FromBody] OwnerDto ownerUpdate)
        {
            if (ownerUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (ownerId != ownerUpdate.Id)
            {
                return BadRequest(ModelState);
            }

            if (!ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ownerMap = mapper.Map<Owner>(ownerUpdate);

            if (!ownerRepository.UpdateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Error while updating owner!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated owner!");
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{ownerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteOwner(int ownerId)
        {
            if (!ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }

            var ownerToDelete = ownerRepository.GetOwner(ownerId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ownerRepository.DeleteOwner(ownerToDelete))
            {
                ModelState.AddModelError("", "Error while deleting owner");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted owner!");
        }
    }
}
