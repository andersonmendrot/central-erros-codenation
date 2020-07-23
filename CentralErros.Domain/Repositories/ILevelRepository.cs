using CentralErros.Domain.Models;

namespace CentralErros.Domain.Repositories
{
    public interface ILevelRepository : IBaseRepository<Level>
    {
        bool HasErrorsWithLevelId(int id);
    }
}
