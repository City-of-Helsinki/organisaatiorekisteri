using System;
using System.Collections.Generic;
using OrganizationRegister.Application.Location;
using System.Linq;

namespace OrganizationRegister.Application.Organization
{
    public interface IOrganizationRepository
    {
        void AddOrganizationAndSave(IOrganization organization);
        void AddSubOrganizationAndSave(Guid parentOrganizationId, IOrganization organization);
        IReadOnlyCollection<IHierarchicalOrganization> GetOrganizationHierarchy();
        IReadOnlyCollection<IHierarchicalOrganization> GetOrganizationHierarchy(bool includeFutureOrganizations);
        IReadOnlyCollection<IHierarchicalOrganization> GetOrganizationHierarchyForOrganization(Guid? rootOrganizationId, bool includeFutureOrganizations = false);
        IReadOnlyCollection<IHierarchicalOrganization> GetCompleteOrganizationHierarchyForOrganization(Guid rootOrganizationId);
        IReadOnlyCollection<IOrganizationName> GetOrganizations();
        IReadOnlyCollection<IOrganizationName> GetMainOrganizations();
        IReadOnlyCollection<IOrganizationName> GetMainOrganizationsNames();
     
        IHierarchicalOrganization GetHierarchicalOrganization(Guid id);

        IReadOnlyCollection<IOrganizationName> GetMunicipalMainOrganizations(int municipalityCode);


        IReadOnlyCollection<IOrganizationListItem> GetOrganizationListForOrganization(Guid organizationId);


        IReadOnlyCollection<IOrganizationListItem> GetOrganizationListForGroup(Guid groupId);

        IReadOnlyCollection<IOrganizationListItem> GetMainOrganizationList();

        IReadOnlyCollection<IOrganizationListItem> GetMainOrganizationList(string businessId);

        void SaveChanges();
        IOrganization GetOrganization(Guid id);
        void UpdateOrganizationBasicInformation(Guid id, IBasicInformation information, bool allowExistingBusinessId);
        void UpdateOrganizationContactInformation(Guid id, IContactInformation information);
        void UpdateOrganizationVisitingAddress(Guid id, IVisitingAddress address);
        void UpdateOrganizationPostalAddresses(Guid id, IPostalAddress addresses);
        bool HasActiveOrganization(string businessId, Guid? excludedOrganizationId);

        void UpdateOrganizationAuthorizationtInformation(Guid id, IAuthorizationInformation information);

        void RemoveOrganization(Guid id);
        void DeactivateOrganization(Guid id);
        IOrganizationName GetOrganizationName(Guid id);

        //PM 20.4.2021
        List<Common.AuthorizationGroup> GetAuthorizationGroups();

        List<IOrganization> GetOrganizationsByPtvId(string ptvId);

    }
}
