using System;
using System.Collections.Generic;
using CentralErros.Domain.Repositories;
using System.Linq;

namespace CentralErros.Infrastructure.Repositories
{
    public class BaseRepository<T>: IBaseRepository<T> where T: class, IEntity
    {
        protected readonly CentralErrosContext _context;
        public BaseRepository(CentralErrosContext context)
        {
            _context = context;
        }
        public void Save(T entity)
        {
            var existingEntity = _context.Set<T>().Find(entity.Id);

            if(existingEntity != null)
            {
                throw new ArgumentException("Item já existente");
            }

            else
            {
                _context.Set<T>().Add(entity);
            }

            _context.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Remove(int id)
        {
            var entity = _context.Set<T>().FirstOrDefault(x => x.Id == id);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
