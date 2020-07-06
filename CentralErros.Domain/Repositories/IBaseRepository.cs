using System;
using System.Collections.Generic;

namespace CentralErros.Domain.Repositories
{
    public interface IBaseRepository<T> : IDisposable where T: class, IEntity
    {
        void Save(T entity);
        List<T> GetAll();
        void Remove(int id);
    }
}
