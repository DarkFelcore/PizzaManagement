namespace PizzaManagement.Contracts.Recipes
{
    public record PreparationTimeRequest(
        int Duration,
        string TimeMeasurement
    );
}