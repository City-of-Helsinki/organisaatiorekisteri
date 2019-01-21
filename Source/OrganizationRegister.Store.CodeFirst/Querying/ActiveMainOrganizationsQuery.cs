using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    internal class ActiveMainOrganizationsQuery
    {
        private readonly IQueryable<Organization> organizations;

        public ActiveMainOrganizationsQuery(IQueryable<Organization> organizations)
        {
            if (organizations == null)
            {
                throw new ArgumentNullException("organizations");
            }
            this.organizations = organizations;
        }

        public IEnumerable<Organization> Execute()
        {
            return organizations.Where(o => o.Active && o.ParentOrganizationId == null);
        }

        public IEnumerable<Organization> Execute(string businessId)
        {
            return organizations.Where(o => o.Active && o.ParentOrganizationId == null && o.BusinessId == businessId);
        }
    }
}
