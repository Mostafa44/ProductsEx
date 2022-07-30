using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProductsEx.Application.Common.Interfaces.Authentication;
using ProductsEx.Application.Persistence;
using ProductsEx.Application.Authentication.Common;
using ProductsEx.Domain.Entities;

namespace ProductsEx.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {

        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            if (_userRepository.GetUserByEmail(query.Email) is not User user)
            {
                throw new Exception("This user does not exist!");
            }
            if (user.Password != query.Password)
            {
                throw new Exception("Password is not correct!");
            }
            var token = _jwtTokenGenerator.GenerateToken(user);
            return Task.FromResult<AuthenticationResult>(new AuthenticationResult(
           user,
            token));
        }
    }
}