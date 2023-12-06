using PizzaManagement.Domain.Entities.Users;

namespace PizzaManagement.Application.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token
    );
}