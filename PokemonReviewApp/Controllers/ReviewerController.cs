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
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository reviewerRepository;

        public ReviewerController(IReviewerRepository reviewerRepository)
        {
            this.reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Reviewer>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewers()
        {
            var response = reviewerRepository.GetReviewers();

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer(int reviewerId)
        {
            var response = reviewerRepository.GetReviewer(reviewerId);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(200, Type = typeof(List<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            var response = reviewerRepository.GetReviewsByReviewer(reviewerId);

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
        public IActionResult CreateReviewer([FromBody] ReviewerRequest reviewerCreate)
        {
            var response = reviewerRepository.CreateReviewer(reviewerCreate);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(String.Format(GlobalConstants.SuccessfulCreate, "reviewer"));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{reviewerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateReviewer(int reviewerId, [FromBody] ReviewerRequest reviewerUpdate)
        {
            var response = reviewerRepository.UpdateReviewer(reviewerId, reviewerUpdate);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(String.Format(GlobalConstants.SuccessfulUpdate, "reviewer"));
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{reviewerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteReviewer(int reviewerId)
        {
            var response = reviewerRepository.DeleteReviewer(reviewerId);

            if (response.ServerMessage != GlobalConstants.Success)
            {
                return BadRequest(response);
            }

            return Ok(String.Format(GlobalConstants.SuccessfulDelete, "reviewer"));
        }
    }
}
