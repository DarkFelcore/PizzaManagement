
using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PizzaManagement.Api.Extentions;
using PizzaManagement.Api.Helpers;
using PizzaManagement.Application.Helpers;
using PizzaManagement.Application.Recipes.Create;
using PizzaManagement.Application.Recipes.Get.All;
using PizzaManagement.Application.Recipes.Get.ById;
using PizzaManagement.Application.Recipes.Ratings.CreateUpdate;
using PizzaManagement.Application.Recipes.Ratings.GetAllByRecipeId;
using PizzaManagement.Application.Recipes.Ratings.GetByRecipeId;
using PizzaManagement.Contracts.Recipes;
using PizzaManagement.Contracts.Recipes.Ratings;

namespace PizzaManagement.Api.Controllers
{
    public class RecipesController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public RecipesController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Cached(600)]
        public async Task<IActionResult> GetAllRecipesAsync([FromQuery] RecipeSpecParams recipeSpecParams)
        {
            var query = _mapper.Map<GetAllRecipesQuery>(recipeSpecParams);
            var result = await _mediator.Send(query);

            var mappedRecipes = result.Value.Select(x => _mapper.Map<RecipeResponse>(x)).ToList();

            return result.Match(
                _ => Ok(mappedRecipes),
                Problem
            );
        }

        [HttpGet("{recipeId:guid}")]
        [Cached(600)]
        public async Task<IActionResult> GetRecipeByIdAsync([FromRoute]Guid recipeId) {
            var query = new GetRecipeByIdQuery(recipeId);
            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<RecipeResponse>(result)),
                Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipeAsync([FromForm] CreateRecipeCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<RecipeResponse>(result)),
                Problem
            );
        }

        [Authorize]
        [HttpPut("rating")]
        public async Task<IActionResult> CreateOrUpdateRatingAsync(CreateOrUpdateRatingRequest request)
        {
            var email = AuthenticationExtentions.GetEmailByClaimTypesAsync(HttpContext.User);

            var command = _mapper.Map<CreateOrUpdateRatingCommand>((email, request));
            var result = await _mediator.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<RatingResponse>(result)),
                Problem
            );
        }

        [Authorize]
        [HttpGet("ratings/{ratingId:guid}")]
        public async Task<IActionResult> GetAllRecipeRatingsAsync([FromRoute] Guid ratingId)
        {
            var query = new GetAllRatingsByRecipeIdQuery(ratingId);
            var result = await _mediator.Send(query);

            var mappedRatingResponse = new List<RatingResponse>();

            if (!result.IsError)
            {
                mappedRatingResponse = result.Value.Select(x => _mapper.Map<RatingResponse>(x)).ToList();
            }
            
            return result.Match(
                result => Ok(mappedRatingResponse),
                Problem
            );
        }

        [Authorize]
        [HttpGet("rating/{ratingId:guid}")]
        public async Task<IActionResult> GetUserRatingByRecipeIdAsync([FromRoute] Guid ratingId)
        {
            var email = AuthenticationExtentions.GetEmailByClaimTypesAsync(HttpContext.User);

            var query = _mapper.Map<GetUserRatingByRecipeIdQuery>((ratingId, email));
            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<RatingResponse>(result)),
                Problem
            );
        }
    }
}