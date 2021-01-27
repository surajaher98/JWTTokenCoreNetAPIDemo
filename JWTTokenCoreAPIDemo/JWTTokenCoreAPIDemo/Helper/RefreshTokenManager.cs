using JWTTokenCoreAPIDemo.Models;
using JWTTokenCoreAPIDemo.Services;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JWTTokenCoreAPIDemo.Helper
{
  public class RefreshTokenManager
  {

    public static AuthManager RefreshToken(string accessToken, string refreshToken, dynamic config, IUserService userService)
    {
      ClaimsPrincipal principal = GetPrincipalFromExpiredToken(accessToken, config["JWT:SecretKey"]);
      var userDetails = JsonConvert.DeserializeObject<UserDetails>(principal.Claims.FirstOrDefault(claim => claim.Type == "User").Value); // ; // 
      if (userDetails != null)
      {
        // CheckAccessAndRefreshTokenIsPresent in DB against that User
        if (userService.CheckTokensExist(userDetails.UserId, accessToken, refreshToken))
        {
          // DeleteAccessAndRefreshToken from DB
        if (userService.CheckTokensExist(userDetails.UserId, accessToken, refreshToken))
          userService.DeleteToken(userDetails.UserId, accessToken, refreshToken);
          return TokenManager.GenerateToken(userDetails, config, userService);
        }
      }
      return null;
    }


    private static ClaimsPrincipal GetPrincipalFromExpiredToken(string token, string secretKey)
    {
      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      SecurityToken securityToken;
      var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
      var jwtSecurityToken = securityToken as JwtSecurityToken;
      if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        throw new SecurityTokenException("Invalid token");

      return principal;
    }

    public static string GenerateRefreshToken()
    {
      var randomNumber = new byte[32];
      using (var rng = RandomNumberGenerator.Create())
      {
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
      }
    }
  }
}
