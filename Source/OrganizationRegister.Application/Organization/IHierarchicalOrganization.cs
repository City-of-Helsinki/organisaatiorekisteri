using System.Collections.Generic;

namespace OrganizationRegister.Application.Organization
{
    public interface IHierarchicalOrganization : IOrganizationName, IHierarchical
    {
        IEnumerable<IHierarchicalOrganization> SubOrganizations { get; }
    }
}