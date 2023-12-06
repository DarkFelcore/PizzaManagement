using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Http;

using PizzaManagement.Application.Recipes.Common;
using PizzaManagement.Domain.Entities.Recipes;

namespace PizzaManagement.Application.Recipes.Create
{
    public record CreateRecipeCommand(
        string Title,
        string Description,
        IFormFile? ImageFile,
        PreparationTimeRequest PreparationTime,
        string Steps,
        string Ingredients
    ) : IRequest<ErrorOr<Recipe>>;
}