using System;
using System.Collections.Generic;
using Affecto.Identifiers.Finnish;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Organization
{
    internal class SubOrganization : Organization
    {
        public SubOrganization(Guid id, long numericId, string businessId, string oid, string type, int? municipalityCode, IEnumerable<LocalizedText> names,
            IEnumerable<string> languageCodes, bool canBeTransferredToFsc)
            : base(id, numericId, businessId, oid, type, municipalityCode, names, languageCodes, canBeTransferredToFsc)
        {
        }

        public SubOrganization(Guid id, string businessId, string oid, string type, string municipalityCode, IEnumerable<LocalizedText> names, 
            IEnumerable<string> languageCodes, bool canBeTransferredToFsc)
            : base(id, businessId, oid, type, municipalityCode, names, languageCodes, canBeTransferredToFsc)
        {
        }

        public override string BusinessId
        {
            get { return businessId == null ? null : businessId.ToString(); }
            set
            {
                businessId = null;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    businessId = BusinessIdentifier.Create(value);                    
                }
            }
        }
    }
}