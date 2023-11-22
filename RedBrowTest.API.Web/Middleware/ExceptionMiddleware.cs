using Newtonsoft.Json;
using RedBrowTest.Core.Application.Exceptions;
using RedBrowTest.Core.Application.Models;
using System.Net;

namespace RedBrowTest.API.Web.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "aplication/json";
                var statusCode = (int)HttpStatusCode.InternalServerError;
                string? errorCode = null;
                var errorResponse = new ErrorResponse("");

                switch (ex)
                {
                    case BadRequestException badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        errorCode = badRequestException.ErrorCode;
                        break;
                    case FluentValidation.ValidationException:
                        statusCode |= (int)HttpStatusCode.BadRequest;
                        errorResponse = new ErrorResponse(((FluentValidation.ValidationException)ex).Errors.Select(s => s.ErrorMessage).ToList());
                        break;
                    default:
                        break;
                }

                if (errorResponse.ErrorMessages.Count() == 1 &&
                    string.IsNullOrEmpty(errorResponse.ErrorMessages.First()))
                {
                    errorResponse = new ErrorResponse(ex.Message, errorCode);
                }

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), System.Text.Encoding.UTF8);
            }
        }
    }
}
