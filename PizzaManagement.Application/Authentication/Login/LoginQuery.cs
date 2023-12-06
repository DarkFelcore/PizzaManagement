using ErrorOr;

using MediatR;

using PizzaManagement.Application.Authentication.Common;

namespace PizzaManagement.Application.Authentication.Login
{
    public record LoginQuery(
        string Email,
        string Password
    ): IRequest<ErrorOr<AuthenticationResult>>;
}