using CentralErros.Domain.Models;
using System.Collections.Generic;

namespace CentralErros.Domain.Repositories
{
    public interface IUserRepository
    {
        void Save(User user);
        User GetByEmail(string email);
        List<User> GetAll();
        User GetById(int id);
        BaseResult<User> Authorize(User user);
    }
}
