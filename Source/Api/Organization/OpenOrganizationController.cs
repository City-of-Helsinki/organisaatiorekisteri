using System;
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
            W("OpenOrganizationController");

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
            W("organizationhierarchyfororganization organizationId: " + organizationId);

            IEnumerable<IHierarchicalOrganization> organizations = organizationService.GetCompleteOrganizationHierarchyForOrganization(organizationId);
            var mapper = mapperFactory.CreateHierarchicalOrganizationMapper();
            IEnumerable<HierarchicalOrganization> mappedOrganizations = mapper.Map(organizations);
            return Ok(mappedOrganizations);
        }

        [HttpGet]
        [GetRoute("currentorganizationhierarchy")]
        public IHttpActionResult GetCurrentOrganizationHierarchy()
        {
            W("currentorganizationhierarchy");
         

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
            W("GetCurrentOrganizationHierarchyForOrganization organizationId: " + organizationId);

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


        //PM 20.4.2021
        [HttpGet]
        [GetRoute("allauthorizationgroups")]
        public IHttpActionResult GetAllAuthorizationGroups()
        {
            W("GetAllAuthorizationGroups");
            int A = Environment.TickCount;

            List<Common.AuthorizationGroup> authorizationGroups = null;
            try
            {
                authorizationGroups = organizationService.GetAuthorizationGroups();
            }
            catch (Exception ex)
            {
                W(ex);
                throw ex;
            }

            W("GetAllAuthorizationGroups TickCount: " + (Environment.TickCount - A));
            return Ok(authorizationGroups);
        }

        private string GetAsciiList(string str)
        {
            string s = "";

            foreach (char c in str.ToCharArray())
            {
                s += ((int)c) + ",";
            }

            return s;
        }

        //PM 20.4.2021
        [HttpPost]
        [PostRoute("authorizationgroups")]
        public IHttpActionResult GetAuthorizationGroups([FromBody] string groupNames)
        {
            W("GetAuthorizationGroups GroupNames: " + groupNames);

            
            if (string.IsNullOrWhiteSpace(groupNames))
            {
                throw new Exception("GroupNames is null or empty.");
            }

            int A = Environment.TickCount;

            List<Common.AuthorizationGroup> authorizationGroups = new List<Common.AuthorizationGroup>();

            try
            {
                string[] arr = groupNames.ToLower().Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
                List<string> liGroupNames = new List<string>();
                liGroupNames.AddRange(arr);

                List<Common.AuthorizationGroup>  ags = organizationService.GetAuthorizationGroups();
                foreach (Common.AuthorizationGroup ag in ags)
                {
                    W("ags: [" + ag.Name + "][" + ag.OrganizationId + "][" + ag.RoleId + "]");

                    if (liGroupNames.Contains(ag.Name.ToLower()))
                    {
                        authorizationGroups.Add(ag);
                    }
                }

            }
            catch (Exception ex)
            {
                W(ex);
                throw ex;
            }

            W("GetAuthorizationGroups Count: " + authorizationGroups.Count + ", TickCount: " + (Environment.TickCount - A));
            return Ok(authorizationGroups);

        }

        private void W(object s)
        {
            System.Diagnostics.Trace.WriteLine("Trace[" + this.GetType() + "][" + this.GetHashCode() + "] " + s);
            System.Diagnostics.Debug.WriteLine("Debug[" + this.GetType() + "][" + this.GetHashCode() + "] " + s);
            Console.WriteLine("Debug[" + this.GetType() + "][" + this.GetHashCode() + "] " + s);

        }
        

    }
}