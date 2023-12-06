using ErrorOr;

using MediatR;

using PizzaManagement.Application.Authentication.Common;

namespace PizzaManagement.Application.Recipes.Ratings.CreateUpdate
{
    public record CreateOrUpdateRatingCommand(
        string Email,
        string RecipeId,
        int Stars,
        string? Comment
    ) : IRequest<ErrorOr<RatingResult>>;
}