using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using City.General.Api.Models;
using System.Net;
using System;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Logging;
using City.Common.Providers;
using City.Common.Dictionaries;

namespace City.General.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityInformationController : ControllerBase
    {
        private HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly ICorrelationProvider correlationProvider;
        private readonly ILogger logger;

        public CityInformationController(
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration, 
            ILoggerFactory loggerFactory,
            ICorrelationProvider correlationProvider )
        {
            this.logger = loggerFactory.CreateLogger(nameof(CityInformationController));
            this.configuration = configuration;
            this.correlationProvider = correlationProvider;
            this.httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<CityInformation>> Get([FromRoute]string name)
        {
            //Todo - real scenario => get city Id from database by name
            string weatherApiUrl =  this.configuration.GetSection("WeatherApiUrl").Value + "/" + name;

            this.httpClient.DefaultRequestHeaders.Add(Headers.CorrelationId, correlationProvider.CorrelationId);
            var weather = await this.httpClient.GetStringAsync(weatherApiUrl);
            
            var actualWeather = JsonConvert.DeserializeObject<WeatherForecast>(weather);

            this.logger.LogInformation($"{ System.DateTime.UtcNow} - Fetched data from : { weatherApiUrl }. "
                 +	$"| CorrelationId: { correlationProvider.CorrelationId }");

            var rand = new Random();

            var cityInfo = new CityInformation
            {
                Name = name,
                Population = rand.Next(100000,2500000),
                DateOfBuild = new System.DateTime(rand.Next(1600,2000), 12, 1),
                ActualWeather = actualWeather
            };

            this.logger.LogInformation($"{ System.DateTime.UtcNow} - Returned information about: { name }. "
                +	$"| CorrelationId: { correlationProvider.CorrelationId }");


            return Ok(cityInfo);
        }

    }
}