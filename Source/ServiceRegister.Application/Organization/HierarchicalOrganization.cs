﻿using System;
using System.Collections.Generic;
using System.Linq;
using ServiceRegister.Common;

namespace ServiceRegister.Application.Organization
{
    internal class HierarchicalOrganization : OrganizationName, IHierarchicalOrganization
    {
        private readonly List<IHierarchicalOrganization> children;

        public HierarchicalOrganization(Guid id, IEnumerable<LocalizedText> names, Guid? parentId)
            : base(id, names)
        {
            ParentId = parentId;
            children = new List<IHierarchicalOrganization>();
        }

        public Guid? ParentId { get; private set; }

        public IEnumerable<IHierarchicalOrganization> SubOrganizations
        {
            get { return children; }
        }

        public IEnumerable<IHierarchical> Children
        {
            get { return children; }
        }

        public bool HasParent
        {
            get { return ParentId.HasValue; }
        }

        public bool IsMyChild(IHierarchical hierarchical)
        {
            HierarchicalOrganization organization = hierarchical as HierarchicalOrganization;
            return organization != null && organization.HasParent && Id.Equals(organization.ParentId.Value);
        }

        public void AddChildren(IEnumerable<IHierarchical> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            var newChildren = items as IList<IHierarchical> ?? items.ToList();
            if (!newChildren.All(item => (item is IHierarchicalOrganization)))
            {
                throw new ArgumentException("Only hierarchical organizations can be added.", "items");
            }
            if (newChildren.Any())
            {
                children.AddRange(newChildren.Cast<IHierarchicalOrganization>().ToList());
            }
        }
    }
}