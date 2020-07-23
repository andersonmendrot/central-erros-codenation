using CentralErros.Domain.Models; 

namespace CentralErros.Domain.Repositories
{
    public interface IApplicationLayerRepository : IBaseRepository<ApplicationLayer>
    {
        bool HasErrorWithoutApplicationLayerId(int id);
    }
}
