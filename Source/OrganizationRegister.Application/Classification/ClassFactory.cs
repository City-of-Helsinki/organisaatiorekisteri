using System;

namespace OrganizationRegister.Application.Classification
{
    public class ClassFactory
    {
        public static IHierarchicalClass CreateHierarchicalClass(Guid id, string name, string sourceId, string sourceParentId, int? orderNumber)
        {
            return new HierarchicalClass(id, name, sourceId, sourceParentId, orderNumber);
        }

        public static IClass CreateClass(Guid id, string name)
        {
            return new Class(id, name);
        }
    }
}
