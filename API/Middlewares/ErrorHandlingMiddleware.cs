using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model;
using Domain.Exceptions;

namespace API.Middlewares
{
  public class ErrorHandlingMiddleware : IMiddleware
  {

    // private readonly ILogger<ErrorHandlingMiddleware> _logger;

    // public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    // {
    //   _logger = logger;
    // }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      try
      {
        await next(context);
      }
      catch (NotFoundException)
      {
        var response = ApiResponse<object>.NotFound();
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(response);
      }
      catch (Exception ex)
      {
        // _logger.LogError(ex, ex.Message);
        var response = ApiResponse<object>.Error(message: "Something went wrong", statusCode: 500);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(response);
      }
    }
  }
}