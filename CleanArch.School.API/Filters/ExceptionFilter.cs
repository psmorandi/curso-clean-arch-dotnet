using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArch.School.API.Filters
{
    using System.Net;
    using System.Text.Json;
    using Microsoft.AspNetCore.Mvc;

    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var message = JsonSerializer.Serialize(context.Exception.Message);
            context.Result = new BadRequestObjectResult (message);
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        }
    }
}