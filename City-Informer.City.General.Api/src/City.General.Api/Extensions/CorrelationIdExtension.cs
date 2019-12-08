using System;
using City.General.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace City.General.Api.Extensions
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