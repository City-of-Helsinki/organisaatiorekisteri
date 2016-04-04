using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    internal class SubOrganizationQuery
    {
        private readonly IQueryable<Organization> organizations;

        public SubOrganizationQuery(IQueryable<Organization> organizations)
        {
            if (organizations == null)
            {
                throw new ArgumentNullException("organizations");
            }
            this.organizations = organizations;
        }

        public IEnumerable<Organization> Execute(Guid organizationId)
        {
            return organizations.Where(org => org.ParentOrganization != null && org.ParentOrganization.Id == organizationId);
        }
    }
}