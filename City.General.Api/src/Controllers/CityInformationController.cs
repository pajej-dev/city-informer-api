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

namespace City.General.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityInformationController : ControllerBase
    {
        private HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public CityInformationController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger(nameof(CityInformationController));
            this.configuration = configuration;
            this.httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<CityInformation>> Get([FromRoute]string name)
        {
            //Todo - real scenario => get city Id from database by name
            string weatherApiUrl =  this.configuration.GetSection("WeatherApiUrl").Value + "/1";

            var weather = await this.httpClient.GetStringAsync(weatherApiUrl);
            var actualWeather = JsonConvert.DeserializeObject<WeatherForecast>(weather);

            this.logger.LogInformation($"{ System.DateTime.UtcNow} - Sucessfully fetched data from : { weatherApiUrl } ");

            var rand = new Random();

            var cityInfo = new CityInformation
            {
                Name = name,
                Population = rand.Next(100000,2500000),
                DateOfBuild = new System.DateTime(rand.Next(1600,2000), 12, 1),
                ActualWeather = actualWeather
            };

            this.logger.LogInformation($"{ System.DateTime.UtcNow} - Sucessfully returned information about: { name }. ");

            return Ok(cityInfo);
        }

    }
}