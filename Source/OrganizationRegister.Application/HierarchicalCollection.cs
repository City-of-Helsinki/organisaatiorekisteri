using System;
using System.Collections.Generic;
using System.Linq;

namespace OrganizationRegister.Application
{
    public class HierarchicalCollection<T> where T : IHierarchical
    {
        public static IEnumerable<T> CreateHierarchy(IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            List<T> remainingItems = items.ToList();
            if (remainingItems.Any(org => org.HasParent && remainingItems.All(o => !o.IsMyChild(org))))
            {
                throw new ArgumentException("Hierarchical item(s) found with unexisting parent item.");
            }

            IReadOnlyCollection<T> rootItems = remainingItems.Where(org => !org.HasParent).ToList();
            remainingItems.RemoveAll(rootItems.Contains);
            AddChildren(remainingItems, rootItems);
            return rootItems;
        }

        private static void AddChildren(List<T> orphans, IEnumerable<T> parents)
        {
            if (orphans.Any())
            {
                foreach (T parent in parents)
                {
                    parent.AddChildren(orphans.Where(org => parent.IsMyChild(org)).Cast<IHierarchical>());
                    orphans.RemoveAll(orphan => parent.Children.Contains(orphan));
                    AddChildren(orphans, parent.Children.Cast<T>());
                }                
            }
        }
    }
}