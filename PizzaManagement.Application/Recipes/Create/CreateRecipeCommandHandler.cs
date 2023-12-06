using ErrorOr;

using MediatR;

using Newtonsoft.Json;

using PizzaManagement.Application.Common.Interfaces;
using PizzaManagement.Application.Recipes.Common;
using PizzaManagement.Domain.Entities.Ingredients;
using PizzaManagement.Domain.Entities.Recipes;
using PizzaManagement.Domain.Entities.Recipes.Enums;
using PizzaManagement.Domain.Entities.Recipes.ValueObjects;
using PizzaManagement.Domain.Entities.RecipesIngredients;
using PizzaManagement.Domain.Entities.Steps;

namespace PizzaManagement.Application.Recipes.Create
{
    public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand, ErrorOr<Recipe>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseCacheService _responseCacheService;

        public CreateRecipeCommandHandler(IUnitOfWork unitOfWork, IResponseCacheService responseCacheService)
        {
            _unitOfWork = unitOfWork;
            _responseCacheService = responseCacheService;
        }

        public async Task<ErrorOr<Recipe>> Handle(CreateRecipeCommand command, CancellationToken cancellationToken)
        {
            // Generate new Recipe Identifier
            var recipeId = Guid.NewGuid();

            byte[]? image = null;

            // Convert json strings step and ingredients into lists
            var deserializedSteps = JsonConvert.DeserializeObject<List<StepRequest>>(command.Steps);
            var deserializedIngredients = JsonConvert.DeserializeObject<List<IngredientRequest>>(command.Ingredients);

            // Preparation Time
            var preparationTime = new PreparationTime(command.PreparationTime.Duration, (TimeMeasurement)Enum.Parse(typeof(TimeMeasurement), command.PreparationTime.TimeMeasurement));

            // Image login here
            if (command.ImageFile != null && command.ImageFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await command.ImageFile.CopyToAsync(memoryStream, cancellationToken);
                image = memoryStream.ToArray();
            }

            var steps = new List<Step>();
            foreach (var item in deserializedSteps!)
            {
                steps.Add(new Step(Guid.NewGuid(), item.Number, item.Description, recipeId));
            }

            var recipesIngredients = new List<RecipeIngredient>();

            foreach (var i in deserializedIngredients!)
            {
                // Ingredient exists
                if ((await _unitOfWork.IngredientRepository.GetIngredientByNameAsync(i.Name)) is Ingredient ingredient)
                {
                    recipesIngredients.Add(new RecipeIngredient(recipeId, ingredient.Id, i.Quantity, i.Measurement));
                }
                else
                {
                    var newIngredient = await _unitOfWork.IngredientRepository.AddAsync(new Ingredient(Guid.NewGuid(), i.Name));
                    recipesIngredients.Add(new RecipeIngredient(recipeId, newIngredient.Id, i.Quantity, i.Measurement));
                }
            }

            var recipe = new Recipe(recipeId, command.Title, command.Description, image, preparationTime, steps, recipesIngredients);

            await _unitOfWork.RecipeRepository.AddAsync(recipe);
            await _unitOfWork.CompleteAsync();

            // Reset the cache for getting all recipes since a new Recipe will be added
            _responseCacheService.SetResetAllRecipeCache(true);

            return recipe;
        }
    }
}