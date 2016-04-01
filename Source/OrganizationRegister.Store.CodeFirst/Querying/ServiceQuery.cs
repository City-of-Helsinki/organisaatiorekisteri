using System;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    public class ServiceQuery
    {
        private readonly IQueryable<Service> services;

        public ServiceQuery(IQueryable<Service> services)
        {
            if (services == null)
            {
                throw new ArgumentNullException("services");
            }
            this.services = services;
        }

        public Service Execute(Guid organizationId, Guid serviceId)
        {
            try
            {
                return services.Single(service => service.Id.Equals(serviceId) && service.OrganizationId.Equals(organizationId));
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Format("Service '{0}' not found for organization '{1}'.", serviceId, organizationId), e);
            }
        }
    }
}
