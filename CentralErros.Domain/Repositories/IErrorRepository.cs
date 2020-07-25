using System.Collections.Generic;
using CentralErros.Domain.Models;

namespace CentralErros.Domain.Repositories
{
    public interface IErrorRepository
    {
        public List<Error> OrderByLevel(List<Error> errors, string orderDirection);
        public List<Error> OrderByStatus(List<Error> errors, string orderDirection);
        public List<Error> FindByLevelId(int levelId);
        public List<Error> FindByEnvironmentId(int environmentId);
        public List<Error> FindByApplicationLayerId(int applicationLayerId);
        public List<Error> FindByStatus(char status);
        public List<Error> FindByLanguageId(int languageId);
        public void ChangeStatus(Error error);
        public void Save(Error error);
        public List<Error> GetAll();
        public Error GetById(int id);
        public Error Remove(Error error);
        public void Update(Error error);
        public List<Error> LimitResultNumber(List<Error> errors, int limit);
        public List<Error> SearchByApplicationLayerName(List<Error> errorList, string applicationLayerName);
        public List<Error> SearchByEnvironmentName(List<Error> errorList, string environmentName);
        public List<Error> SearchByLanguageName(List<Error> errorList, string languageName);
        public List<Error> SearchByLevelName(List<Error> errorList, string levelName);

        public List<Error> SearchByTitle(List<Error> errorList, string title);

        public List<Error> SearchByOrigin(List<Error> errorList, string origin);

        public int GetQuantity(Error error);
    }
}
