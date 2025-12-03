using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.DTOs.User
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string EmailAddress { get; set; } 
    }
}
