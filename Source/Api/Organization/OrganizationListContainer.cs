using System.Collections.Generic;

namespace OrganizationRegister.Api.Organization
{
    public class OrganizationListContainer
    {
        public IEnumerable<OrganizationListItem> PeruskayttajaOrganizations { get; set; }

        public IEnumerable<OrganizationListItem> EsteettomyysOrganizations { get; set; }
    }

}