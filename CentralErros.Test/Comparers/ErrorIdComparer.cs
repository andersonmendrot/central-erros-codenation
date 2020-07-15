using CentralErros.Domain.Models;
using System.Collections.Generic;

namespace CentralErros.Test.Comparers
{
    class ErrorIdComparer : IEqualityComparer<Error>
    {
        public bool Equals(Error x, Error y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Error obj)
        {
            return obj.Id.GetHashCode();
        } 
    }
}
