using System;
using System.Collections.Generic;
using System.Linq;

namespace EkranPaylas.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<T> GetInstances<T>(this Type type)
        {
            return typeof (T).Assembly.GetTypes().Where(q => q.GetInterface(typeof (T).Name) == typeof (T))
                             .Select(b => b.GetConstructors().First().Invoke(null))
                             .Cast<T>();
        }
    }
}
