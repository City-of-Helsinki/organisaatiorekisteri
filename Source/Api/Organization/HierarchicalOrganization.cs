using System;
using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Organization
{
    public class HierarchicalOrganization
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public IEnumerable<LocalizedText> Names { get; set; }
        public string ValidFrom { get; set; }   //string, contains only date part in yyyy-MM-dd format
        public string ValidTo { get; set; }     //string, contains only date part in yyyy-MM-dd format
        public IEnumerable<HierarchicalOrganization> SubOrganizations { get; set; }
    }
}