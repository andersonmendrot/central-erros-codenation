using System.Collections.Generic;
using CentralErros.Domain.Models;

namespace CentralErros.Domain.Repositories
{
    public interface IErrorRepository
    {
        public List<Error> OrderByLevel(string sortdir);
        public List<Error> OrderByQuantity(string sortdir);
        public List<Error> OrderByStatus(string sortdir);
        public List<Error> FindByLevelId(int levelId);
        public List<Error> FindByEnvironmentId(int environmentId);
        public List<Error> FindByApplicationLayerId(int applicationLayerId);
        public List<Error> FindByStatus(char status);
        public List<Error> FindByLanguageId(int languageId);
        public void ChangeStatus(Error error);
        public void Save(Error error);
        public List<Error> GetAll();
        public Error GetById(int id);
        public void Remove(Error error);
        public void Update(Error error);
        public List<Error> LimitResultNumber(List<Error> errors, int limit);
    }
}
