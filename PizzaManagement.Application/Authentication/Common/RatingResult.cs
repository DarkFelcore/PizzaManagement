namespace PizzaManagement.Application.Authentication.Common
{
    public record RatingResult(
        string FirstName,
        string LastName,
        string Email,
        int Stars,
        string? Comment
    );
}