using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Infrastructure.ErrorHandlingMiddleware
{
    public class ResponseDetails
    {
        public string ExceptionMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
