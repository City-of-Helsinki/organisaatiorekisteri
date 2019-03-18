using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OrganizationRegister.Application.Settings;
using OrganizationRegister.Common;
using Affecto.Authentication.Claims;
using OrganizationRegister.Application.User;
using OrganizationRegister.Common.User;

namespace OrganizationRegister.Application.Organization
{
    internal class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository organizationRepository;
        private readonly ISettingsRepository settingsRepository;
        private readonly IAuthenticatedUserContext userContext;

       

        public OrganizationService(IOrganizationRepository organizationRepository, ISettingsRepository settingsRepository, IAuthenticatedUserContext userContext = null)
        {
            this.organizationRepository = organizationRepository ?? throw new ArgumentNullException("organizationRepository");
            this.settingsRepository = settingsRepository ?? throw new ArgumentNullException("settingsRepository");
           
            this.userContext = userContext;
        }

        public Guid AddOrganization(string businessId, string oid, string type, string municipalityCode, IEnumerable<LocalizedText> names, IEnumerable<LocalizedText> descriptions, 
            DateTime? validFrom, DateTime? validTo, IEnumerable<LocalizedText> nameAbbreviations, bool canBeTransferredToFsc, bool canBeResponsibleDeptForService)
        {
            CheckAddOrganizationPermission();

            IReadOnlyCollection<string> languageCodes = settingsRepository.GetDataLanguageCodes();
            // TODO: Wrap id generation into a service
            var id = Guid.NewGuid();
            var organization = new Organization(id, businessId, oid, type, municipalityCode, names, languageCodes, canBeTransferredToFsc, canBeResponsibleDeptForService) { Descriptions = descriptions, NameAbbreviations = nameAbbreviations, HomepageUrls = null};
            organization.SetValidity(validFrom, validTo);
            organizationRepository.AddOrganizationAndSave(organization);
            return id;
        }

        public Guid AddSubOrganization(Guid parentOrganizationId, string businessId, string oid, string type, string municipalityCode, IEnumerable<LocalizedText> names,
            IEnumerable<LocalizedText> descriptions, DateTime? validFrom, DateTime? validTo, IEnumerable<LocalizedText> nameAbbreviations, bool canBeTransferredToFsc, bool canBeResponsibleDeptForService)
        {
            CheckManageOrganizationPermission(parentOrganizationId);

            IReadOnlyCollection<string> languageCodes = settingsRepository.GetDataLanguageCodes();
            // TODO: Wrap id generation into a service
            var id = Guid.NewGuid();
            var organization = new SubOrganization(id, businessId, oid, type, municipalityCode, names, languageCodes, canBeTransferredToFsc, canBeResponsibleDeptForService) { Descriptions = descriptions, NameAbbreviations = nameAbbreviations, HomepageUrls = null };
            organization.SetValidity(validFrom, validTo);
            organizationRepository.AddSubOrganizationAndSave(parentOrganizationId, organization);
            return id;
        }

        public IEnumerable<IHierarchicalOrganization> GetOrganizationHierarchy()
        {
            return organizationRepository.GetOrganizationHierarchy();
        }

        public IEnumerable<IHierarchicalOrganization> GetOrganizationsAsFlatlist(string searchTerm, Guid? organizationId)
        {
            // TODO : repository methods for searching organizations
            IEnumerable<IHierarchicalOrganization> orgs; 
            if (organizationId != null)
            {
                orgs = organizationRepository.GetCompleteOrganizationHierarchyForOrganization((Guid)organizationId).Flatten(o => o.SubOrganizations);
            }
            else
            {
                orgs = organizationRepository.GetOrganizationHierarchy().Flatten(o => o.SubOrganizations);
            }

            return from org in orgs
                   where org.Names.Any(x => x.LocalizedValue!= null && x.LocalizedValue.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                                select org;

        }


        public IEnumerable<IOrganizationListItem> GetGroupOrganizationsAsFlatlist(string groups)
        {
            if (string.IsNullOrWhiteSpace(groups))
            {
                throw new ArgumentNullException("groupIds");
            }

            // groupids: 11112222-3333-4444-5555-666677778888;22223333-4444-5555-6666-777788889999;...
            var ids = groups.Split(';').ToList();
            List<IOrganizationListItem> orgs = new List<IOrganizationListItem>();
            if (ids.Any())
            {
                ids.ForEach(id =>
                {
                    Guid groupId = Guid.Parse(id);
                    var parentOrgs = organizationRepository.GetOrganizationListForGroup(groupId);

                    foreach (var parentOrg in parentOrgs)
                    {
                        orgs.AddRange(organizationRepository.GetOrganizationListForOrganization(parentOrg.Id));
                    }

                });

                orgs = orgs.Distinct().ToList();
            }

            return orgs;

        }

        public IOrganizationListContainer GetGroupOrganizationsAndRoles(string groups)
        {
            if (string.IsNullOrWhiteSpace(groups))
            {
                throw new ArgumentNullException("groupIds");
            }
            
            var peruskayttajaRoleGuid = Guid.Parse("0ccf78ea-10ea-4b00-a16b-9776c77ca46f");
            var esteettomyysRoleGuid = Guid.Parse("919c81e2-db6e-4f00-92ed-780b756ebf03");

            // groups contains ;-delimited guids: 11112222-3333-4444-5555-666677778888;22223333-4444-5555-6666-777788889999;...
            var ids = groups.Split(';').ToList();
            List<IOrganizationListItem> peruskayttajaOrgs = new List<IOrganizationListItem>();
            List<IOrganizationListItem> esteettomyysOrgs = new List<IOrganizationListItem>();
            if (ids.Any())
            {
                ids.ForEach(id =>
                {
                    Guid groupId = Guid.Parse(id);

                    var parentOrgs = organizationRepository.GetOrganizationListForGroupAndRole(groupId, peruskayttajaRoleGuid);
                    foreach (var parentOrg in parentOrgs)
                    {
                        peruskayttajaOrgs.AddRange(organizationRepository.GetOrganizationListForOrganization(parentOrg.Id));
                    }

                    parentOrgs = organizationRepository.GetOrganizationListForGroupAndRole(groupId, esteettomyysRoleGuid);
                    foreach (var parentOrg in parentOrgs)
                    {
                        esteettomyysOrgs.AddRange(organizationRepository.GetOrganizationListForOrganization(parentOrg.Id));
                    }


                });

                //TODO: Get distinct organizations
                peruskayttajaOrgs = peruskayttajaOrgs.Distinct().ToList();
                esteettomyysOrgs = esteettomyysOrgs.Distinct().ToList();

            }

            var orgListContainer = new OrganizationListContainer(peruskayttajaOrgs, esteettomyysOrgs);
            return orgListContainer;

        }


        public IEnumerable<IOrganizationListItem> GetOrganizationListForMunicipality(int rootMunicipalityCode)
        {
            var orgs = new List<IOrganizationListItem>();
            foreach (var org in organizationRepository.GetMunicipalMainOrganizations(rootMunicipalityCode))
            {
                orgs.AddRange(organizationRepository.GetOrganizationListForOrganization(org.Id));
            }
            return orgs;
        }

        public IEnumerable<IOrganizationListItem> GetOrganizationListForOrganization(Guid organizationId)
        {
            return organizationRepository.GetOrganizationListForOrganization(organizationId);
        }
        public IEnumerable<IOrganizationListItem> GetOrganizationListForOrganization(string businessId)
        {
            var orgs = new List<IOrganizationListItem>();
            foreach (var org in organizationRepository.GetMainOrganizationList(businessId))
            {
                orgs.AddRange(organizationRepository.GetOrganizationListForOrganization(org.Id));
            }
            return orgs;
        }

        public IEnumerable<IOrganizationListItem> GetMainOrganizationList()
        {
            return organizationRepository.GetMainOrganizationList();
        }

        public IEnumerable<IHierarchicalOrganization> GetOrganizationHierarchy(bool includeFutureOrganizations)
        {
            return organizationRepository.GetOrganizationHierarchy(includeFutureOrganizations);
        }

        public IEnumerable<IHierarchicalOrganization> GetOrganizationHierarchyForOrganization(Guid? organizationId, bool includeFutureOrganizations)
        {
            return organizationRepository.GetOrganizationHierarchyForOrganization(organizationId, includeFutureOrganizations);
        }

        public IEnumerable<IHierarchicalOrganization> GetCompleteOrganizationHierarchyForOrganization(Guid organizationId)
        {
            return organizationRepository.GetCompleteOrganizationHierarchyForOrganization(organizationId);
        }

        public IEnumerable<IOrganizationName> GetOrganizations()
        {
            return organizationRepository.GetOrganizations();
        }

        public IEnumerable<IOrganizationName> GetMainOrganizations()
        {
            return organizationRepository.GetMainOrganizations();
        }

        public IEnumerable<IOrganizationName> GetMainOrganizationsNames()
        {
            return organizationRepository.GetMainOrganizationsNames();
        }

        public IOrganization GetOrganization(Guid organizationId)
        {
            return organizationRepository.GetOrganization(organizationId);
        }

        public IOrganizationName GetOrganizationName(Guid organizationId)
        {
            return organizationRepository.GetOrganizationName(organizationId);
        }

        public void SetOrganizationBasicInformation(Guid organizationId, string businessId, string oid, IEnumerable<LocalizedText> names, IEnumerable<LocalizedText> descriptions, 
            string type, string municipalityCode, DateTime? validFrom, DateTime? validTo, IEnumerable<LocalizedText> nameAbbreviations, bool canBeTransferredToFsc, bool canBeResponsibleDeptForService)
        {
            CheckManageOrganizationPermission(organizationId);

            Organization organization = GetOrganization(organizationId) as Organization;
            organization.BusinessId = businessId;
            organization.Oid = oid;
            organization.Names = names;
            organization.Descriptions = descriptions;
            organization.NameAbbreviations = nameAbbreviations;
            organization.CanBeTransferredToFsc = canBeTransferredToFsc;
            organization.SetType(type, municipalityCode);
            organization.SetValidity(validFrom, validTo);
            organization.SetCanBeResponsibleDeptForService(canBeResponsibleDeptForService);
            organizationRepository.UpdateOrganizationBasicInformation(organizationId, organization, organization is SubOrganization);
            organizationRepository.SaveChanges();
        }

        public void SetOrganizationContactInformation(Guid organizationId, string phoneNumber, string callChargeType, IEnumerable<LocalizedText> callChargeInfos, string emailAddress, IEnumerable<WebPage> webSites, IEnumerable<LocalizedText> homepageUrls)
         {

            CheckManageOrganizationPermission(organizationId);

            Organization organization = GetOrganization(organizationId) as Organization;
            organization.SetCallInformation(phoneNumber, callChargeType, callChargeInfos);
            organization.EmailAddress = emailAddress;
            organization.WebPages = webSites;
            organization.HomepageUrls = homepageUrls;
            organizationRepository.UpdateOrganizationContactInformation(organizationId, organization);
            organizationRepository.SaveChanges();
        }

        public void SetOrganizationVisitingAddress(Guid organizationId, IEnumerable<LocalizedText> streetAddresses, string postalCode, IEnumerable<LocalizedText> postalDistricts, 
            IEnumerable<LocalizedText> qualifiers)
        {
            CheckManageOrganizationPermission(organizationId);

            Organization organization = GetOrganization(organizationId) as Organization;
            organization.SetVisitingAddress(streetAddresses, postalCode, postalDistricts);
            organization.VisitingAddressQualifiers = qualifiers;
            organizationRepository.UpdateOrganizationVisitingAddress(organizationId, organization);
            organizationRepository.SaveChanges();
        }

        public void SetOrganizationPostalAddresses(Guid organizationId, bool useVisitingAddress, IEnumerable<LocalizedText> streetAddresses, string streetAddressPostalCode,
            IEnumerable<LocalizedText> streetAddressPostalDistricts, string postOfficeBox, string postOfficeBoxAddressPostalCode, 
            IEnumerable<LocalizedText> postOfficeBoxAddressPostalDistricts)
        {
            CheckManageOrganizationPermission(organizationId);

            Organization organization = GetOrganization(organizationId) as Organization;
            organization.SetPostalAddress(useVisitingAddress, streetAddresses, streetAddressPostalCode, streetAddressPostalDistricts, postOfficeBox, 
                postOfficeBoxAddressPostalCode, postOfficeBoxAddressPostalDistricts);
            organizationRepository.UpdateOrganizationPostalAddresses(organizationId, organization);
            organizationRepository.SaveChanges();
        }


        public void SetOrganizationAuthorizationInformation(Guid organizationId, IEnumerable<AuthorizationGroup> groups)
        {
            CheckManageOrganizationPermission(organizationId);

           

            Organization organization = GetOrganization(organizationId) as Organization;
            organization.AuthorizationGroups = groups;
            organizationRepository.UpdateOrganizationAuthorizationtInformation(organizationId, organization);
            organizationRepository.SaveChanges();
        }



        public void RemoveOrganization(Guid organizationId)
        {
            CheckManageOrganizationPermission(organizationId);

            organizationRepository.RemoveOrganization(organizationId);
            organizationRepository.SaveChanges();
        }

        public void DeactivateOrganization(Guid organizationId)
        {
            CheckManageOrganizationPermission(organizationId);

            organizationRepository.DeactivateOrganization(organizationId);
            organizationRepository.SaveChanges();
        }



        private void CheckManageOrganizationPermission(Guid organizationId)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException("userContext");
            }

            Guid userOrganizationId = userContext.GetUserOrganizationId();
            var userOrgs = this.GetCompleteOrganizationHierarchyForOrganization(userOrganizationId).Flatten(o => o.SubOrganizations);
            userContext.CheckPermission(userOrgs.Any(o => o.Id == organizationId)
                ? Permissions.Organization.MaintenanceOfOwnOrganizationData : Permissions.Organization.MaintenanceOfAllOrganizationData);
        }

        private void CheckAddOrganizationPermission()
        {
            if (userContext == null)
            {
                throw new ArgumentNullException("userContext");
            }
            userContext.CheckPermission(Permissions.Organization.MaintenanceOfAllOrganizationData);
        }
    }
}
