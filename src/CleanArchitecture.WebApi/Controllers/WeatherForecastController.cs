using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using CleanArchitecture.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.WebApi.Controllers
{
    public class WeatherForecastController : ApiController
    {
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await Mediator.Send(new GetWeatherForecastsQuery());
        }
    }
}
