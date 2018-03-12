using System;
using OrganizationRegister.Application.Location;

namespace OrganizationRegister.Application.Organization
{
    public interface IOrganization : IBasicInformation, IContactInformation, IVisitingAddress, IPostalAddress, IAuthorizationInformation
    {
        Guid Id { get; }
        long NumericId { get; }
        bool IsSubOrganization { get; }



    }
}