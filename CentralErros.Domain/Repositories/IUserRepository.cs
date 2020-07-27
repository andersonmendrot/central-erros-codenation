﻿using CentralErros.Domain.Models;

namespace CentralErros.Domain.Repositories
{
    public interface IUserRepository
    {
        void Save(User user);
        User GetByEmail(string email);
        BaseResult<User> Authorize(User user);
    }
}
