using System;
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

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
      _logger = logger;
    }
    [Authorize(Roles = Role.User)]
    [HttpGet]
    [Route("getUserNames")]
    public List<string> GetUserNames()
    {
      return new List<string>  {
            "user1", "user2", "user3", "user4", "user5", "user6", "user7" };
    }


    [Authorize(Roles = Role.Admin)]
    [HttpGet]
    [Route("GetAdminNames")]
    public List<string> GetAdminNames()
    {
      return new List<string>  {
            "Admin", "Admin1", "Admin2", "Admin3", "Admin3", "Admin4", "Admin5" };
    }
  }
}
