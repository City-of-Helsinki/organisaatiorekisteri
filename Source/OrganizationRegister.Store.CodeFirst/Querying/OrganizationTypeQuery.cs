using System;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    internal class OrganizationTypeQuery
    {
        private readonly IQueryable<OrganizationType> organizationTypes;

        public OrganizationTypeQuery(IQueryable<OrganizationType> organizationTypes)
        {
            if (organizationTypes == null)
            {
                throw new ArgumentNullException("organizationTypes");
            }
            this.organizationTypes = organizationTypes;
        }

        public OrganizationType Execute(string type)
        {
            try
            {
                return organizationTypes.Single(t => t.Name.Equals(type, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Format("No or more than one organization types '{0}' found.", type), e);
            }
        }
    }
}
