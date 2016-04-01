using System;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    internal class OrganizationQuery
    {
        private readonly IQueryable<Organization> organizations;

        public OrganizationQuery(IQueryable<Organization> organizations)
        {
            if (organizations == null)
            {
                throw new ArgumentNullException("organizations");
            }
            this.organizations = organizations;
        }

        public Organization Execute(Guid id)
        {
            try
            {
                return organizations.Single(o => o.Id.Equals(id));
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Format("Organization '{0}' not found.", id), "id", e);
            }
        }
    }
}