using System;
using System.Collections.Generic;
using System.Web.Http;
using Affecto.Mapping;
using Affecto.WebApi.Toolkit.CustomRoutes;
using OrganizationRegister.Application.Organization;

namespace OrganizationRegister.Api.Organization
{
    [RoutePrefix("v1/organizationregister")]
    public class OpenOrganizationController : ApiController
    {
        private readonly IOrganizationService organizationService;
        private readonly MapperFactory mapperFactory;

        public OpenOrganizationController(IOrganizationService organizationService, MapperFactory mapperFactory)
        {
            if (organizationService == null)
            {
                throw new ArgumentNullException("organizationService");
            }
            if (mapperFactory == null)
            {
                throw new ArgumentNullException("mapperFactory");
            }
            this.organizationService = organizationService;
            this.mapperFactory = mapperFactory;
        }

        [HttpGet]
        [GetRoute("organizationhierarchy")]
        public IHttpActionResult GetOrganizationHierarchy()
        {
            IEnumerable<IHierarchicalOrganization> organizations = organizationService.GetActiveOrganizationHierarchy();
            var mapper = mapperFactory.CreateHierarchicalOrganizationMapper();
            IEnumerable<HierarchicalOrganization> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

        [HttpGet]
        [GetRoute("currentorganizationhierarchy")]
        public IHttpActionResult GetCurrentOrganizationHierarchy()
        {
            IEnumerable<IHierarchicalOrganization> organizations = organizationService.GetActiveOrganizationHierarchyForRootOrganization(null, includeFutureOrganizations: false);
            var mapper = mapperFactory.CreateHierarchicalOrganizationMapper();
            IEnumerable<HierarchicalOrganization> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }


        [HttpGet]
        [GetRoute("currentandfutureorganizationhierarchy")]
        public IHttpActionResult GetCurrentAndFutureOrganizationHierarchy()
        {
            IEnumerable<IHierarchicalOrganization> organizations = organizationService.GetActiveOrganizationHierarchyForRootOrganization(null, includeFutureOrganizations: true);
            var mapper = mapperFactory.CreateHierarchicalOrganizationMapper();
            IEnumerable<HierarchicalOrganization> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

        [HttpGet]
        [GetRoute("currentorganizationhierarchyfororganization/{organizationId}")]
        public IHttpActionResult GetCurrentOrganizationHierarchyForOrganization(Guid organizationId)
        {
            IEnumerable<IHierarchicalOrganization> organizations = organizationService.GetActiveOrganizationHierarchyForRootOrganization(organizationId, includeFutureOrganizations: false);
            var mapper = mapperFactory.CreateHierarchicalOrganizationMapper();
            IEnumerable<HierarchicalOrganization> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

        [HttpGet]
        [GetRoute("currentandfutureorganizationhierarchyfororganization/{organizationId}")]
        public IHttpActionResult GetCurrentAndFutureOrganizationHierarchyForOrganization(Guid organizationId)
        {
            IEnumerable<IHierarchicalOrganization> organizations = organizationService.GetActiveOrganizationHierarchyForRootOrganization(organizationId, includeFutureOrganizations:true);
            var mapper = mapperFactory.CreateHierarchicalOrganizationMapper();
            IEnumerable<HierarchicalOrganization> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }


        [HttpGet]
        [GetRoute("mainorganizations")]
        public IHttpActionResult GetMainOrganizations()
        {
            IEnumerable<IOrganizationName> organizations = organizationService.GetActiveMainOrganizations();
            var mapper = mapperFactory.CreateOrganizationNameMapper();
            IEnumerable<OrganizationName> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

        [HttpGet]
        [GetRoute("organizations")]
        public IHttpActionResult GetOrganizations()
        {
            IEnumerable<IOrganizationName> organizations = organizationService.GetActiveOrganizations();
            var mapper = mapperFactory.CreateOrganizationNameMapper();
            IEnumerable<OrganizationName> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

        [HttpGet]
        [GetRoute("organizations/{organizationId}")]
        public IHttpActionResult GetOrganization(Guid organizationId)
        {
            IOrganization organization = organizationService.GetOrganization(organizationId);
            var mapper = mapperFactory.CreateOrganizationMapper();
            Organization mappedOrganization = mapper.Map(organization);
            return Ok(mappedOrganization);
        }
    }
}