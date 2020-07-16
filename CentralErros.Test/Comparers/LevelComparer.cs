using CentralErros.Domain.Models;
using System.Collections.Generic;

namespace CentralErros.Test.Comparers
{
    class LevelComparer : IEqualityComparer<Level>
    {
        public bool Equals(Level x, Level y)
        {
            return x.Id == y.Id && x.Name == y.Name; 
        }

        public int GetHashCode(Level obj)
        {
            return obj.GetHashCode();
        }
    }
}
