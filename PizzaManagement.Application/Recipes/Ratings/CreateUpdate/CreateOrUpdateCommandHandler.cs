using ErrorOr;

using MediatR;

using PizzaManagement.Application.Authentication.Common;
using PizzaManagement.Application.Common.Interfaces;
using PizzaManagement.Domain.Entities.Ratings;
using PizzaManagement.Domain.Errors;

namespace PizzaManagement.Application.Recipes.Ratings.CreateUpdate
{
    public class CreateOrUpdateCommandHandler : IRequestHandler<CreateOrUpdateRatingCommand, ErrorOr<RatingResult>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrUpdateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<RatingResult>> Handle(CreateOrUpdateRatingCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email);

            if (user is null)
            {
                return Errors.Users.NotFound;
            }

            var recipe = await _unitOfWork.RecipeRepository.GetByIdAsync(Guid.Parse(command.RecipeId));

            if (recipe is null)
            {
                return Errors.Recipes.NotFound;
            }

            var rating = await _unitOfWork.RecipeRepository.GetUserRatingByRecipeIdAsync(recipe.Id, user.Id);
            // Check if user already rated the recipe
            if (rating is not null)
            {
                // Update rating
                rating.UpdateRating(command.Stars, command.Comment);
            }
            else
            {
                // Create rating
                rating = new Rating(user.Id, Guid.Parse(command.RecipeId), command.Stars, command.Comment);
                recipe.Ratings.Add(rating);
            }

            await _unitOfWork.RecipeRepository.UpdateAsync(recipe);
            await _unitOfWork.CompleteAsync();

            return new RatingResult(user.FirstName, user.LastName, user.Email, rating.Stars, rating.Comment);
        }
    }
}