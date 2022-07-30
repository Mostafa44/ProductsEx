using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using ProductsEx.Application.Common.Errors;
using ProductsEx.Application.Common.Interfaces.Authentication;
using ProductsEx.Application.Persistence;
using ProductsEx.Application.Authentication.Common;
using ProductsEx.Domain.Entities;

namespace ProductsEx.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public Task<Result<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (_userRepository.GetUserByEmail(command.Email) is not null)
            {
                return Task.FromResult<Result<AuthenticationResult>>(Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() }));
            }
            var user = new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password
            };

            _userRepository.AddUser(user);

            var token = _jwtTokenGenerator.GenerateToken(user);
            return Task.FromResult<Result<AuthenticationResult>>(new AuthenticationResult(user,
            token));
        }
    }
}