using CentralErros.Domain.Models;
using System.Collections.Generic;

namespace CentralErros.API.Helpers
{
    public interface IOrderHelper
    {
        List<Error> OrderErrors(List<Error> errors, string orderDirection, int? orderField);
    }
}
