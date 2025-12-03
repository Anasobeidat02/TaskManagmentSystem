using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains.DTOs.User;

public class AddUserRequest
{ 
    public string FullName { get; set; }
     
    public string EmailAddress { get; set; }
     
    public string Password { get; set; }
}
