using Donkey.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Infrastructure.ErrorHandlingMiddleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next,ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DonkeyException ex)
            {
                _logger.LogWarning($"HANDLED EXCEPTION THROWN: CODE - {ex.StatusCode} - {ex.Message}");

            }
            catch (InvalidOperationException ex)
            {
                _logger.LogInformation($"UNAUTHORIZED OPERATION: CODE - {HttpStatusCode.Unauthorized} - {ex.Message}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"!UNHANDLED EXCEPTION THROWN: CODE - 500 - {ex.Message}");
            }
        }

        private async Task WriteExceptionAsync(HttpContext context, ErrorDetails details)
        {
            ResponseDetails model = new ResponseDetails();
            model.ExceptionMessage = details.ExceptionMessage;
            model.StatusCode = details.StatusCode;
            string error = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            context.Response.StatusCode = (int)details.StatusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(error);
        }
    }
}
