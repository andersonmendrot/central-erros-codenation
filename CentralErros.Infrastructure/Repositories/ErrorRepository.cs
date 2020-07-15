using System;
using System.Collections.Generic;
using CentralErros.Domain.Repositories;
using CentralErros.Domain.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CentralErros.Infrastructure.Repositories
{
    public class ErrorRepository : IErrorRepository
    {
        private readonly CentralErrosContext _context;
        public ErrorRepository(CentralErrosContext context)
        {
            _context = context;
        }

        public void ChangeStatus(Error error)
        {
            var existingError = _context.Errors.Find(error.Id);
            existingError.Status = existingError.Status == 'y' ? 'n' : 'y';
            _context.Entry(existingError).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Error> FindByApplicationLayerId(int applicationLayerId)
        {
            return _context.Errors.
                Where(x => x.ApplicationLayerId == applicationLayerId).
                ToList();
        }

        public List<Error> FindByEnvironmentId(int environmentId)
        { 
            return _context.Errors.
                Where(x => x.EnvironmentId == environmentId).
                ToList();
        }

        public List<Error> FindByLanguageId(int languageId)
        {
            return _context.Errors.
                Where(x => x.LanguageId == languageId).
                ToList();
        }

        public List<Error> FindByLevelId(int levelId)
        {
            return _context.Errors.
                Where(x => x.LevelId == levelId).
                ToList();
        }

        public List<Error> FindByStatus(char status)
        {
            return _context.Errors.
                Where(x => x.Status == status).
                ToList();
        }

        public List<Error> GetAll()
        {
            return _context.Errors.ToList();
        }

        public Error GetById(int id)
        {
            return _context.Errors.SingleOrDefault(x => x.Id == id);
        }

        public List<Error> OrderByLevel(string sortdir)
        {
            if (sortdir == "descending")
            {
                return _context.Errors.OrderByDescending(x => x.LevelId).ToList();
            }

            return _context.Errors.OrderBy(x => x.LevelId).ToList();
        }

        public List<Error> OrderByQuantity(string sortdir)
        {
            if (sortdir == "descending")
            {
                return _context.Errors.OrderByDescending(x => x.NumberEvents).ToList();
            }

            return _context.Errors.OrderBy(x => x.NumberEvents).ToList();
        }

        public List<Error> OrderByStatus(string sortdir)
        {
            if (sortdir == "descending")
            {
                return _context.Errors.OrderByDescending(x => x.Status).ToList();
            }

            return _context.Errors.OrderBy(x => x.Status).ToList();
        }

        public void Remove(Error error)
        {
            var existingError = _context.Errors.Find(error.Id);
            _context.Remove(existingError);
            _context.SaveChanges();
        }

        public void Save(Error error)
        {
            _context.Errors.Add(error);
            _context.SaveChanges();
        }

        public void Update(Error error)
        {
            var entity = _context.Errors.Find(error.Id);

            if(entity != null)
            {
                var attachedEntry = _context.Entry(entity);
                attachedEntry.CurrentValues.SetValues(error);
            }
        }

        public List<Error> LimitResultNumber(List<Error> errors, int limit)
        {
            return errors.Take(limit).ToList();
        }
    }
}
