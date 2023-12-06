using ErrorOr;
using MediatR;
using PizzaManagement.Application.Authentication.Common;
using PizzaManagement.Application.Common.Interfaces;
using PizzaManagement.Domain.Entities.Users;
using PizzaManagement.Domain.Errors;

namespace PizzaManagement.Application.Authentication.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RegisterCommandHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            // Check if there is already a user with specified email
            if((await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email)) is not null)
            {
                return Errors.Users.DuplicateEmail;
            }

            // Hash the user password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);

            // Creating new user object
            var newUser = new User(
                id: Guid.NewGuid(),
                firstName: command.FirstName,
                lastName: command.LastName,
                email: command.Email,
                passwordHash: passwordHash
            );

            // Persist the user to the database
            await _unitOfWork.UserRepository.AddAsync(newUser);
            await _unitOfWork.CompleteAsync();

            // Generate the users token
            var token = _jwtTokenGenerator.GenerateToken(newUser);
            return new AuthenticationResult(newUser, token);
        }
    }
}