using PizzaManagement.Domain.Entities.RecipesIngredients;
using PizzaManagement.Domain.Primitives;

namespace PizzaManagement.Domain.Entities.Ingredients
{
    public class Ingredient : Entity
    {
        public string Name { get; set; } = string.Empty;
        public List<RecipeIngredient> RecipesIngredients = null!;
        public Ingredient(Guid id, string name) : base(id)
        {
            Name = name;
        }
    }
}