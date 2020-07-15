using CentralErros.Domain.Models;
using System.Collections.Generic;
namespace CentralErros.Test.Comparers
{
    class ApplicationLayerComparer : IEqualityComparer<ApplicationLayer>
    {
        public bool Equals(ApplicationLayer x, ApplicationLayer y)
        {
            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(ApplicationLayer obj)
        {
            return obj.GetHashCode();
        }
    }
}
