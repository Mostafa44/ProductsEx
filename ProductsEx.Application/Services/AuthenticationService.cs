using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using ProductsEx.Application.Common.Errors;
using ProductsEx.Application.Common.Interfaces.Authentication;
using ProductsEx.Application.Persistence;
using ProductsEx.Domain.Entities;

namespace ProductsEx.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator,
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

        public Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) is not null)
            {
                return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
            }
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _userRepository.AddUser(user);

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user,
            token);
        }
    }
}