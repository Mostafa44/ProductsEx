using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsEx.Domain.Entities;

namespace ProductsEx.Application.Authentication.Common
{
    public record AuthenticationResult(
         User User,
         string Token

     );
}