using ErrorOr;

using MediatR;

using PizzaManagement.Application.Authentication.Common;
using PizzaManagement.Application.Common.Interfaces;
using PizzaManagement.Domain.Errors;

namespace PizzaManagement.Application.Recipes.Ratings.GetAllByRecipeId
{
    public class GetAllRatingsByRecipeIdQueryHandler : IRequestHandler<GetAllRatingsByRecipeIdQuery, ErrorOr<List<RatingResult>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllRatingsByRecipeIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<RatingResult>>> Handle(GetAllRatingsByRecipeIdQuery query, CancellationToken cancellationToken)
        {
            var ratingResultList = new List<RatingResult>();
            var ratings = await _unitOfWork.RecipeRepository.GetAllRatingsByRecipeIdAsync(query.RecipeId);

            if(ratings.Count == 0) return Errors.Recipes.RatingNotFound;

            foreach (var rating in ratings)
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(rating.UserId);

                if(user is null) return Errors.Users.NotFound;

                ratingResultList.Add(new RatingResult(user.FirstName, user.LastName, user.Email, rating.Stars, rating.Comment));
            }

            return ratingResultList;
        }
    }
}