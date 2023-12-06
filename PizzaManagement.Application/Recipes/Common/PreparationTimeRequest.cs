namespace PizzaManagement.Application.Recipes.Common
{
    public record PreparationTimeRequest(
        int Duration,
        string TimeMeasurement
    );
}