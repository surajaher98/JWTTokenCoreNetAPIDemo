using JWTTokenCoreAPIDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTTokenCoreAPIDemo.Services
{
  public interface IUserService
  {
    UserDetails GetUserDetails(LoginRequest req);
    bool AddTokenInfo(int userId, string accessToken, string refreshToken, DateTime expiryTime);
    bool CheckTokensExist(int userId, string accessToken, string refreshToken);
    bool DeleteToken(int userId, string accessToken, string refreshToken);
  }
}
