﻿using NotesApp.Models.DTO;
using System.Data.SqlTypes;
using System.Net;

namespace NotesApp.Models.Middleware
{
    public class ExceptionHandleMiddleware
    {
        private readonly ILogger<ExceptionHandleMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandleMiddleware(
            ILogger<ExceptionHandleMiddleware> logger,
            RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (SqlTypeException ex)
            {
                await HandleExceptionAsync(
                    httpContext,
                    ex.Message,
                    HttpStatusCode.InternalServerError,
                    "Error with data base");
            }
            catch ()
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            string exceptionMessage,
            HttpStatusCode httpStatusCode,
            string message)
        {
            _logger.LogError(exceptionMessage);

            HttpResponse response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatusCode;

            ErrorDto errorDto = new()
            {
                Message = message,
                StatusCode = (int)httpStatusCode
            };

            await response.WriteAsJsonAsync(errorDto);
        }
    }
}
