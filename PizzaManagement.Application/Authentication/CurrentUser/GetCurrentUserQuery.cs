using ErrorOr;

using MediatR;

using PizzaManagement.Application.Authentication.Common;

namespace PizzaManagement.Application.Authentication.CurrentUser
{
    public record GetCurrentUserQuery(
        string? Email
    ) : IRequest<ErrorOr<AuthenticationResult>>;
}