using System;
using System.Collections.Generic;

namespace OrganizationRegister.Application.Classification
{
    public interface IHierarchicalClass : IHierarchical, IOrderable
    {
        Guid Id { get; }
        IEnumerable<IHierarchicalClass> SubClasses { get; }
    }
}
