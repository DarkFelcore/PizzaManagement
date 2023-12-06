using ErrorOr;

using MediatR;

using PizzaManagement.Application.Common.Interfaces;
using PizzaManagement.Domain.Entities.Recipes;
using PizzaManagement.Domain.Errors;

namespace PizzaManagement.Application.Recipes.Get.ById
{
    public class GetRecipeByIdQueryHandler : IRequestHandler<GetRecipeByIdQuery, ErrorOr<Recipe>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRecipeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Recipe>> Handle(GetRecipeByIdQuery query, CancellationToken cancellationToken)
        {
            var recipe = await _unitOfWork.RecipeRepository.GetByIdAsync(query.RecipeId);

            if(recipe is null) {
                return Errors.Recipes.NotFound;
            }

            return recipe;
        }
    }
}