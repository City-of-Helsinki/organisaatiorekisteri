using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Location
{
    internal class PostalAddresses
    {
        private readonly IReadOnlyCollection<string> languageCodes;

        public PostalAddresses(IEnumerable<string> languageCodes)
        {
            if (languageCodes == null)
            {
                throw new ArgumentNullException("languageCodes");
            }
            this.languageCodes = languageCodes.ToList();
        }

        public StreetAddress StreetAddress { get; private set; }
        public PostOfficeBoxAddress PostOfficeBoxAddress { get; private set; }
        public bool UseVisitingAddress { get; private set; }

        public void Set(IReadOnlyCollection<LocalizedText> streetAddresses, string postalCode, IReadOnlyCollection<LocalizedText> postalDistricts, bool useVisitingAddress)
        {
            StreetAddress = StreetAddress.Create(languageCodes, streetAddresses, postalCode, postalDistricts);
            if (useVisitingAddress && StreetAddress.IsDefined)
            {
                throw new ArgumentException("Cannot use both a separate street address and the visiting address as postal addresses.");
            }
            UseVisitingAddress = useVisitingAddress;
        }

        public void Set(string postOfficeBox, string postalCode, List<LocalizedText> postalDistricts)
        {
            PostOfficeBoxAddress = PostOfficeBoxAddress.Create(languageCodes, postOfficeBox, postalCode, postalDistricts);
        }
    }
}
