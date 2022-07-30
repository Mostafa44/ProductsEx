using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsEx.Application.Authentication.Commands.Register;
using ProductsEx.Application.Authentication.Queries.Login;
using ProductsEx.Application.Common.Errors;
using ProductsEx.Application.Authentication.Common;
using ProductsEx.Contracts.Authentication;

namespace ProductsEx.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMediator _mediator;

        public AuthenticationController(ILogger<AuthenticationController> logger,
                                         IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(request.FirstName,
                                              request.LastName,
                                              request.Email,
                                              request.Password);

            Result<AuthenticationResult> registerResult = await _mediator.Send(command);
            if (registerResult.IsSuccess)
            {
                return Ok(MapToAuthResult(registerResult.Value));
            }
            var firstError = registerResult.Errors[0];
            if (firstError is DuplicateEmailError)
            {
                return Problem(statusCode: StatusCodes.Status409Conflict, title: "Actually, This Email Aleady Exists");
            }
            return Problem();
            // AuthenticationResponse response = MapToAuthResult();
            //  return Ok(response);
        }

        private static AuthenticationResponse MapToAuthResult(AuthenticationResult authenticationResult)
        {
            return new AuthenticationResponse(authenticationResult.User.Id,
                                                     authenticationResult.User.FirstName,
                                                     authenticationResult.User.LastName,
                                                     authenticationResult.User.Email,
                                                     authenticationResult.Token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var loginQuery = new LoginQuery(request.Email, request.Password);
            var authenticationResult = await _mediator.Send(loginQuery);
            var response = new AuthenticationResponse(authenticationResult.User.Id,
                                                  authenticationResult.User.FirstName,
                                                  authenticationResult.User.LastName,
                                                  authenticationResult.User.Email,
                                                  authenticationResult.Token);
            return Ok(response);
        }
    }
}