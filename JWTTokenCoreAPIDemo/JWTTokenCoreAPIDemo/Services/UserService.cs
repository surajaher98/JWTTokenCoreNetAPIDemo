using JWTTokenCoreAPIDemo.Helper;
using JWTTokenCoreAPIDemo.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace JWTTokenCoreAPIDemo.Services
{
  public class UserService : IUserService
  {
    private readonly IConfiguration _config;
    public UserService(IConfiguration config)
    {
      _config = config;
    }

    public UserDetails GetUserDetails(LoginRequest req)
    {
      Dictionary<string, object> cond = new Dictionary<string, object>
      {
        {"UserName",req.UserName},
        {"Password",req.Password}
      };
      string jsonString = Common.GetJsonString(Common.GetSQLQueryFromFile("SQL", "ValidateUserCredentials.sql"), _config.GetConnectionString("DefaultConnection"), cond);

      if (!string.IsNullOrEmpty(jsonString))
      {
        return JsonConvert.DeserializeObject<List<UserDetails>>(jsonString).FirstOrDefault();
      }
      return null;
    }


    public bool AddTokenInfo(int userId, string accessToken, string refreshToken, DateTime expiryTime)
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>
      {
        {"UserId", userId},
        {"AccessToken", accessToken },
        {"RefreshToken", refreshToken },
        {"ExpiryTime",expiryTime}
      };
      Common.ExecuteQueryAndGetDataTable(Common.GetSQLQueryFromFile("SQL", "AddTokenInfo.sql"), _config.GetConnectionString("DefaultConnection"), parameters);
      return true;
    }

    public bool CheckTokensExist(int userId, string accessToken, string refreshToken)
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>
      {
        {"UserId", userId},
        {"AccessToken", accessToken },
        {"RefreshToken", refreshToken },
      };
      DataTable dt = Common.ExecuteQueryAndGetDataTable(Common.GetSQLQueryFromFile("SQL", "CheckTokensExist.sql"), _config.GetConnectionString("DefaultConnection"), parameters);
      if (dt.Rows.Count > 0) return true;
      return false;
    }


    public bool DeleteToken(int userId, string accessToken, string refreshToken)
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>
      {
        {"UserId", userId},
        {"AccessToken", accessToken },
        {"RefreshToken", refreshToken },
      };
      Common.ExecuteQueryAndGetDataTable(Common.GetSQLQueryFromFile("SQL", "DeleteToken.sql"), _config.GetConnectionString("DefaultConnection"), parameters);
      return true;
    }
  }
}
