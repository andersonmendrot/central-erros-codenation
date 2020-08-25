using CentralErros.Domain.Models;
using CentralErros.Domain.Repositories;
using System.Linq;

namespace CentralErros.Infrastructure.Repositories
{
    public class ApplicationLayerRepository : BaseRepository<ApplicationLayer>, IApplicationLayerRepository
    {
        public ApplicationLayerRepository(CentralErrosContext context) : base(context)
        {
        }
    }
}
