using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PizzaManagement.Api.Extentions;
using PizzaManagement.Application.Authentication.CurrentUser;
using PizzaManagement.Application.Authentication.Login;
using PizzaManagement.Application.Authentication.Register;
using PizzaManagement.Contracts.Authentication.Common;
using PizzaManagement.Contracts.Authentication.Login;
using PizzaManagement.Contracts.Authentication.Register;

namespace PizzaManagement.Api.Controllers
{
    public class AuthController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            var email = AuthenticationExtentions.GetEmailByClaimTypesAsync(HttpContext.User);

            var query = new GetCurrentUserQuery(email);
            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                Problem
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var result = await _mediator.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                Problem
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);
            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                Problem
            );
        }

    }
}