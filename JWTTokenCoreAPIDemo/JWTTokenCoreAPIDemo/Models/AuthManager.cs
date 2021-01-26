using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTTokenCoreAPIDemo.Models
{
  public class AuthManager
  {
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiryTime { get; set; }
  }
}
