using Microsoft.AspNetCore.Http;
using City.Common.Dictionaries;

namespace City.Common.Providers
{
    public class CorrelationProvider : ICorrelationProvider
    {
        private string _correlationId;
        public string CorrelationId => _correlationId ;

        public CorrelationProvider(IHttpContextAccessor httpContextAccessor)
        {
            this._correlationId = httpContextAccessor.HttpContext.Response.Headers[Headers.CorrelationId];
        }

    }
}