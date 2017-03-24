using System;
using System.Collections.Generic;
using OrganizationRegister.Application.Location;

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
        IReadOnlyCollection<IOrganizationName> GetOrganizationsForOrganization(Guid rootOrganizationId);
        IHierarchicalOrganization GetHierarchicalOrganization(Guid id);

        IReadOnlyCollection<IOrganizationName> GetMunicipalMainOrganizations(int municipalityCode);


        IReadOnlyCollection<IOrganizationListItem> GetOrganizationListForOrganization(Guid organizationId);



        void SaveChanges();
        IOrganization GetOrganization(Guid id);
        void UpdateOrganizationBasicInformation(Guid id, IBasicInformation information, bool allowExistingBusinessId);
        void UpdateOrganizationContactInformation(Guid id, IContactInformation information);
        void UpdateOrganizationVisitingAddress(Guid id, IVisitingAddress address);
        void UpdateOrganizationPostalAddresses(Guid id, IPostalAddress addresses);
        bool HasActiveOrganization(string businessId, Guid? excludedOrganizationId);
        void RemoveOrganization(Guid id);
        void DeactivateOrganization(Guid id);
        IOrganizationName GetOrganizationName(Guid id);
    }
}
