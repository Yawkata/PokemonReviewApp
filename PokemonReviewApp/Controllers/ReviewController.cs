using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Helper;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Authorize(Roles = "Administrator, RegularUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviews()
        {
            var response = reviewRepository.GetReviews();

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            var response = reviewRepository.GetReview(reviewId);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(List<Review>))]
        [ProducesResponseType(200)]
        public IActionResult GetReviewsOfAPokemon(int pokeId)
        {
            var response = reviewRepository.GetReviewsOfAPokemon(pokeId);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokemonId, 
           [FromBody] ReviewRequest reviewCreate)
        {
            var response = reviewRepository.CreateReview(reviewerId, pokemonId, reviewCreate);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(String.Format(GlobalConstants.SuccessfulCreate, "review"));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewRequest reviewUpdate)
        {
            var response = reviewRepository.UpdateReview(reviewId, reviewUpdate);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(String.Format(GlobalConstants.SuccessfulUpdate, "review"));
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteReview(int reviewId)
        {
            var response = reviewRepository.DeleteReview(reviewId);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(String.Format(GlobalConstants.SuccessfulDelete, "review"));
        }
    }
}
