﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTTokenCoreAPIDemo.Models
{
  public class LoginRequest
  {
    public string UserName { get; set; }
    public string Password { get; set; }
  }
}
