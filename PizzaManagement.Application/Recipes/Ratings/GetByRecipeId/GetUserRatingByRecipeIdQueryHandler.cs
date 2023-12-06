using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ErrorOr;

using MediatR;

using PizzaManagement.Application.Authentication.Common;
using PizzaManagement.Application.Common.Interfaces;
using PizzaManagement.Domain.Errors;

namespace PizzaManagement.Application.Recipes.Ratings.GetByRecipeId
{
    public class GetUserRatingByRecipeIdQueryHandler : IRequestHandler<GetUserRatingByRecipeIdQuery, ErrorOr<RatingResult>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserRatingByRecipeIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<RatingResult>> Handle(GetUserRatingByRecipeIdQuery query, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(query.Email);

            if(user is null)
            {
                return Errors.Users.NotFound;
            }

            var rating = await _unitOfWork.RecipeRepository.GetUserRatingByRecipeIdAsync(query.RecipeId, user.Id);

            if(rating is null)
            {
                return Errors.Recipes.RatingNotFound;
            }

            return new RatingResult(user.FirstName, user.LastName, user.Email, rating.Stars, rating.Comment);
        }
    }
}