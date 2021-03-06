﻿using System.Collections.Generic;
using CentralErros.Domain.Models;

namespace CentralErros.Domain.Repositories
{
    public interface IErrorRepository
    {
        public void Save(Error error);
        public List<Error> GetAll();
        public Error GetById(int id);
        public Error Remove(Error error);
        public void Update(Error error);

        public List<Error> OrderByLevel(List<Error> errors, string orderDirection);
        public List<Error> OrderByStatus(List<Error> errors, string orderDirection);
        public List<Error> SearchByApplicationLayerName(List<Error> errorList, string applicationLayerName);
        public List<Error> SearchByEnvironmentName(List<Error> errorList, string environmentName);
        public List<Error> SearchByLanguageName(List<Error> errorList, string languageName);
        public List<Error> SearchByLevelName(List<Error> errorList, string levelName);

        public List<Error> SearchByTitle(List<Error> errorList, string title);

        public List<Error> SearchByOrigin(List<Error> errorList, string origin);

        public int GetQuantity(Error error);
        public void ChangeStatus(Error error);
    }
}
