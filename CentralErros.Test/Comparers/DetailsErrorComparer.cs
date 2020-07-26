using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using CentralErros.Domain.Models;

namespace CentralErros.Test.Comparers
{
    public class DetailsErrorComparer : IEqualityComparer<Error>
    {
        public bool Equals(Error x, Error y)
        {
            return x.Details == y.Details;
        }

        public int GetHashCode(Error obj)
        {
            return obj.Details.GetHashCode();
        }
    }
}
