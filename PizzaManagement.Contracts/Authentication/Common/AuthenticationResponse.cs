namespace PizzaManagement.Contracts.Authentication.Common
{
    public record AuthenticationResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Token
    );
}