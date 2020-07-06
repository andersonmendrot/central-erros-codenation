using System.Collections.Generic;
using CentralErros.Domain.Models;

namespace CentralErros.Domain.Repositories
{
    public interface IErrorRepository
    {
        public List<Error> OrderByLevel();
        public List<Error> OrderByQuantity();
        public List<Error> OrderByEnvironment();
        public List<Level> OrderByApplicationLayer();
        public List<Level> OrderByStatus();
    }
}
