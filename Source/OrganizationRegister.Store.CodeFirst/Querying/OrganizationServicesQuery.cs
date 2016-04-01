using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    internal class OrganizationServicesQuery
    {
        private readonly IQueryable<Service> services;

        public OrganizationServicesQuery(IQueryable<Service> services)
        {
            if (services == null)
            {
                throw new ArgumentNullException("services");
            }
            this.services = services;
        }

        public IEnumerable<Service> Execute(Guid organizationId)
        {
            return services.Where(o => o.OrganizationId.Equals(organizationId));
        }
    }
}

