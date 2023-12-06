using ErrorOr;

using MediatR;

using PizzaManagement.Application.Common.Interfaces;
using PizzaManagement.Domain.Entities.Recipes;

namespace PizzaManagement.Application.Recipes.Get.All
{
    public class GetAllRecipesQueryHandler : IRequestHandler<GetAllRecipesQuery, ErrorOr<List<Recipe>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllRecipesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<Recipe>>> Handle(GetAllRecipesQuery query, CancellationToken cancellationToken)
        {
            if(query.Search is null || string.IsNullOrEmpty(query.Search))
            {
                var data = await _unitOfWork.RecipeRepository.AllAsync();
                return data;
            }
            return await _unitOfWork.RecipeRepository.GetAllRecipesWithSearch(query.Search!);
        }
    }
}