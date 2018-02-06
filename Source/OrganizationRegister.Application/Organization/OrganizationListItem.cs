using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Organization
{
    internal class OrganizationListItem : OrganizationName, IOrganizationListItem
    {
       
        public OrganizationListItem(Guid id, Guid? parentId, IEnumerable<LocalizedText> names, string type, bool canBeTransferredToFsc, bool canBeResponsibleDeptForService)

            : base(id, names)
        {
            ParentId = parentId;
            Type = type;
            CanBeTransferredToFsc = canBeTransferredToFsc;
            CanBeResponsibleDeptForService = canBeResponsibleDeptForService;

        }

        public Guid? ParentId { get; set; }

        public string Type { get; }

        public bool CanBeTransferredToFsc { get; }

        public bool CanBeResponsibleDeptForService { get; }

        
    }
}