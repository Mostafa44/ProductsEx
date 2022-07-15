using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductsEx.Api.Filters
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {


        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var problemDetails = new ProblemDetails()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Title = "An error occured while processing your request",
                Status = (int)HttpStatusCode.InternalServerError,

            };
            context.Result = new ObjectResult(problemDetails);
            context.ExceptionHandled = true;
        }





    }
}