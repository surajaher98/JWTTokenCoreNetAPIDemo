using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTTokenCoreAPIDemo.Helper;
using JWTTokenCoreAPIDemo.Models;
using JWTTokenCoreAPIDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JWTTokenCoreAPIDemo.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IConfiguration _config;
    private readonly IUserService _userService;

    public UserController(IConfiguration config, IUserService userService)
    {
      _config = config;
      _userService = userService;
    }


    [HttpPost]
    [Route("token")]
    public IActionResult Token(LoginRequest req)
    {
     UserDetails user =  _userService.GetUserDetails(req);
      if (user != null)
      {
        var result = TokenManager.GenerateToken(user, _config, _userService);
        if (result == null) return Unauthorized();
        return Ok(result);
      }
      return Unauthorized();
    }

    [HttpPost]
    [Route("token/refreshToken")]
    public IActionResult RefreshToken(RefreshTokenRequest req)
    {
        var result =  RefreshTokenManager.RefreshToken(req.AccessToken, req.RefreshToken, _config, _userService);
      if (result == null) return Unauthorized(); 
      return Ok(result);
    }

  }
}
