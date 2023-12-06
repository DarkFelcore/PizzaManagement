using ErrorOr;

namespace PizzaManagement.Domain.Errors
{
    public partial class Errors
    {
        public static class Recipes
        {
            public static Error NotFound => Error.NotFound(
                code: "Recipe.NotFound",
                description: "Recipe not found."
            );
            public static Error DuiplicateRating => Error.Conflict(
                code: "Recipe.DuiplicateRating",
                description: "You have already rated this recipe."
            );
            public static Error RatingNotFound => Error.NotFound(
                code: "Recipe.RatingNotFound",
                description: "Rating for this recipe was not found."
            );
        }
    }
}