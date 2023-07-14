using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Dto.RequestDTOs;
using PokemonReviewApp.Helper;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper) 
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetCategories() 
        {
            var response = categoryRepository.GetCategories();

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryId) 
        {
            var response = categoryRepository.GetCategory(categoryId);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategory(int categoryId)
        {
            if (!categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var pokemons = mapper.Map<List<PokemonDto>>(categoryRepository.GetPokemonByCategory(categoryId));

            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryRequest categoryCreate)
        {
            var response = categoryRepository.CreateCategory(categoryCreate);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok("Successfully created new category!");
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryRequest categoryUpdate)
        {
            var response = categoryRepository.UpdateCategory(categoryId, categoryUpdate);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok("Successfully updated category!");
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteCategory(int categoryId) 
        {
            var response = categoryRepository.DeleteCategory(categoryId);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok("Successfully deleted category!");
        }

    }
}
