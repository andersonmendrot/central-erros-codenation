using CentralErros.Domain.Models;

namespace CentralErros.Domain.Repositories
{
    public interface IAuthenticationRepository
    {
        AuthenticationResult Authenticate(User user);
    }
}
