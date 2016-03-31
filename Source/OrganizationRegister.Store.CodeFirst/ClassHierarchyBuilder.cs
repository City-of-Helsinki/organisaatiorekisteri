using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Application;
using OrganizationRegister.Application.Classification;

namespace OrganizationRegister.Store.CodeFirst
{
    internal class ClassHierarchyBuilder
    {
        public static IReadOnlyCollection<IHierarchicalClass> CreateOrderedClassHierarchy(IEnumerable<Model.IClass> classes)
        {
            IEnumerable<IHierarchicalClass> hierarchicalClasses = classes.ToList()
                .Select(@class => ClassFactory.CreateHierarchicalClass(@class.Id, @class.Name, @class.SourceId, @class.SourceParentId, @class.OrderNumber));
            OrderableCollection<IHierarchicalClass> orderableClasses =
                new OrderableCollection<IHierarchicalClass>(HierarchicalCollection<IHierarchicalClass>.CreateHierarchy(hierarchicalClasses));
            return orderableClasses.Order().ToList();
        }
    }
}
