using PizzaManagement.Domain.Entities.Recipes;
using PizzaManagement.Domain.Primitives;

namespace PizzaManagement.Domain.Entities.Steps
{
    public class Step : Entity
    {
        public int Number { get; private set; }
        public string Description { get; private set; } = null!;
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;
        public Step(Guid id, int number, string description, Guid recipeId) : base(id)
        {
            Number = number;
            Description = description;
            RecipeId = recipeId;
        }
    }
}