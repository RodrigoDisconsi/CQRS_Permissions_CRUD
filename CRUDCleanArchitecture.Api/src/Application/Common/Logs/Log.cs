using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDCleanArchitecture.Application.Common.Exceptions;

namespace CRUDCleanArchitecture.Application.Common.Logs;
public static class Log
{
    public static string FormatError(this Exception exception, string headingInfo = "Error")
    {
        var sb = new StringBuilder();
        sb.Append(headingInfo);
        sb.Append(BuildExceptionMessage(exception));
        return sb.ToString();
    }
    private static string BuildExceptionMessage(Exception exception)
    {
        var sb = new StringBuilder();
        sb.Append($"Mensaje: {exception.Message}\n");
        sb.Append($"StackTrace: {exception.StackTrace}\n");
        if (exception.InnerException != null)
        {
            sb.Append(BuildExceptionMessage(exception.InnerException));
        }
        if (exception is ValidationException)
        {
            sb.Append($"Errors:\n");

            var errors = (exception as ValidationException).Errors;

            foreach (var key in errors.Keys) foreach (var error in errors[key]) sb.Append($"{key}: {error}");
        }
        return sb.ToString();
    }
}
