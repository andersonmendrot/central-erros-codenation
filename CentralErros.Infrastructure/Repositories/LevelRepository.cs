using CentralErros.Domain.Repositories;
using CentralErros.Domain.Models;

namespace CentralErros.Infrastructure.Repositories
{
    public class LevelRepository : BaseRepository<Level>, ILevelRepository
    {
        public LevelRepository(CentralErrosContext context) : base(context)
        {
        }
    }
}
