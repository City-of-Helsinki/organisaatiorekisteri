using System;
using System.Collections.Generic;

namespace OrganizationRegister.Application.Organization
{
    public interface IOrganizationListItem : IOrganizationName
    {
        string Type { get; }

        bool CanBeTransferredToFsc { get; }
       
    }
}