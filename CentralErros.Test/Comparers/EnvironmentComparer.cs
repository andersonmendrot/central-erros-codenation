using CentralErros.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Environment = CentralErros.Domain.Models.Environment;

namespace CentralErros.Test.Comparers
{
    class EnvironmentComparer : IEqualityComparer<Environment>
    {
        public bool Equals(Environment x, Environment y)
        {
            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(Environment obj)
        {
            return obj.GetHashCode();
        }
    }
}
