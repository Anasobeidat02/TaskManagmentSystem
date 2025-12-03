using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.DTOs.User
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
