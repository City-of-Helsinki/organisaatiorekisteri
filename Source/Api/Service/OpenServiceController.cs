using System;
using System.Collections.Generic;
using System.Web.Http;
using Affecto.Mapping;
using Affecto.WebApi.Toolkit.CustomRoutes;
using OrganizationRegister.Application.Service;

namespace OrganizationRegister.Api.Service
{
    [RoutePrefix("v1/serviceregister")]
    public class OpenServiceController : ApiController
    {
        private readonly IServiceService serviceService;
        private readonly MapperFactory mapperFactory;

        public OpenServiceController(IServiceService serviceService, MapperFactory mapperFactory)
        {
            if (serviceService == null)
            {
                throw new ArgumentNullException("serviceService");
            }
            if (mapperFactory == null)
            {
                throw new ArgumentNullException("mapperFactory");
            }

            this.serviceService = serviceService;
            this.mapperFactory = mapperFactory;
        }

        [HttpGet]
        [GetRoute("organizations/{organizationId}/services")]
        public IHttpActionResult GetServices(Guid organizationId)
        {
            IEnumerable<IServiceListItem> services = serviceService.GetServices(organizationId);
            var mapper = mapperFactory.CreateServiceNameMapper();
            IEnumerable<ServiceListItem> mappedServices = mapper.Map(services);
            return Ok(mappedServices);
        }

        [HttpGet]
        [GetRoute("organizations/{organizationId}/services/{serviceId}")]
        public IHttpActionResult GetService(Guid organizationId, Guid serviceId)
        {
            IService service = serviceService.GetService(organizationId, serviceId);
            var mapper = mapperFactory.CreateServiceMapper();
            Service mappedService = mapper.Map(service);
            return Ok(mappedService);
        }
    }
}