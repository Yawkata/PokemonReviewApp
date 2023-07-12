using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository reviewerRepository;
        private readonly IMapper mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            this.reviewerRepository = reviewerRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Reviewer>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewers()
        {
            var reviewers = mapper.Map<List<ReviewerDto>>(reviewerRepository.GetReviewers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviewers);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!reviewerRepository.ReviewerExists(reviewerId))
            {
                return NotFound();
            }

            var reviewers = mapper.Map<ReviewerDto>(reviewerRepository.GetReviewer(reviewerId));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviewers);
        }

        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(200, Type = typeof(List<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!reviewerRepository.ReviewerExists(reviewerId))
            { 
                return NotFound();
            }

            var reviews = mapper.Map<List<ReviewDto>>(reviewerRepository.GetReviewsByReviewer(reviewerId));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerCreate)
        {
            if (reviewerCreate == null)
            {
                return BadRequest(ModelState);
            }

            var reviewer = reviewerRepository.GetReviewers()
                .Where(r => r.FirstName.ToUpper() == reviewerCreate.FirstName.ToUpper() &&
                r.LastName.ToUpper() == reviewerCreate.LastName.ToUpper())
                .FirstOrDefault();

            if (reviewer != null)
            {
                ModelState.AddModelError("", "Reviewer Already Exists!");
                StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewerMap = mapper.Map<Reviewer>(reviewerCreate);

            if (!reviewerRepository.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Error while saving reviewer!");
                StatusCode(500, ModelState);
            }

            return Ok("Successfully created new reviewer!");
        }
    }
}
