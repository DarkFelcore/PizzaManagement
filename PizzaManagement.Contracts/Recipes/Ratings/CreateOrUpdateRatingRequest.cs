namespace PizzaManagement.Contracts.Recipes.Ratings
{
    public record CreateOrUpdateRatingRequest(
        string RecipeId,
        int Stars,
        string? Comment
    );
}