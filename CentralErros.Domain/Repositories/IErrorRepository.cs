using System.Collections.Generic;
using CentralErros.Domain.Models;

namespace CentralErros.Domain.Repositories
{
    public interface IErrorRepository
    {
        public List<Error> OrderByLevel();
        public List<Error> OrderByQuantity();
        public List<Error> OrderByEnvironment();
        public List<Error> OrderByApplicationLayer();
        public List<Error> OrderByStatus();
        public List<Error> FindByLevelId(int levelId);
        public List<Error> FindByEnvironmentId(int environmentId);
        public List<Error> FindByApplicationLayerId(int applicationLayerId);
        public List<Error> FindByStatus(char status);

        public void Save(Error error);
        public List<Error> GetAll();
        public Error GetById(int id);
        public void Remove(Error error);
        public void Update(Error error);
        public void ChangeStatus(Error error);
    }
}
