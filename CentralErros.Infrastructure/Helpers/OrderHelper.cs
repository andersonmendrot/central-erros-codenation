using CentralErros.Domain.Models;
using CentralErros.Domain.Repositories;
using System.Collections.Generic;

namespace CentralErros.Infrastructure.Helpers
{
    public class OrderHelper
    {
        IErrorRepository _errorRepository;
        public OrderHelper(IErrorRepository errorRepository)
        {
            _errorRepository = errorRepository;
        }

        public List<Error> OrderErrors(List<Error> errors, string orderDirection, int? orderField)
        {
            return orderField switch
            {
                1 => _errorRepository.OrderByLevel(errors, orderDirection),
                2 => _errorRepository.OrderByStatus(errors, orderDirection),
                _ => errors,
            };
        } 
    }
}
