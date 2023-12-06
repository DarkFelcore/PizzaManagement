using ErrorOr;

using MediatR;

using PizzaManagement.Domain.Entities.Recipes;

namespace PizzaManagement.Application.Recipes.Get.ById
{
    public record GetRecipeByIdQuery(
        Guid RecipeId
    ): IRequest<ErrorOr<Recipe>>;
}