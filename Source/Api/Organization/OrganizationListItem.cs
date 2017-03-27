using System;
using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Organization
{
    public class OrganizationListItem
    {
        public Guid Id { get; set; }
        public IEnumerable<LocalizedText> Names { get; set; }
        public string Type { get; set; }
        public bool CanBeTransferredToFsc { get; set; }
    }
}