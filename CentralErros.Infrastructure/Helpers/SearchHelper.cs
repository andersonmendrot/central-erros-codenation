using CentralErros.Domain.Models;
using CentralErros.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace CentralErros.Infrastructure.Helpers
{
    public class SearchHelper
    {
        IErrorRepository _errorRepository;

        public SearchHelper(IErrorRepository errorRepository)
        {
            _errorRepository = errorRepository;
        }

        public List<Error> SearchErrors(List<Error> errors, string searchValue, int? searchField)
        {
            if (!String.IsNullOrWhiteSpace(searchValue))
            {
                return searchField switch
                {
                    1 => _errorRepository.SearchByApplicationLayerName(errors, searchValue),
                    2 => _errorRepository.SearchByLanguageName(errors, searchValue),
                    3 => _errorRepository.SearchByLevelName(errors, searchValue),
                    4 => _errorRepository.SearchByOrigin(errors, searchValue),
                    5 => _errorRepository.SearchByTitle(errors, searchValue),
                    _ => errors
                };
            }
            return errors;
        }
    }
}
