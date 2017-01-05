using System;
using System.Collections.Generic;
using System.Text;

namespace OrganizationRegister.Common
{

    public static class CollectionExtensions
    {

        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childrenSelector)
        {
            foreach (var item in source)
            {
                yield return item;
                foreach (var child in childrenSelector(item).Flatten(childrenSelector))
                {
                    yield return child;
                }
            }
        }
    }

}
