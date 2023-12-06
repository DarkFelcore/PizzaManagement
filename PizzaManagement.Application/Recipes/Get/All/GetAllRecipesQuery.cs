using ErrorOr;

using MediatR;

using PizzaManagement.Domain.Entities.Recipes;

namespace PizzaManagement.Application.Recipes.Get.All
{
    public record GetAllRecipesQuery(
        string? Search
    ) : IRequest<ErrorOr<List<Recipe>>>;
}