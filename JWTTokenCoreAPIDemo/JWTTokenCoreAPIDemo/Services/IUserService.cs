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

  }
}
