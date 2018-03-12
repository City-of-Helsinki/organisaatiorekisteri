using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using OrganizationRegister.Api.Settings;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Organization
{
    public class AuthorizationInformation
    {
       
        public IEnumerable<AuthorizationGroup> AuthorizationGroups { get; set; }
        
    }



}