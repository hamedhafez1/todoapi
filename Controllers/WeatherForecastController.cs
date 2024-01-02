using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    [HttpGet("{cityName}")]
    public async Task<ActionResult<WeatherForecast?>> GetWeatherForecast(string cityName)
    {
        try
        {
            if (!string.IsNullOrEmpty(cityName))
            {
                var client = new HttpClient();
                // var request = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.openweathermap.org/data/2.5/weather?units=metric&lang=fa&q={0}&appid=a9bc40e37b9e8727e004e89d553ecb84", city));
                var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?units=metric&lang=fa&q={cityName}&appid=a9bc40e37b9e8727e004e89d553ecb84");
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WeatherForecast>(jsonString);
            }
            else
            {
                return BadRequest();
            }
        }
        catch
        {
            return StatusCode(500);
        }

    }
}
