using CentralErros.Domain.Repositories;
using CentralErros.Domain.Models;

namespace CentralErros.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CentralErrosContext context) : base(context)
        {
        }
    }
}
