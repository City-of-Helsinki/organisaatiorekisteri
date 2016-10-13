using System;
using System.Collections.Generic;
using System.Linq;

using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    internal class ActiveCurrentAndFutureOrganizationsQuery
    {
        private readonly IQueryable<Organization> organizations;

        public ActiveCurrentAndFutureOrganizationsQuery(IQueryable<Organization> organizations)
        {
            if (organizations == null)
            {
                throw new ArgumentNullException("organizations");
            }
            this.organizations = organizations;
        }

        public IEnumerable<Organization> Execute()
        {
            var now = DateTime.Now.Date;
            var validOrganizations = organizations.Where(o =>
                    o.Active
                    && (!o.ValidTo.HasValue || (o.ValidTo.HasValue && o.ValidTo.Value >= now))
                    ).OrderBy(org => org.LanguageSpecifications.FirstOrDefault(lang => lang.Language.Language.Code == "fi").Name);

            return validOrganizations;
        }
    }
}
