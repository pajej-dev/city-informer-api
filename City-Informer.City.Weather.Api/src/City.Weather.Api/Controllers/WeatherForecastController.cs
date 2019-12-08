using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using City.Common.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace City.Weather.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration configuration;
        private readonly ICorrelationProvider correlationProvider;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger, 
            IConfiguration configuration,
            ICorrelationProvider correlationProvider)
        {
            this.configuration = configuration;
            this.correlationProvider = correlationProvider;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();

            this._logger.LogInformation($"Returned random weather informations."
                + $"| CorrelationId: { correlationProvider.CorrelationId }");

            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.UtcNow,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }));
        }

        [HttpGet("{name}")]
        public ActionResult<WeatherForecast> GetById([FromRoute]string name)
        {
            var rng = new Random();

            this._logger.LogInformation($"Returned random weather informations for City: { name }."
                + $"| CorrelationId: { correlationProvider.CorrelationId }");

            return Ok(new WeatherForecast
            {
                Date = DateTime.UtcNow,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }
    }
}
