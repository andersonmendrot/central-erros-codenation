using CentralErros.Domain.Models;
using System.Collections.Generic;
using CentralErros.Domain.Repositories;

namespace CentralErros.Infrastructure.Repositories
{
    public class ApplicationLayerRepository : BaseRepository<ApplicationLayer>
    {
        public ApplicationLayerRepository(CentralErrosContext context) : base(context)
        {
        }

    }
}
