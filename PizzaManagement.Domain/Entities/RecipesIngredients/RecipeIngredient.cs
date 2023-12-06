using PizzaManagement.Domain.Entities.Ingredients;
using PizzaManagement.Domain.Entities.Recipes;

namespace PizzaManagement.Domain.Entities.RecipesIngredients
{
    public class RecipeIngredient
    {
        // Recipe
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;

        // Ingredient
        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;

        // Additional properties
        public double? Quantity { get; private set; }
        public string? Measurement { get; private set; }
        public RecipeIngredient(Guid recipeId, Guid ingredientId, double? quantity = null, string? measurement = null)
        {
            RecipeId = recipeId;
            IngredientId = ingredientId;
            Quantity = quantity;
            Measurement = measurement;
        }
    }
}