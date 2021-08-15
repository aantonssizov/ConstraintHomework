using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstraintHomework.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{customParam:int:range(5, 10)}")]
        public IEnumerable<WeatherForecast> Get(int customParam)
        {
            var rng = new Random();
            return Enumerable.Range(1, customParam).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("{min:float:min(-20)}/{max:decimal:max(50)}")]
        public IEnumerable<WeatherForecast> Get(float min, decimal max)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next((int)min, (int) max),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("{summary:alpha:minlength(3):maxlength(12)}")]
        public IEnumerable<WeatherForecast> Get(string summary)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 50),
                Summary = summary
            })
            .ToArray();
        }

        [HttpGet]
        [Route("{summary:alpha:length(8):regex(^[[a-z]]+$)}/{date:datetime}")]
        public IEnumerable<WeatherForecast> Get(string summary, DateTime date)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = date.AddDays(index),
                TemperatureC = rng.Next(-20, 50),
                Summary = summary
            })
            .ToArray();
        }

        [HttpGet]
        [Route("{isWinter:bool}/{maxTemp:double}")]
        public IEnumerable<WeatherForecast> Get(bool isWinter, double maxTemp)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next((isWinter) ? -20 : 10, (isWinter) ? (int) maxTemp / 10 : (int) maxTemp),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("{guid:guid}")]
        public string Get(Guid guid)
        {
            return guid.ToString();
        }
    }
}
