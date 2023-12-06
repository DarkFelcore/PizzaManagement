using ErrorOr;

using MediatR;

using PizzaManagement.Application.Authentication.Common;
using PizzaManagement.Application.Common.Interfaces;
using PizzaManagement.Domain.Entities.Users;
using PizzaManagement.Domain.Errors;

namespace PizzaManagement.Application.Authentication.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginQueryHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // Check if we have a user with given email
            if(await _unitOfWork.UserRepository.GetUserByEmailAsync(query.Email) is not User user)
            {
                return Errors.Users.InvalidCredentials;
            }

            if(!BCrypt.Net.BCrypt.Verify(query.Password, user.PasswordHash))
            {
                return Errors.Users.InvalidCredentials;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}