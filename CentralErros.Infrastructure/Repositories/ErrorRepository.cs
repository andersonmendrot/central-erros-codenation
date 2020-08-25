using System.Collections.Generic;
using CentralErros.Domain.Repositories;
using CentralErros.Domain.Models;
using System.Linq;

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
            error.Status = error.Status == 'y' ? 'n' : 'y';
            _context.Update(error);
            _context.SaveChanges();
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

        public List<Error> OrderByLevel(List<Error> errors, string orderDirection)
        {
            if (orderDirection == "Descending")
            {
                return _context.Errors.OrderByDescending(x => x.LevelId).ToList();
            }

            return _context.Errors.OrderBy(x => x.LevelId).ToList();
        }

        public List<Error> OrderByStatus(List<Error> errors, string orderDirection)
        {
            if (orderDirection == "Descending")
            {
                return _context.Errors.OrderByDescending(x => x.Status).ToList();
            }

            return _context.Errors.OrderBy(x => x.Status).ToList();
        }

        public List<Error> OrderByDate()
        {
            return new List<Error>();
        }

        public Error Remove(Error error)
        {
            var existingError = _context.Errors.Find(error.Id);
            _context.Remove(existingError);
            _context.SaveChanges();
            return existingError;
        }

        public void Save(Error error)
        {
            _context.Errors.Add(error);
            _context.SaveChanges();
        }

        public void Update(Error error)
        {
            var attachedEntry = _context.Entry(error);
            attachedEntry.CurrentValues.SetValues(error);
            _context.SaveChanges();
        }

        public List<Error> SearchByApplicationLayerName(List<Error> errorList, string applicationLayerName)
        {
            applicationLayerName = applicationLayerName.Trim();

            //lista com os identificadores retornados pela pesquisa
            List<int> applicationLayerList = _context.ApplicationLayers.
                Where(x => x.Name.Contains(applicationLayerName)).
                Select(x => x.Id).
                ToList();

            if (applicationLayerList == null)
            {
                return null;
            }

            //lista que vai conter os erros que utilizam todas as 
            //camadas de aplicacao retornadas pela pesquisa
            List<Error> errors = new List<Error>();

            //a cada iteracao, o vetor errors recebe a lista de erros
            //de cada camada de aplicacao retornada pela pesquisa
            foreach (var applicationLayerId in applicationLayerList)
            {
                var listById = errorList.Where(x => x.ApplicationLayerId == applicationLayerId).ToList();
                errors.AddRange(listById);
            }

            return errors;
        }

        public List<Error> SearchByEnvironmentName(List<Error> errorList, string environmentName)
        {
            environmentName = environmentName.Trim();

            //lista com os identificadores dos ambientes retornados pela pesquisa
            List<int> environmentList = _context.Environments.
                Where(x => x.Name == environmentName).
                Select(x => x.Id).
                ToList();

            if (environmentList == null)
            {
                return null;
            }

            //lista que vai conter os erros que utilizam todos os 
            //ambientes retornados pela pesquisa
            List<Error> errors = new List<Error>();

            //a cada iteracao, o vetor errors recebe a lista de erros
            //de cada ambiente retornado pela pesquisa
            foreach (var environmentId in environmentList)
            {
                var listById = errorList.Where(x => x.EnvironmentId == environmentId).ToList();
                errors.AddRange(listById);
            }

            return errors;
        }

        public List<Error> SearchByLanguageName(List<Error> errorList, string languageName)
        {
            languageName = languageName.Trim();

            //lista com os identificadores das linguagens retornadas pela pesquisa
            List<int> languageList = _context.Languages.
                Where(x => x.Name.Contains(languageName)).
                Select(x => x.Id).
                ToList();

            if (languageList == null)
            {
                return null;
            }

            //lista que vai conter os erros que utilizam todas as 
            //linguagens retornadas pela pesquisa
            List<Error> errors = new List<Error>();

            //a cada iteracao, o vetor errors recebe a lista de erros
            //de cada linguagem que foi retornada pela pesquisa
            foreach (var languageId in languageList)
            {
                var listById = errorList.Where(x => x.LanguageId == languageId).ToList();
                errors.AddRange(listById);
            }

            return errors;
        }

        public List<Error> SearchByLevelName(List<Error> errorList, string levelName)
        {
            levelName = levelName.Trim();

            //lista com os identificadores dos levels retornados pela pesquisa
            List<int> levelList = _context.Levels.
                Where(x => x.Name.Contains(levelName)).
                Select(x => x.Id).
                ToList();

            if (levelList == null)
            {
                return null;
            }

            //lista que vai conter os erros que utilizam todos os levels 
            //retornados pela pesquisa
            List<Error> errors = new List<Error>();

            //a cada iteracao, o vetor errors recebe a lista de erros
            //de cada level retornado pela pesquisa
            foreach (var levelId in levelList)
            {
                var listById = errorList.Where(x => x.LevelId == levelId).ToList();
                errors.AddRange(listById);
            }

            return errors;
        }

        public List<Error> GetByStatus(List<Error> errorList, char status)
        {
            return errorList.Where(x => x.Status == status).ToList();
        }

        public List<Error> SearchByTitle(List<Error> errorList, string title)
        {
            return errorList.Where(x => x.Title.Contains(title)).ToList();
        }

        public List<Error> SearchByOrigin(List<Error> errorList, string origin)
        {
            origin = origin.Trim();
            return errorList.Where(x => x.Origin.Contains(origin)).ToList();
        }

        public int GetQuantity(Error error)
        {
            var findValue = error.Title;
            List<Error> errors = _context.Errors.ToList();

            return errors.Where(x => x.Title == error.Title &&
                    x.Details == error.Details &&
                    x.Status.ToString() == error.Status.ToString() &&
                    x.ApplicationLayerId == error.ApplicationLayerId &&
                    x.EnvironmentId == error.EnvironmentId &&
                    x.LanguageId == error.LanguageId &&
                    x.LevelId == error.LevelId).Count();
        }
    }
}
