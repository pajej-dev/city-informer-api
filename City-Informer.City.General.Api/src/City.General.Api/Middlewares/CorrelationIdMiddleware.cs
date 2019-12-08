using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using City.General.Api.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace City.General.Api.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger logger;

        public CorrelationIdMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger(nameof(CorrelationIdMiddleware));
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            if(!httpContext.Request.Headers.TryGetValue(Headers.CorrelationId, out StringValues headerValues))
            {
                headerValues = Guid.NewGuid().ToString();
                this.logger.LogDebug($"Created new correlation Id: { headerValues }");
            }
            
            httpContext.Response.Headers.Add(Headers.CorrelationId, headerValues);

            this.logger.LogDebug($"Passed out correlation Id : { headerValues }");

            return _next(httpContext);
        }
    }
}