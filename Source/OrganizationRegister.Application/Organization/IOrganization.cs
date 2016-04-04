using System;
using OrganizationRegister.Application.Location;

namespace OrganizationRegister.Application.Organization
{
    public interface IOrganization : IBasicInformation, IContactInformation, IVisitingAddress, IPostalAddress
    {
        Guid Id { get; }
        long NumericId { get; }
        bool IsSubOrganization { get; }
    }
}