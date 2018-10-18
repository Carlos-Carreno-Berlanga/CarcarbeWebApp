using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarcarbeWebApp.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Rebus.Bus;

namespace CarcarbeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly IBus _bus;
        private readonly IDistributedCache _distributedCache;
        public SampleDataController(IBus bus,
            IDistributedCache distributedCache
            )
        {
            _bus = bus;
            _distributedCache = distributedCache;
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts(int startDateIndex)
        {
            var cacheKey = "TheTime";
            string  counter = _distributedCache.GetString(cacheKey);
            int counterValue = 0;
            if (string.IsNullOrEmpty(counter))
            {
                counter = "0";
            }
            else
            {
                //existingTime = DateTime.UtcNow.ToString();
                //_distributedCache.SetString(cacheKey, existingTime);
                //"Added to cache : " + existingTime;
                counterValue = Convert.ToInt32(counter);
                counterValue++;
            }
            _distributedCache.SetString(cacheKey, counterValue.ToString());
            //***********
            _bus.Send(new Message1());
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index + startDateIndex).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
