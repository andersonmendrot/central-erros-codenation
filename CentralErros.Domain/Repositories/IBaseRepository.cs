using System;
using System.Collections.Generic;

namespace CentralErros.Domain.Repositories
{
    public interface IBaseRepository<T> : IDisposable where T: class, IEntity
    {
        void Save(T entity);
        IList<T> GetAll();
        T Remove(int id);
        T GetById(int id);
    }
}
