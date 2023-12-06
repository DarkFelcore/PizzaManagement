using PizzaManagement.Domain.Entities.Recipes;
using PizzaManagement.Domain.Entities.Users;

namespace PizzaManagement.Domain.Entities.Ratings
{
    public class Rating
    {
        // Users
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        // Recipes
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;

        // Additional properties
        public int Stars { get; private set; }
        public string? Comment { get; private set; }

        public Rating(Guid userId, Guid recipeId, int stars, string? comment)
        {
            UserId = userId;
            RecipeId = recipeId;
            Stars = stars;
            Comment = comment;
        }

        public void UpdateRating(int stars, string? comment)
        {
            Stars = stars;
            Comment = comment;
        }
    }
}