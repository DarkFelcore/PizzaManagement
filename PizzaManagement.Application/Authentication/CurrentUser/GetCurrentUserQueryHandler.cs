using ErrorOr;

using MediatR;

using PizzaManagement.Application.Authentication.Common;
using PizzaManagement.Application.Common.Interfaces;
using PizzaManagement.Domain.Errors;

namespace PizzaManagement.Application.Authentication.CurrentUser
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public GetCurrentUserQueryHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(query.Email);

            if(user is null) return Errors.Users.InvalidCredentials;

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}