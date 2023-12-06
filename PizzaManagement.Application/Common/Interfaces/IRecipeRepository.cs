using PizzaManagement.Domain.Entities.Ratings;
using PizzaManagement.Domain.Entities.Recipes;

namespace PizzaManagement.Application.Common.Interfaces
{
    public interface IRecipeRepository : IGenericRepository<Recipe>
    {
        Task<List<Recipe>> GetAllRecipesWithSearch(string search);
        Task<List<Rating>> GetAllRatingsByRecipeIdAsync(Guid recipeId);
        Task<Rating?> GetUserRatingByRecipeIdAsync(Guid recipeId, Guid userId);
    }
}