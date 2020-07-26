using CentralErros.Domain.Interfaces;
using CentralErros.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralErros.Domain.Repositories
{
    public interface ILoggedUserService
    {
        BaseResult<T> GetLoggedUser<T>()
            where T : class, IUser;
    }
}
