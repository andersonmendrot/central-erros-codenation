using CentralErros.Domain.Interfaces;
using CentralErros.Domain.Models;
using CentralErros.Domain.Models.Authentication;
using System.Threading.Tasks;

namespace CentralErros.Domain.Repositories
{
    public interface IAuthorizationService
    {
        Task<BaseResult<IUser>> AuthorizeAsync(
            LoginUser loginUser);
    }
}
