namespace PizzaManagement.Contracts.Recipes
{
    public record RecipeResponse(
        Guid Id,
        string Title,
        string Description,
        string Image,
        PreparationTimeResponse PreparationTime,
        List<StepResponse> Steps,
        List<IngredientResponse> Ingredients,
        List<RatingResponse> Ratings
    );

    public record StepResponse(
        int Number,
        string Description
    );

    public record IngredientResponse(
        string Name,
        double? Quantity,
        string? Measurement
    );

    public record RatingResponse(
        string FirstName,
        string LastName,
        string Email,
        int Stars,
        string? Comment
    );

    public record PreparationTimeResponse(
        int Duration,
        string TimeMeasurement
    );
}