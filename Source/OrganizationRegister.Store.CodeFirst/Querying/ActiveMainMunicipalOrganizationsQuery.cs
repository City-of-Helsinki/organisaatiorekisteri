using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    internal class ActiveCurrentMainMunicipalOrganizationsQuery
    {
        private readonly IQueryable<Organization> organizations;
        private readonly int municipalityCode;

        public ActiveCurrentMainMunicipalOrganizationsQuery (IQueryable<Organization> organizations, int municipalityCode)
        {
            if (organizations == null)
            {
                throw new ArgumentNullException("organizations");
            }
            this.organizations = organizations;
            this.municipalityCode = municipalityCode;

        }

        public IEnumerable<Organization> Execute()
        {
            var now = DateTime.Now.Date;
            return
                organizations.Where(
                    o => o.Active
                    && (!o.ValidFrom.HasValue || o.ValidFrom.HasValue && o.ValidFrom.Value <= now)
                    && (!o.ValidTo.HasValue || o.ValidTo.HasValue && o.ValidTo.Value >= now)
                    && o.ParentOrganizationId == null 
                    && o.MunicipalityCode == municipalityCode 
                    && o.Type.Name == Common.OrganizationType.Municipality);
        }
    }
}
