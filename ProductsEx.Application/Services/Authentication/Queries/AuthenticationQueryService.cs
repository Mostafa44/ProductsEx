using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using ProductsEx.Application.Common.Errors;
using ProductsEx.Application.Common.Interfaces.Authentication;
using ProductsEx.Application.Persistence;
using ProductsEx.Application.Services.Authentication.Common;
using ProductsEx.Domain.Entities;

namespace ProductsEx.Application.Services.Authentication.Queries
{
    public class AuthenticationQueryService : IAuthenticationQueryService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator,
                                     IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login(string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("This user does not exist!");
            }
            if (user.Password != password)
            {
                throw new Exception("Password is not correct!");
            }
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(
           user,
            token);
        }


    }
}