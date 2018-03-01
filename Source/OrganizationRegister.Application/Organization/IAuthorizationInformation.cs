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
}