using CentralErros.Domain.Models;
using CentralErros.Domain.Repositories;

namespace CentralErros.Infrastructure.Repositories
{
    public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(CentralErrosContext context) : base(context)
        {
        }
    }
}
