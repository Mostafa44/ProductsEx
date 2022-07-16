using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProductsEx.Application.Common.Errors
{
    public class DuplicateEmailException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "Email Already exists";
    }
}