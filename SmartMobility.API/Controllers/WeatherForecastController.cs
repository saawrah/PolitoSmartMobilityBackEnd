using Common.Tool.JsonTools;
using MathNet.Numerics.Distributions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SmartMobility.API.Data;
using SmartMobility.API.DTOs;
using SmartMobility.API.Models;
using SmartMobility.API.Tools;
using System.Linq;

namespace SmartMobility.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        MongoDbContext _mcontext;


        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, MongoDbContext context)
        {
            _logger = logger;
            _mcontext = context;

        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<string> Get()
        {
            return "HI!!";
        }


    }
}