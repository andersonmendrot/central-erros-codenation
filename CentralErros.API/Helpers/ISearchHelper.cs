using CentralErros.Domain.Models;
using System.Collections.Generic;

namespace CentralErros.API.Helpers
{
    public interface ISearchHelper
    {
        List<Error> SearchErrors(List<Error> errors, string searchValue, int? searchField);
    }
}
