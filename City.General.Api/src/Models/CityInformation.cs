namespace City.General.Api.Models
{
    public class CityInformation
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public System.DateTime DateOfBuild { get; set; }
        public WeatherForecast ActualWeather { get; set; }
    }
}