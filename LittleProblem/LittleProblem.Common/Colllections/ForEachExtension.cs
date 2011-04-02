using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LittleProblem.Common.Colllections
{
    /// <summary>
    /// Extension to give IEnumerable an extension method to iterate on its content.
    /// </summary>
    public static class ForEachExtension
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var e in enumerable)
            {
                action(e);
            }
        }
    }
}
