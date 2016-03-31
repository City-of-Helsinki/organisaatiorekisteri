using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OrganizationRegister.Application
{
    public class OrderableCollection<T> : IEnumerable<T> where T : IOrderable
    {
        private readonly List<T> items;

        public OrderableCollection(IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            this.items = items.ToList();
        }

        public IEnumerable<T> Order()
        {
            IEnumerable<T> numericallyOrdered = items.Where(l => l.OrderNumber.HasValue).OrderBy(l => l.OrderNumber);
            IEnumerable<T> alphabeticallyOrdered = items.Where(l => !l.OrderNumber.HasValue).OrderBy(l => l.Name);
            return numericallyOrdered.Concat(alphabeticallyOrdered);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
