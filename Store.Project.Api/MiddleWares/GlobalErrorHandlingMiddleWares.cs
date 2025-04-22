using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorsModels;

namespace Store.Project.Api.MiddleWares
{
    public class GlobalErrorHandlingMiddleWares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleWares> _logger;

        public GlobalErrorHandlingMiddleWares(RequestDelegate next , ILogger<GlobalErrorHandlingMiddleWares> logger)
        {
           _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.Response.ContentType = "application/json";
                    var responce = new ErrorDetails()
                    {
                        StatusCode = StatusCodes.Status404NotFound,

                        ErrorMessage = $"End Point {context.Request.Path} is Not Found"
                    };

                  await  context.Response.WriteAsJsonAsync(responce);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

              //  context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var responce = new ErrorDetails()
                {
                    
                    ErrorMessage = ex.Message
                };

                responce.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                     _ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = responce.StatusCode;

                await context.Response.WriteAsJsonAsync(responce );
            }       
        }
    }
}
