using CentralErros.Domain.Repositories;
using CentralErros.Domain.Models;
using System.Linq;

namespace CentralErros.Infrastructure.Repositories
{
    public class LevelRepository : BaseRepository<Level>, ILevelRepository
    {
        public LevelRepository(CentralErrosContext context) : base(context)
        {
        }
    }
}
