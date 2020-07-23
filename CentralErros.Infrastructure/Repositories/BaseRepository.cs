using System;
using System.Collections.Generic;
using CentralErros.Domain.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            _context.Set<T>().Add(entity);
            _context.SaveChanges();        
        }

        public IList<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().SingleOrDefault(x => x.Id == id);
        }

        public T Remove(int id)
        {
            var entity = _context.Set<T>().Single(x => x.Id == id);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
