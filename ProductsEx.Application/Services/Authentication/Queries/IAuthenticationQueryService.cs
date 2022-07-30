using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using ProductsEx.Application.Services.Authentication.Common;

namespace ProductsEx.Application.Services.Authentication.Queries
{
    public interface IAuthenticationQueryService
    {
        AuthenticationResult Login(string email, string password);

    }
}