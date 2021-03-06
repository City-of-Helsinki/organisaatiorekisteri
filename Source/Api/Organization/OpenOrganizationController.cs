﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        [GetRoute("organizationflatlist/{organizationId}/{searchTerm}")]
        public IHttpActionResult GetOrganizationFlatlistForOrganization(string searchTerm, Guid? organizationId)
        {
            // TODO: replace return type IHierarchicalOrganization with new type ("IOrganizationListItem" etc) 
            IEnumerable<IHierarchicalOrganization> organizations = organizationService.GetOrganizationsAsFlatlist(searchTerm, organizationId);
            var mapper = mapperFactory.CreateHierarchicalOrganizationMapper();
            IEnumerable<HierarchicalOrganization> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }


        [HttpGet]
        [GetRoute("organizationflatlist/{searchTerm}")]
        public IHttpActionResult GetOrganizationFlatlist(string searchTerm)
        {
            // TODO: replace return type IHierarchicalOrganization with new type ("IOrganizationListItem" etc) 
            IEnumerable<IHierarchicalOrganization> organizations = organizationService.GetOrganizationsAsFlatlist(searchTerm, null);
            var mapper = mapperFactory.CreateHierarchicalOrganizationMapper();
            IEnumerable<HierarchicalOrganization> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

        [HttpGet]
        [GetRoute("organizationlistformunicipality/{rootMunicipalityCode}")]
        public IHttpActionResult GetOrganizationListForMunicipality(int rootMunicipalityCode)
        {

            IEnumerable<IOrganizationListItem> organizations = organizationService.GetOrganizationListForMunicipality(rootMunicipalityCode);
            var mapper = mapperFactory.CreateOrganizationListItemMapper();
            IEnumerable<OrganizationListItem> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

        [HttpGet]
        [GetRoute("organizationlistfororganization/{businessId:length(9)}")]
        public IHttpActionResult GetOrganizationListForOrganizationByBusinessId(string businessId)
        {

            IEnumerable<IOrganizationListItem> organizations = organizationService.GetOrganizationListForOrganization(businessId);
            var mapper = mapperFactory.CreateOrganizationListItemMapper();
            IEnumerable<OrganizationListItem> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }


        [HttpGet]
        [GetRoute("organizationlistfororganization/{organizationId:guid}")]
        public IHttpActionResult GetOrganizationListForOrganization(Guid organizationId)
        {

            IEnumerable<IOrganizationListItem> organizations = organizationService.GetOrganizationListForOrganization(organizationId);
            var mapper = mapperFactory.CreateOrganizationListItemMapper();
            IEnumerable<OrganizationListItem> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

        [HttpGet]
        [GetRoute("mainorganizationlist")]
        public IHttpActionResult GetMainOrganizationList()
        {

            IEnumerable<IOrganizationListItem> organizations = organizationService.GetMainOrganizationList();
            var mapper = mapperFactory.CreateOrganizationListItemMapper();
            IEnumerable<OrganizationListItem> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

        [HttpGet]
        [GetRoute("organizationhierarchy")]
        public IHttpActionResult GetOrganizationHierarchy()
        {
            IEnumerable<IHierarchicalOrganization> organizations = organizationService.GetOrganizationHierarchy();
            var mapper = mapperFactory.CreateHierarchicalOrganizationMapper();
            IEnumerable<HierarchicalOrganization> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

        [HttpGet]
        [GetRoute("organizationhierarchyfororganization/{organizationId}")]
        public IHttpActionResult GetOrganizationHierarchyForOrganization(Guid organizationId)
        {
            IEnumerable<IHierarchicalOrganization> organizations = organizationService.GetCompleteOrganizationHierarchyForOrganization(organizationId);
            var mapper = mapperFactory.CreateHierarchicalOrganizationMapper();
            IEnumerable<HierarchicalOrganization> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

        [HttpGet]
        [GetRoute("currentorganizationhierarchy")]
        public IHttpActionResult GetCurrentOrganizationHierarchy()
        {
            IEnumerable<IHierarchicalOrganization> organizations = organizationService.GetOrganizationHierarchy(false);
            var mapper = mapperFactory.CreateHierarchicalOrganizationMapper();
            IEnumerable<HierarchicalOrganization> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }


        [HttpGet]
        [GetRoute("currentandfutureorganizationhierarchy")]
        public IHttpActionResult GetCurrentAndFutureOrganizationHierarchy()
        {
            IEnumerable<IHierarchicalOrganization> organizations = organizationService.GetOrganizationHierarchy(true);
            var mapper = mapperFactory.CreateHierarchicalOrganizationMapper();
            IEnumerable<HierarchicalOrganization> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

        [HttpGet]
        [GetRoute("currentorganizationhierarchyfororganization/{organizationId}")]
        public IHttpActionResult GetCurrentOrganizationHierarchyForOrganization(Guid organizationId)
        {
            IEnumerable<IHierarchicalOrganization> organizations = organizationService.GetOrganizationHierarchyForOrganization(organizationId, includeFutureOrganizations: false);
            var mapper = mapperFactory.CreateHierarchicalOrganizationMapper();
            IEnumerable<HierarchicalOrganization> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

        [HttpGet]
        [GetRoute("currentandfutureorganizationhierarchyfororganization/{organizationId}")]
        public IHttpActionResult GetCurrentAndFutureOrganizationHierarchyForOrganization(Guid organizationId)
        {
            IEnumerable<IHierarchicalOrganization> organizations = organizationService.GetOrganizationHierarchyForOrganization(organizationId, includeFutureOrganizations:true);
            var mapper = mapperFactory.CreateHierarchicalOrganizationMapper();
            IEnumerable<HierarchicalOrganization> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }


        [HttpGet]
        [GetRoute("mainorganizations")]
        public IHttpActionResult GetMainOrganizations()
        {
            IEnumerable<IOrganizationName> organizations = organizationService.GetMainOrganizations();
            var mapper = mapperFactory.CreateOrganizationNameMapper();
            IEnumerable<OrganizationName> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

        [HttpGet]
        [GetRoute("organizations")]
        public IHttpActionResult GetOrganizations()
        {
            IEnumerable<IOrganizationName> organizations = organizationService.GetOrganizations();
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




        [HttpGet]
        [GetRoute("organizationflatlistforgroups/{groupIds}")]
        public IHttpActionResult GetOrganizationFlatlistForGroups(string groupIds)
        {
            IEnumerable<IOrganizationListItem> organizations = organizationService.GetGroupOrganizationsAsFlatlist(groupIds);
            var mapper = mapperFactory.CreateOrganizationListItemMapper();
            IEnumerable<OrganizationListItem> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

    }
}