using CentralErros.Domain.Interfaces;
using CentralErros.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CentralErros.Domain.Repositories
{
    public interface IAuthenticationService
    {
        AuthenticationResult Authenticate(
            IUser user);
    }
}
