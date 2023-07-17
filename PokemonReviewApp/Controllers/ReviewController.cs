using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.Data;

namespace PokemonReviewApp.Controllers
{
    [Authorize(Roles = "Administrator, RegularUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IReviewerRepository reviewerRepository;
        private readonly IPokemonRepository pokemonRepository;
        private readonly IMapper mapper;

        public ReviewController(IReviewRepository reviewRepository, 
            IReviewerRepository reviewerRepository, IPokemonRepository pokemonRepository, IMapper mapper)
        {
            this.reviewRepository = reviewRepository;
            this.reviewerRepository = reviewerRepository;
            this.pokemonRepository = pokemonRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviews()
        {
            var reviews = mapper.Map<List<ReviewDto>>(reviewRepository.GetReviews());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }

            var reviews = mapper.Map<ReviewDto>(reviewRepository.GetReview(reviewId));

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }

        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(List<Review>))]
        [ProducesResponseType(200)]
        public IActionResult GetReviewsOfAPokemon(int pokeId)
        {
            var reviews = mapper.Map<List<ReviewDto>>(reviewRepository.GetReviewsOfAPokemon(pokeId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokemonId, 
           [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
            {
                return BadRequest(ModelState);
            }

            var review = reviewRepository.GetReviews()
                .Where(r => r.Title.ToUpper() == reviewCreate.Title.ToUpper())
                .FirstOrDefault();

            if (review != null)
            {
                ModelState.AddModelError("", "Review Already Exists!");
                StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewMap = mapper.Map<Review>(reviewCreate);
            reviewMap.Reviewer = reviewerRepository.GetReviewer(reviewerId);
            reviewMap.Pokemon = pokemonRepository.GetPokemon(pokemonId);

            if (!reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Error while saving review!");
                StatusCode(500, ModelState);
            }

            return Ok("Successfully created new review!");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto reviewUpdate)
        {
            if (reviewUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (reviewId != reviewUpdate.Id)
            {
                return BadRequest(ModelState);
            }

            if (!reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewMap = mapper.Map<Review>(reviewUpdate);

            if (!reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Error while updating review!");
            }

            return Ok("Successfully updated review!");
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }

            var reviewToDelete = reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!reviewRepository.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Error while deleting review");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted review!");
        }
    }
}
