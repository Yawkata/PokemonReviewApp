using AutoMapper;
using Azure;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Dto.RequestDTOs;
using PokemonReviewApp.Dto.ResponseDTOs;
using PokemonReviewApp.Helper;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public CategoryResponse GetCategories()
        {
            CategoryResponse response = new();

            var categories = _context.Categories.OrderBy(c => c.Id).ToList();

            response.Categories = _mapper.Map<List<CategoryDto>>(categories);
            response.ServerMessage= GlobalConstants.Success;

            return response;
        }

        public CategoryResponse GetCategory(int categoryId)
        {
            CategoryResponse response = new();

            if (!CategoryExists(categoryId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Category");
                return response;
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);
            response.Categories.Add(_mapper.Map<CategoryDto>(category));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public PokemonResponse GetPokemonByCategory(int categoryId)
        {
            PokemonResponse response = new();

            if (!CategoryExists(categoryId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Category");
                return response;
            }

            var pokemons = _context.PokemonCategories.Where(o => o.CategoryId == categoryId).Select(p => p.Pokemon).ToList();

            response.Pokemons = _mapper.Map<List<PokemonDto>>(pokemons);
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public CategoryResponse CreateCategory(CategoryRequest categoryCreate)
        {
            CategoryResponse response = new();

            if (CategoryExists(categoryCreate.Name))
            {
                response.ServerMessage = String.Format(GlobalConstants.AlreadyExists, "Category");
                return response;
            }

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            _context.Add(categoryMap);
            Save();

            var category = _context.Categories.FirstOrDefault(x => x.Name == categoryCreate.Name);
            response.Categories.Add(_mapper.Map<CategoryDto>(category));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public CategoryResponse UpdateCategory(int categoryId, CategoryRequest categoryUpdate)
        {
            CategoryResponse response = new();

            if (!CategoryExists(categoryId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Category");
                return response;
            }

            if (CategoryExists(categoryUpdate.Name))
            {
                response.ServerMessage = String.Format(GlobalConstants.AlreadyExists, "Category");
                return response;
            }

            var category = _mapper.Map<Category>(categoryUpdate);
            category.Id = categoryId;

            _context.Update(category);
            Save();
;
            response.Categories.Add(_mapper.Map<CategoryDto>(category));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public CategoryResponse DeleteCategory(int categoryId) 
        {
            CategoryResponse response = new();

            if (!CategoryExists(categoryId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Category");
                return response;
            }

            var categoryToDelete = _context.Categories.FirstOrDefault(c => c.Id == categoryId);

            _context.Remove(categoryToDelete);
            Save();

            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.Id == categoryId);
        }

        public bool CategoryExists(string categoryName)
        {
            return _context.Categories.Any(x => x.Name.ToUpper() == categoryName.ToUpper());
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
