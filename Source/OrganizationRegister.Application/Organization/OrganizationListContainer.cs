using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Organization
{
    internal class OrganizationListContainer : IOrganizationListContainer
    {
        public OrganizationListContainer()
        {
            PeruskayttajaOrganizations = new List<IOrganizationListItem>();
            EsteettomyysOrganizations = new List<IOrganizationListItem>();
        }

        public OrganizationListContainer(IEnumerable<IOrganizationListItem> peruskayttajaOrgs, IEnumerable<IOrganizationListItem> esteettomyysOrgs)
        {
            PeruskayttajaOrganizations = peruskayttajaOrgs ?? new List<IOrganizationListItem>();
            EsteettomyysOrganizations = esteettomyysOrgs ?? new List<IOrganizationListItem>();
        }

        public IEnumerable<IOrganizationListItem> PeruskayttajaOrganizations { get; set; }

        public IEnumerable<IOrganizationListItem> EsteettomyysOrganizations { get; set; }
    }


}