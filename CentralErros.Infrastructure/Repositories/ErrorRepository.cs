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

        public List<Error> FindByApplicationLayer(string applicationLayerName)
        {
            var applicationLayerId = _context.ApplicationLayers.
                Where(x => x.Name == applicationLayerName).
                Select(x => x.Id).
                FirstOrDefault();

            return _context.Errors.
                Where(x => x.ApplicationLayerId == applicationLayerId).
                Select(x => x).
                ToList();
        }

        public List<Error> FindByEnvironment(string environmentName)
        { 
            var environmentNameId = _context.Environments.
                Where(x => x.Name == environmentName).
                Select(x => x.Id).
                FirstOrDefault();

            return _context.Errors.
                Where(x => x.EnvironmentId == environmentNameId).
                Select(x => x).
                ToList();
        }

        public List<Error> FindByLevel(string levelName)
        {
            var levelNameId = _context.Levels.
                Where(x => x.Name == levelName).
                Select(x => x.Id).
                FirstOrDefault();

            return _context.Errors.
                Where(x => x.LevelId == levelNameId).
                Select(x => x).
                ToList();
        }

        public List<Error> FindByStatus(char status)
        {
            return _context.Errors.
                Where(x => x.Status == status).
                Select(x => x).
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

        public List<Error> OrderByApplicationLayer()
        {
            return _context.Errors.
                OrderBy(x => x.ApplicationLayerId).
                ToList();
        }

        public List<Error> OrderByEnvironment()
        {
            return _context.Errors.
                OrderBy(x => x.EnvironmentId).
                ToList();
        }

        public List<Error> OrderByLevel()
        {
            return _context.Errors.
                OrderBy(x => x.LevelId).
                ToList();
        }

        public List<Error> OrderByQuantity()
        {
            return _context.Errors.
                OrderBy(x => x.NumberEvents).
                ToList();
        }

        public List<Error> OrderByStatus()
        {
            return _context.Errors.
                OrderBy(x => x.Status).
                ToList();
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
            _context.Errors.Update(error);
            _context.SaveChanges();
        }
    }
}
