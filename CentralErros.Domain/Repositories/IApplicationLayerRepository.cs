using CentralErros.Domain.Models; 

namespace CentralErros.Domain.Repositories
{
    public interface IApplicationLayerRepository : IBaseRepository<ApplicationLayer>
    {
        int HasErrorWithApplicationLayerId(int id);
    }
}
