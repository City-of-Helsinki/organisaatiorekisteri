using System;
using System.Collections.Generic;
using OrganizationRegister.Application.User;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Organization
{

    public interface IAuthorizationInformation
    {
        IEnumerable<AuthorizationGroup> AuthorizationGroups { get; }
    }

    //PM 20.4.2021
    public interface IAuthorizationGroup 
    {
        string Name { get; set; }
        Guid RoleId { get; set; }
        Guid? GroupId { get; set; }
        Guid? OrganizationId { get; set; }
    }
}