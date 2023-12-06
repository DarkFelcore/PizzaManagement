namespace PizzaManagement.Application.Recipes.Common
{
    public record IngredientRequest(
        string Name,
        double? Quantity,
        string? Measurement
    );
}