using Hellang.Middleware.ProblemDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeYourRestaurant.Platform.Core.API
{
    public static class ProblemDetailsOptionsExtensions
    {
        public static void MapException<TException>(this ProblemDetailsOptions options, int statusCode) where TException : Exception
        {
            options.Map<TException>(exception =>
            {
                var error = StatusCodeProblemDetails.Create(statusCode);
                error.Type = exception.GetType().Name;
                error.Detail = exception.Message;
                return error;
            });
        }
    }
}
