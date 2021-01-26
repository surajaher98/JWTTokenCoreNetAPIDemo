using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTTokenCoreAPIDemo.Models
{
  public class UserDetails
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string[] Roles { get; set; }
  }
}
