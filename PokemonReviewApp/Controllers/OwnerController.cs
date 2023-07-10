﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository ownerRepository;
        private readonly IMapper mapper;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            this.ownerRepository = ownerRepository;
            this.mapper = mapper;
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

        // Does Not Work !
        [HttpGet("{pokeID}/owners")]
        [ProducesResponseType(200, Type = typeof(List<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnersOfAPokemon(int pokeId)
        {
            var owners = mapper.Map<List<OwnerDto>>(ownerRepository.GetOwnersOfAPokemon(pokeId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(owners);
        }
    }
}
