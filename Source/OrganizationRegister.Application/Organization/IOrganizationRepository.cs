using System;
using System.Collections.Generic;
using OrganizationRegister.Application.Location;

namespace OrganizationRegister.Application.Organization
{
    public interface IOrganizationRepository
    {
        void AddOrganizationAndSave(IOrganization organization);
        void AddSubOrganizationAndSave(Guid parentOrganizationId, IOrganization organization);
        IReadOnlyCollection<IHierarchicalOrganization> GetActiveOrganizationHierarchy();
        IReadOnlyCollection<IHierarchicalOrganization> GetActiveOrganizationHierarchyForRootOrganization(Guid? rootOrganizationId, bool includeFutureOrganizations = false);
        IReadOnlyCollection<IOrganizationName> GetActiveOrganizations();
        IReadOnlyCollection<IOrganizationName> GetActiveMainOrganizations();
        IReadOnlyCollection<IOrganizationName> GetMainOrganizations();
        IHierarchicalOrganization GetHierarchicalOrganization(Guid id);
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
