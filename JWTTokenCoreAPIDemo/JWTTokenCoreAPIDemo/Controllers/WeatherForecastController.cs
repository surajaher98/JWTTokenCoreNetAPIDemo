﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTTokenCoreAPIDemo.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JWTTokenCoreAPIDemo.Controllers
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
    [Authorize(Roles = Role.User)]
    [HttpGet]
    [Route("userNames")]
    public List<string> UserNames()
    {
      return new List<string>  {
            "Admin", "Admin1", "Admin2", "Admin3", "Admin3", "Admin4", "Admin5" };
    }


    [Authorize(Roles = Role.Admin)]
    [HttpGet]
    [Route("adminNames")]
    public List<string> AdminNames()
    {

      return new List<string>  {
            "Admin", "Admin1", "Admin2", "Admin3", "Admin3", "Admin4", "Admin5" };
    }
  }
}
