using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{cityName}")]
    public async Task<ActionResult<WeatherForecast?>> GetWeatherForecast(string cityName)
    {
        try
        {
            var client = new HttpClient();
            // var request = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.openweathermap.org/data/2.5/weather?units=metric&lang=fa&q={0}&appid=a9bc40e37b9e8727e004e89d553ecb84", city));
            var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?units=metric&lang=fa&q={cityName}&appid=a9bc40e37b9e8727e004e89d553ecb84");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WeatherForecast>(jsonString);
        }
        catch
        {
            throw;
        }

    }
}
