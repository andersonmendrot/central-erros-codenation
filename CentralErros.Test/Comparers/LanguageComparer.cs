using CentralErros.Domain.Models;
using System.Collections.Generic;

namespace CentralErros.Test.Comparers
{
    class LanguageComparer : IEqualityComparer<Language>
    {
        public bool Equals(Language x,  Language y)
        {
            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(Language obj)
        {
            return obj.GetHashCode();
        }
    }
}
