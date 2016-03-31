using System;
using OrganizationRegister.Application.Localization;

namespace OrganizationRegister.Application.Organization
{
    public interface IOrganizationName
    {
        Guid Id { get; }
        LocalizedSingleTexts Names { get; }
    }
}
