using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc; // model view controller
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")] // atributes  ex.:localhost:7000/Pokemon/GetPokemons
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository pokemonRepository;
        private readonly IOwnerRepository ownerRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public PokemonController(IPokemonRepository pokemonRepository, 
            IOwnerRepository ownerRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.pokemonRepository = pokemonRepository;
            this.ownerRepository = ownerRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons() 
        {
            var pokemons = mapper.Map<List<PokemonDto>>(pokemonRepository.GetPokemons());

            if (!ModelState.IsValid)
            {  // if we submit wrong data to our api the model state is going to pick that up and return bad request
                return BadRequest(ModelState);
            }

            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId) 
        {
            if (!pokemonRepository.PokemonExists(pokeId))
            {
                return NotFound();
            }

            var pokemon = mapper.Map<PokemonDto>(pokemonRepository.GetPokemon(pokeId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(pokemon); 
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

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(pokemonRepository.GetPokemonRating(pokeId));
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
            {
                return BadRequest(ModelState);
            }

            var pokemon = pokemonRepository.GetPokemons()
                .Where(p => p.Name.ToUpper() == pokemonCreate.Name.ToUpper())
                .FirstOrDefault();

            if (pokemon != null)
            {
                ModelState.AddModelError("", "Pokemon Already Exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }    

            var pokemonMap = mapper.Map<Pokemon>(pokemonCreate);

            if (!pokemonRepository.CreatePokemon(ownerId, categoryId, pokemonMap))
            {
                ModelState.AddModelError("", "Error while saving pokemon!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created new pokemon!");
        }

        [HttpPut("{pokeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdatePokemon(int pokeId, [FromBody] PokemonDto pokemonUpdate)
        {
            if (pokemonUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (pokeId != pokemonUpdate.Id)
            {
                return BadRequest(ModelState);
            }

            if (!pokemonRepository.PokemonExists(pokeId)) 
            { 
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pokemonMap = mapper.Map<Pokemon>(pokemonUpdate);

            if (!pokemonRepository.UpdatePokemon(pokemonMap))
            {
                ModelState.AddModelError("", "Error while updating pokemon");
            }

            return Ok("Successfully updated pokemon!");
        }
    }
}
