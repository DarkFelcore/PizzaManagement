using ErrorOr;
using MediatR;
using PizzaManagement.Application.Authentication.Common;

namespace PizzaManagement.Application.Authentication.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password
    ): IRequest<ErrorOr<AuthenticationResult>>;
}