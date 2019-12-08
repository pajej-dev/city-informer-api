using System;
using City.Common.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace City.Common.Extensions
{
    public static class CorrelationIdExtension
    {
        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<CorrelationIdMiddleware>();
            
            return builder;
        }
    }
}