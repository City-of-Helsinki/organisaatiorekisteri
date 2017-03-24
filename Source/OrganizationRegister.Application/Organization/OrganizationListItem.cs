using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Organization
{
    internal class OrganizationListItem : OrganizationName, IOrganizationListItem
    {
       
        public OrganizationListItem(Guid id, IEnumerable<LocalizedText> names, string type, bool canBeTransferredToFsc)
            : base(id, names)
        {
            Type = type;
            CanBeTransferredToFsc = canBeTransferredToFsc;
        }

        public string Type { get; }

        public bool CanBeTransferredToFsc { get; }

    }
}