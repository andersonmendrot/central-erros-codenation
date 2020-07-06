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
        public List<Error> FindByLevel(string levelName);
        public List<Error> FindByEnvironment(string environmentName);
        public List<Error> FindByApplicationLayer(string applicationLayerName);
        public List<Error> FindByStatus(char status);

        public void Save(Error error);
        public List<Error> GetAll();
        public Error GetById(int id);
        public void Remove(Error error);
        public void Update(Error error);
        public void ChangeStatus(Error error);
    }
}
