using System;
using System.Collections.Generic;
using System.Linq;

namespace OrganizationRegister.Application.Classification
{
    internal class HierarchicalClass : IHierarchicalClass
    {
        private List<IHierarchicalClass> children;

        public HierarchicalClass(Guid id, string name, string sourceId, string sourceParentId, int? orderNumber)
        {
            if (id.Equals(Guid.Empty))
            {
                throw new ArgumentException("Id must be defined.", "id");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name must be defined", "name");
            }
            if (string.IsNullOrWhiteSpace(sourceId))
            {
                throw new ArgumentException("Source id must be defined", "sourceId");
            }

            Id = id;
            Name = name;
            SourceId = sourceId;
            SourceParentId = sourceParentId;
            OrderNumber = orderNumber;
            children = new List<IHierarchicalClass>();
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string SourceId { get; private set; }
        public string SourceParentId { get; private set; }
        public int? OrderNumber { get; private set; }

        public IEnumerable<IHierarchical> Children
        {
            get { return children; }
        }

        public IEnumerable<IHierarchicalClass> SubClasses
        {
            get { return children; }
        }

        public bool HasParent
        {
            get { return !string.IsNullOrWhiteSpace(SourceParentId); }
        }

        public void AddSubClasses(IEnumerable<IHierarchicalClass> classes)
        {
            children.AddRange(classes);
        }

        public bool IsMyChild(IHierarchical hierarchical)
        {
            HierarchicalClass @class = hierarchical as HierarchicalClass;
            return @class != null && @class.HasParent && SourceId.Equals(@class.SourceParentId);
        }

        public void AddChildren(IEnumerable<IHierarchical> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            var newChildren = items as IList<IHierarchical> ?? items.ToList();
            if (!newChildren.All(item => (item is IHierarchicalClass)))
            {
                throw new ArgumentException("Only hierarchical classes can be added.", "items");
            }
            if (newChildren.Any())
            {
                OrderableCollection<IHierarchicalClass> allChildren = new OrderableCollection<IHierarchicalClass>(children.Concat(newChildren.Cast<IHierarchicalClass>()));
                children = allChildren.Order().ToList();
            }
        }
    }
}
