using Mapster;

using PizzaManagement.Api.Extentions;
using PizzaManagement.Application.Authentication.Common;
using PizzaManagement.Application.Helpers;
using PizzaManagement.Application.Recipes.Get.All;
using PizzaManagement.Application.Recipes.Ratings.CreateUpdate;
using PizzaManagement.Application.Recipes.Ratings.GetByRecipeId;
using PizzaManagement.Contracts.Recipes;
using PizzaManagement.Contracts.Recipes.Ratings;
using PizzaManagement.Domain.Entities.Ratings;
using PizzaManagement.Domain.Entities.Recipes;
using PizzaManagement.Domain.Entities.RecipesIngredients;
using PizzaManagement.Domain.Entities.Steps;

namespace PizzaManagement.Api.Common.Mappings
{
    public class RecipeMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {

            config.NewConfig<RecipeSpecParams, GetAllRecipesQuery>()
                .Map(dest => dest.Search, src => src.Search);

            config.NewConfig<Step, StepResponse>()
                .Map(dest => dest, src => src);

            config.NewConfig<RecipeIngredient, IngredientResponse>()
                .Map(dest => dest.Name, src => src.Ingredient.Name)
                .Map(dest => dest, src => src);

            config.NewConfig<Rating, RatingResponse>()
                .Map(dest => dest.FirstName, src => src.User.FirstName)
                .Map(dest => dest.LastName, src => src.User.LastName)
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest, src => src);

            config.NewConfig<Recipe, RecipeResponse>()
                .Map(dest => dest.Image, src => src.Image != null ? Convert.ToBase64String(src.Image) : "")
                .Map(dest => dest.Ingredients, src => src.RecipesIngredients)
                .Map(dest => dest.PreparationTime.TimeMeasurement, src => EnumHelperExtentions.GetEnumDisplayName(src.PreparationTime.TimeMeasurement))
                .Map(dest => dest, src => src);


            // Ratings
            config.NewConfig<(string Email, CreateOrUpdateRatingRequest Request), CreateOrUpdateRatingCommand>()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest, src => src.Request);

            config.NewConfig<(Guid RecipeId, string Email), GetUserRatingByRecipeIdQuery>()
                .Map(src => src.RecipeId, dest => dest.RecipeId)
                .Map(src => src.Email, dest => dest.Email);

            config.NewConfig<RatingResult, RatingResponse>()
                .Map(dest => dest, src => src);
        }
    }
}