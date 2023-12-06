using Microsoft.EntityFrameworkCore;

using PizzaManagement.Application.Common.Interfaces;
using PizzaManagement.Domain.Entities.Ratings;
using PizzaManagement.Domain.Entities.Recipes;

namespace PizzaManagement.Infrastructure.Persistance.Repositories
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<Recipe?> GetByIdAsync(Guid id)
        {
            return _context.Recipes
                .Include(x => x.Ratings)
                    .ThenInclude(x => x.User)
                .Include(x => x.Steps)
                .Include(x => x.RecipesIngredients)
                    .ThenInclude(x => x.Ingredient)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override Task<List<Recipe>> AllAsync()
        {
            return _context.Recipes
                .Include(x => x.Ratings)
                    .ThenInclude(x => x.User)
                .Include(x => x.Steps)
                .Include(x => x.RecipesIngredients)
                    .ThenInclude(x => x.Ingredient)
                .ToListAsync();
        }

        public Task<List<Recipe>> GetAllRecipesWithSearch(string search)
        {
            return _context.Recipes
                .Include(x => x.Ratings)
                    .ThenInclude(x => x.User)
                .Include(x => x.Steps)
                .Include(x => x.RecipesIngredients)
                    .ThenInclude(x => x.Ingredient)
                .Where(x => x.Title.Contains(search))
                .ToListAsync();
        }

        public Task<List<Rating>> GetAllRatingsByRecipeIdAsync(Guid recipeId)
        {
            return _context.Ratings
                .Where(x => x.RecipeId == recipeId)
                .ToListAsync();
        }

        public Task<Rating?> GetUserRatingByRecipeIdAsync(Guid recipeId, Guid userId)
        {
            return _context.Ratings
                .FirstOrDefaultAsync(x => x.UserId == userId && x.RecipeId == recipeId);
        }
    }
}