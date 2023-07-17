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
    public class CountryController : Controller
    {
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Country>))]
        [ProducesResponseType(400)]
        public IActionResult GetCountries()
        {
            var countries = mapper.Map<List<CountryDto>>(countryRepository.GetCountries());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            if (!countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }

            var country = mapper.Map<CountryDto>(countryRepository.GetCountry(countryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(country);
        }

        [HttpGet("owners/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(200)]
        public IActionResult GetCountryByOwner(int ownerId)
        {
            var country = mapper.Map<CountryDto>(countryRepository.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(country);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null)
            {
                return BadRequest(ModelState);
            }

            var country = countryRepository.GetCountries()
                .Where(c => c.Name.ToUpper() == countryCreate.Name.ToUpper())
                .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country Already Exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = mapper.Map<Country>(countryCreate);

            if (!countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Error while saving category!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created new country!");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{countryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto countryUpdate)
        {
            if (countryUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (countryId != countryUpdate.Id)
            {
                return BadRequest(ModelState);
            }

            if (!countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = mapper.Map<Country>(countryUpdate);

            if (!countryRepository.UpdateCountry(countryMap))
            {
                ModelState.AddModelError("", "Error while updating country!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated country!");
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteCountry(int countryId)
        {
            if (!countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }

            var countryToDelete = countryRepository.GetCountry(countryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!countryRepository.DeleteCountry(countryToDelete))
            {
                ModelState.AddModelError("", "Error while deleting country!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted country!");
        }
    }
}
