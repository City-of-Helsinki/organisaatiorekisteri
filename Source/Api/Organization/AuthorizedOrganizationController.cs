using System;
using System.Web.Http;
using Affecto.WebApi.Toolkit.CustomRoutes;
using OrganizationRegister.Api.Location;
using OrganizationRegister.Application.Organization;

namespace OrganizationRegister.Api.Organization
{
    [RoutePrefix("v1/organizationregister")]
    public class AuthorizedOrganizationController : ApiController
    {
        private readonly IOrganizationService organizationService;
        
        public AuthorizedOrganizationController(IOrganizationService organizationService, MapperFactory mapperFactory)
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
        }

        [HttpPost]
        [PostRoute("organizations")]
        public IHttpActionResult AddOrganization(BasicInformation organization)
        {
            if (organization == null)
            {
                throw new ArgumentNullException("organization");
            }

            Guid organizationId = organizationService.AddOrganization(organization.BusinessId, organization.Oid, organization.Type, organization.MunicipalityCode,
                organization.Names, organization.Descriptions, organization.ValidFrom, organization.ValidTo, organization.NameAbbreviations, organization.CanBeTransferredToFsc);
            return Ok(organizationId);
        }

        [HttpPost]
        [PostRoute("organizations/{parentOrganizationId}/organizations")]
        public IHttpActionResult AddSubOrganization(Guid parentOrganizationId, BasicInformation organization)
        {
            if (organization == null)
            {
                throw new ArgumentNullException("organization");
            }

            Guid subOrganizationId = organizationService.AddSubOrganization(parentOrganizationId, organization.BusinessId, organization.Oid, organization.Type, 
                organization.MunicipalityCode, organization.Names, organization.Descriptions, organization.ValidFrom, organization.ValidTo, organization.NameAbbreviations, organization.CanBeTransferredToFsc);
            return Ok(subOrganizationId);
        }

        [HttpPut]
        [PutRoute("organizations/{organizationId}/basicinformation")]
        public IHttpActionResult SetOrganizationBasicInformation(Guid organizationId, BasicInformation information)
        {
            if (information == null)
            {
                throw new ArgumentNullException("information");
            }
            organizationService.SetOrganizationBasicInformation(organizationId, information.BusinessId, information.Oid, information.Names, information.Descriptions, 
                information.Type, information.MunicipalityCode, information.ValidFrom, information.ValidTo, information.NameAbbreviations, information.CanBeTransferredToFsc);
            return Ok();
        }

        [HttpPut]
        [PutRoute("organizations/{organizationId}/contactinformation")]
        public IHttpActionResult SetOrganizationContactInformation(Guid organizationId, ContactInformation information)
        {
            if (information == null)
            {
                throw new ArgumentNullException("information");
            }
            organizationService.SetOrganizationContactInformation(organizationId, information.PhoneNumber, information.PhoneCallChargeType, information.PhoneCallChargeInfos,
                information.EmailAddress, information.WebPages, information.HomepageUrls);
            return Ok();
        }

        [HttpPut]
        [PutRoute("organizations/{organizationId}/visitingaddress")]
        public IHttpActionResult SetOrganizationVisitingAddress(Guid organizationId, VisitingAddress address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }
            organizationService.SetOrganizationVisitingAddress(organizationId, address.StreetAddresses, address.PostalCode, address.PostalDistricts, address.Qualifiers);
            return Ok();
        }

        [HttpPut]
        [PutRoute("organizations/{organizationId}/postaladdresses")]
        public IHttpActionResult SetOrganizationPostalAddresses(Guid organizationId, PostalAddresses addresses)
        {
            if (addresses == null)
            {
                throw new ArgumentNullException("addresses");
            }
            organizationService.SetOrganizationPostalAddresses(organizationId, addresses.UseVisitingAddress, addresses.StreetAddresses, addresses.StreetAddressPostalCode,
                addresses.StreetAddressPostalDistricts, addresses.PostOfficeBox, addresses.PostOfficeBoxAddressPostalCode, addresses.PostOfficeBoxAddressPostalDistricts);
            return Ok();
        }

        [HttpDelete]
        [DeleteRoute("organizations/{organizationId}")]
        public IHttpActionResult DeleteOrganization(Guid organizationId)
        {
            organizationService.RemoveOrganization(organizationId);
            return Ok();
        }

        [HttpPut]
        [PutRoute("organizations/{organizationId}/deactivate")]
        public IHttpActionResult DeactivateOrganization(Guid organizationId)    
        {
            organizationService.DeactivateOrganization(organizationId);
            return Ok();
        }
    }
}