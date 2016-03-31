using System;
using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Organization
{
    public class HierarchicalOrganization
    {
        public Guid Id { get; set; }
        public IEnumerable<LocalizedText> Names { get; set; }
        public IEnumerable<HierarchicalOrganization> SubOrganizations { get; set; }
    }
}