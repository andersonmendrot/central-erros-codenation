using System;
using Environment = CentralErros.Domain.Models.Environment;
using CentralErros.Domain.Repositories;

namespace CentralErros.Infrastructure.Repositories
{
    public class EnvironmentRepository : BaseRepository<Environment>, IEnvironmentRepository 
    {
        public EnvironmentRepository(CentralErrosContext context) : base(context)
        {
        }
    }
}
