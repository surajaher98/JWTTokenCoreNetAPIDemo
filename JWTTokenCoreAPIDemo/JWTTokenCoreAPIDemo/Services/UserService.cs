using JWTTokenCoreAPIDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTTokenCoreAPIDemo.Services
{
  public class UserService : IUserService
  {
    public UserDetails GetUserDetails(LoginRequest req)
    {
      if (req.UserName == "john" && req.Password =="cina")
      {
        return new UserDetails
        {
          FirstName = "john",
          LastName = "cina",
          Roles = new string[2] { "admin", "user" }
        };
      }
      return null;
     
    }
  }
}
