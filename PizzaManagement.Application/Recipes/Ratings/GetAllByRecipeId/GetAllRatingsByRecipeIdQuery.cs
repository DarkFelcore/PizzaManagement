using ErrorOr;

using MediatR;

using PizzaManagement.Application.Authentication.Common;

namespace PizzaManagement.Application.Recipes.Ratings.GetAllByRecipeId
{
    public record GetAllRatingsByRecipeIdQuery(
        Guid RecipeId
    ): IRequest<ErrorOr<List<RatingResult>>>;
}