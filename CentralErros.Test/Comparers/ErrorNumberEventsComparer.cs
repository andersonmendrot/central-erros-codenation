using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using CentralErros.Domain.Models;

namespace CentralErros.Test.Comparers
{
    public class ErrorNumberEventsComparer : IEqualityComparer<Error>
    {
        public bool Equals(Error x, Error y)
        {
            return x.NumberEvents == y.NumberEvents;//Details.Equals(y.Details);
        }

        public int GetHashCode(Error obj)
        {
            return obj.NumberEvents.GetHashCode();
        }
    }
}
