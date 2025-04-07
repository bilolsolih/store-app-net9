using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreApp.Core.Exceptions;

namespace StoreApp.Core;

public class CoreExceptionFilters : IExceptionFilter
{
  public void OnException(ExceptionContext context)
  {
    var message = new StringBuilder();
    var statusCode = 400;
    switch (context.Exception)
    {
      case AlreadyExistsException exc:
        message.Append($"You are entering a duplicate value for a unique column. {exc.Message}");
        statusCode = 409;
        context.Result = new ObjectResult(message.ToString()) { StatusCode = statusCode };
        break;
      case DoesNotExistException exc:
        message.Append($"Object with the given credentials does not exist. {exc.Message}");
        statusCode = 404;
        context.Result = new ObjectResult(message.ToString()) { StatusCode = statusCode };
        break;
      case OtpCodeExpiredException exc:
        message.Append($"Otp Code has expired. {exc.Message}");
        statusCode = 400;
        context.Result = new ObjectResult(message.ToString()) { StatusCode = statusCode };
        break;
    }
  }
}