using System;
using System.Collections.Generic;
using System.Text;

namespace CentralErros.Domain.Models.Authentication
{
    public sealed class LoginUser
    {
        public string LoginOrEmail { get; set; }
        public string Password { get; set; }
    }
}
