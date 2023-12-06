namespace PizzaManagement.Contracts.Recipes
{
    public record IngredientRequest(
        string Name,
        double? Quantity,
        string? Measurement
    );
}