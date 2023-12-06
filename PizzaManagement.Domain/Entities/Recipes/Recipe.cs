using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Http;

using PizzaManagement.Domain.Entities.Ratings;
using PizzaManagement.Domain.Entities.Recipes.ValueObjects;
using PizzaManagement.Domain.Entities.RecipesIngredients;
using PizzaManagement.Domain.Entities.Steps;
using PizzaManagement.Domain.Primitives;

namespace PizzaManagement.Domain.Entities.Recipes
{
    public class Recipe : Entity
    {
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        
        public byte[]? Image { get; private set; }

        public PreparationTime PreparationTime { get; private set; } = null!;
        public List<Step> Steps { get; set; } = null!;
        public List<RecipeIngredient> RecipesIngredients { get; set; } = null!;
        public List<Rating> Ratings { get; set; } = null!;

        // For creating a new Recipe (rating a recipe sould not be done at creation time)
        public Recipe(Guid id, string title, string description, byte[]? image, PreparationTime preparationTime, List<Step> steps, List<RecipeIngredient> recipeIngredients) : base(id)
        {
            Title = title;
            Description = description;
            Image = image;
            PreparationTime = preparationTime;
            Steps = steps;
            RecipesIngredients = recipeIngredients;
        }

        // Seeding a new Recipe
        public Recipe(Guid id, string title, string description, byte[]? image, PreparationTime preparationTime) : base(id)
        {
            Title = title;
            Description = description;
            Image = image;
            PreparationTime = preparationTime;
        }

        public Recipe()
        {
        }
    }
}