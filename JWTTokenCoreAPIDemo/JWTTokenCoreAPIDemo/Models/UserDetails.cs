using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTTokenCoreAPIDemo.Models
{
  public class UserDetails
  {
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Roles { get; set; }
  }
}
