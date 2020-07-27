using CentralErros.Domain.Models;

namespace CentralErros.Domain.Repositories
{
    public interface IAuthorizationRepository
    {
        BaseResult<User> Authorize(User user);
    }
}
