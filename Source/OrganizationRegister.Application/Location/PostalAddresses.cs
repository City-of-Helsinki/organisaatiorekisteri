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

        public void Set(bool useVisitingAddress, IReadOnlyCollection<LocalizedText> streetAddresses, string streetAddressPostalCode, 
            IReadOnlyCollection<LocalizedText> streetAddressPostalDistricts, string postOfficeBox, string postOfficeBoxAddressPostalCode,
            IReadOnlyCollection<LocalizedText> postOfficeBoxAddressPostalDistricts)
        {
            UseVisitingAddress = useVisitingAddress;
            StreetAddress = StreetAddress.Create(languageCodes, streetAddresses, streetAddressPostalCode, streetAddressPostalDistricts);
            PostOfficeBoxAddress = PostOfficeBoxAddress.Create(languageCodes, postOfficeBox, postOfficeBoxAddressPostalCode, postOfficeBoxAddressPostalDistricts);

            if (UseVisitingAddress && StreetAddress.IsDefined)
            {
                throw new ArgumentException("Cannot use both a separate street address and the visiting address as postal addresses.");
            }
            if (PostOfficeBoxAddress.IsDefined && (StreetAddress.IsDefined || UseVisitingAddress))
            {
                throw new InvalidOperationException("Cannot use both post office box and street address postal addresses.");
            }
        }
    }
}
