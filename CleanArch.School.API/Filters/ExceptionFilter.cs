using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArch.School.API.Filters
{
    using System.Net;
    using System.Text.Json;
    using Application.Exceptions;
    using Domain;
    using Microsoft.AspNetCore.Mvc;

    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                DomainException or ApplicationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };
            context.HttpContext.Response.StatusCode = statusCode;
            if (statusCode != (int)HttpStatusCode.BadRequest) return;
            var message = JsonSerializer.Serialize(context.Exception.Message);
            context.Result = new BadRequestObjectResult (message);
        }
    }
}