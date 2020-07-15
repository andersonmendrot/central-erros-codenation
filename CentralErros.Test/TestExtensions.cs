using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace CentralErros.Test
{
    public static class TestExtensions
    {
        public static void AssertWith<TExpected, TActual>(this IEnumerable<TActual> actual, IEnumerable<TExpected> expected, Action<TExpected, TActual> inspector)
        {
            Assert.Collection(actual, expected.Select(e => (Action<TActual>)(a => inspector(e, a))).ToArray());
        }
    }
}
