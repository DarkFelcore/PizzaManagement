using ErrorOr;

using MediatR;

using PizzaManagement.Application.Authentication.Common;
using PizzaManagement.Domain.Entities.Ratings;

namespace PizzaManagement.Application.Recipes.Ratings.GetByRecipeId
{
    public record GetUserRatingByRecipeIdQuery(
        Guid RecipeId,
        string Email
    ) : IRequest<ErrorOr<RatingResult>>;
}