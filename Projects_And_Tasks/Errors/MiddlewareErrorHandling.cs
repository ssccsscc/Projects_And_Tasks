using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Projects_And_Tasks
{
    public class MiddlewareErrorHandling
    {
        private readonly RequestDelegate _next;
        public MiddlewareErrorHandling(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                ApiError problem = new ApiError();
                switch (error)
                {
                    case LogicExceptionNotFound e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        problem.status = 404;
                        problem.title = e.Message;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        problem.status = 500;
                        problem.title = "Internal error";
                        break;
                }
                var result = JsonSerializer.Serialize(problem);
                await response.WriteAsync(result);
            }
        }
    }
}
