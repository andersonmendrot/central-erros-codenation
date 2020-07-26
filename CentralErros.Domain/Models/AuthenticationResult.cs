using System;
using System.Collections.Generic;
using System.Text;

namespace CentralErros.Domain.Models
{
    public sealed class AuthenticationResult : BaseResult<object>
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
    }
}
