using System.Net;

namespace Donkey.Infrastructure.ErrorHandlingMiddleware
{
    public class ErrorDetails : Exception
    {
        public string ExceptionMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}