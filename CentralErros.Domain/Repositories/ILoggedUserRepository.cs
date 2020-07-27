using CentralErros.Domain.Models;

namespace CentralErros.Domain.Repositories
{
    public interface ILoggedUserRepository
    {
        BaseResult<UserLogged> GetLoggedUser();
    }
}
