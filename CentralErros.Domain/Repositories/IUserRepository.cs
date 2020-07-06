using CentralErros.Domain.Models;

namespace CentralErros.Domain.Repositories
{
    public interface IUserRepository
    {
        void Save(User user);
    }
}
