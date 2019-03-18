using System.Collections.Generic;

namespace OrganizationRegister.Application.Organization
{
    public interface IOrganizationListContainer
    {
        IEnumerable<IOrganizationListItem> PeruskayttajaOrganizations { get; set; }

        IEnumerable<IOrganizationListItem> EsteettomyysOrganizations { get; set; }
    }
}