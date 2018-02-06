using System;
using System.Collections.Generic;

namespace OrganizationRegister.Application.Organization
{
    public interface IOrganizationListItem : IOrganizationName
    {

        Guid? ParentId { get; set; }

        string Type { get; }

        bool CanBeTransferredToFsc { get; }

        bool CanBeResponsibleDeptForService { get; }

    }
}