using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto.ResponseDTOs;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Helper;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using AutoMapper;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PokemonRepository(DataContext context, IMapper mapper) 
        {     
            _context = context;
            _mapper = mapper;
        }

        public PokemonResponse GetPokemons()
        {
            PokemonResponse response = new();

            var pokemons = _context.Pokemon.OrderBy(c => c.Id).ToList();

            response.Pokemons = _mapper.Map<List<PokemonDto>>(pokemons);
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public PokemonResponse GetPokemon(int pokeId)
        {
            PokemonResponse response = new();

            if (!PokemonExists(pokeId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Pokemon");
                return response;
            }

            var pokemon = _context.Pokemon.FirstOrDefault(c => c.Id == pokeId);
            response.Pokemons.Add(_mapper.Map<PokemonDto>(pokemon));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);

            if (review.Count() <= 0)
            {
                return 0;
            }

            return ((decimal)review.Average(r => r.Rating));
        }

        public PokemonResponse CreatePokemon(int ownerId, int categoryId, PokemonRequest pokemonCreate)
        {
            PokemonResponse response = new();

            if (PokemonExists(pokemonCreate.Name))
            {
                response.ServerMessage = String.Format(GlobalConstants.AlreadyExists, "Pokemon");
                return response;
            }

            var pokeMap = _mapper.Map<Pokemon>(pokemonCreate);

            var ownerEntity = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = ownerEntity,
                Pokemon = pokeMap,
            };

            _context.Add(pokemonOwner);

            var categoryEntity = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var pokemonCategory = new PokemonCategory()
            {
                Category = categoryEntity,
                Pokemon = pokeMap,
            };

            _context.Add(pokemonCategory);

            _context.Add(pokeMap);
            Save();

            var pokemon = _context.Pokemon.FirstOrDefault(x => x.Name == pokemonCreate.Name);
            response.Pokemons.Add(_mapper.Map<PokemonDto>(pokemon));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public PokemonResponse UpdatePokemon(int pokeId, PokemonRequest pokemonUpdate)
        {
            PokemonResponse response = new();

            if (!PokemonExists(pokeId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Pokemon");
                return response;
            }

            if (PokemonExists(pokemonUpdate.Name))
            {
                response.ServerMessage = String.Format(GlobalConstants.AlreadyExists, "Pokemon");
                return response;
            }

            var pokemon = _mapper.Map<Pokemon>(pokemonUpdate);
            pokemon.Id = pokeId;

            _context.Update(pokemon);
            Save();

            response.Pokemons.Add(_mapper.Map<PokemonDto>(pokemon));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public PokemonResponse DeletePokemon(int pokeId)
        {
            PokemonResponse response = new();

            if (!PokemonExists(pokeId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Pokemon");
                return response;
            }

            var pokemonToDelete = _context.Pokemon.FirstOrDefault(c => c.Id == pokeId);
            var reviewsToDelete = _context.Pokemon.Where(p => p.Id == pokeId).Select(p => p.Reviews).FirstOrDefault();

            _context.RemoveRange(reviewsToDelete);
            _context.Remove(pokemonToDelete);
            Save();

            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public bool PokemonExists(int id)
        {
            return _context.Pokemon.Any(p => p.Id == id);
        }

        public bool PokemonExists(string name)
        {
            return _context.Pokemon.Any(p => p.Name.ToUpper() == name.ToUpper());
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
