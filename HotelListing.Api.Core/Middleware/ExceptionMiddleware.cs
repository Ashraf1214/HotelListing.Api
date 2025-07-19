using HotelListing.Api.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;

namespace HotelListing.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        //There is a pipeline in the middleware,
        //which is a series of components that process requests and responses in an ASP.NET Core application.
        //Below, 'next' refers to the next component of the pipeline that will handle the request ,i.e, HttpContext.
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        //Custom method
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //passes the request to the next middleware component in the pipeline
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //Logs the error message with the request path, i.e., the URL that caused the exception
                _logger.LogError($"Something wen wrong while accessing: {httpContext.Request.Path}");
                //calls another custom method
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //The response is set to JSON format
            httpContext.Response.ContentType = "application/json";
            //And the status code is set to 500 (Internal Server Error) by default.
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            // Setting the properties of this custom class "ErrorDetails" instance
            var errorDetails = new ErrorDetails
            {
                Type = "Failure",
                Message = ex.Message
            };  


            switch (ex)
            {
                //Based on the throw exceptions defined in other places of the application,
                //the status code and error details are set accordingly.
                case NotFoundException notFoundException:
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                    errorDetails.Type = "Not Found";
                    break;
                //case BadRequestException badRequestException:
                //    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                //    errorDetails.Type = "Bad Request";
                //    errorDetails.Message = badRequestException.Message;
                //    break;
                default:
                    break;
            }
            // The error details are serialized to JSON format using Newtonsoft.Json library.
            string response = JsonConvert.SerializeObject(errorDetails);
            // The response is written back to the client with the status code set above.
            httpContext.Response.StatusCode = (int)statusCode;
            //The response is written back to the client in JSON format.
            return httpContext.Response.WriteAsync(response);
        }

        public class ErrorDetails
        {
            public string Type { get; set; }
            public string Message { get; set; }
        }
    }
}
