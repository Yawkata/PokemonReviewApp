using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; // model view controller
using PokemonReviewApp.Dto;
using PokemonReviewApp.Helper;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Authorize(Roles = "Administrator, RegularUser")]
    [Route("api/[controller]")] // atributes  ex.:localhost:7000/Pokemon/GetPokemons
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository pokemonRepository;

        public PokemonController(IPokemonRepository pokemonRepository)
        {
            this.pokemonRepository = pokemonRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons() 
        {
            var response = pokemonRepository.GetPokemons();

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId) 
        {
            var response = pokemonRepository.GetPokemon(pokeId);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!pokemonRepository.PokemonExists(pokeId))
            {
                return NotFound();
            }

            return Ok(pokemonRepository.GetPokemonRating(pokeId));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonRequest pokemonCreate)
        {
            var response = pokemonRepository.CreatePokemon(ownerId, categoryId, pokemonCreate);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(String.Format(GlobalConstants.SuccessfulCreate, "pokemon"));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{pokeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdatePokemon(int pokeId, [FromBody] PokemonRequest pokemonUpdate)
        {
            var response = pokemonRepository.UpdatePokemon(pokeId, pokemonUpdate);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(String.Format(GlobalConstants.SuccessfulUpdate, "pokemon"));
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{pokeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeletePokemon(int pokeId)
        {
            var response = pokemonRepository.DeletePokemon(pokeId);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(String.Format(GlobalConstants.SuccessfulDelete, "pokemon"));
        }
    }
}
