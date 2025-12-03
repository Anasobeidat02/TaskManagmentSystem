using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.DTOs.User
{
    public class LoginResponse
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Token { get; set; }
    }
}
