using JWTTokenCoreAPIDemo.Constants;
using JWTTokenCoreAPIDemo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTTokenCoreAPIDemo.Helper
{
  public class TokenManager
  {
    public static AuthManager GenerateToken(UserDetails userDetails, dynamic config)
    {

      List<Claim> authClaims = new List<Claim>
      {
        new Claim("User", JsonConvert.SerializeObject(userDetails)),
        new Claim(ClaimTypes.Role, Role.Admin)
      };

     
      var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecretKey"]));

      var token = new JwtSecurityToken(
        issuer: config["JWT:ValidIssuer"],
        audience: config["JWT:ValidAudience"],
        expires: DateTime.Now.AddMinutes(1),
        claims: authClaims,
        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
      string accessToken = new JwtSecurityTokenHandler().WriteToken(token);
      string refreshToken = RefreshTokenManager.GenerateRefreshToken();
      // AddAccessAndRefreshToken(); //Add refreshAnd Refresh Token to DB

      return new AuthManager
      {
        AccessToken = accessToken,
        RefreshToken = refreshToken,
        ExpiryTime = token.ValidTo,
      }; ;
    }
  }
}
