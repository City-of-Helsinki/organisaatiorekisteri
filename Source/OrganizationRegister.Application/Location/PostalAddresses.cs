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
            UseVisitingAddress = useVisitingAddress;
            if (UseVisitingAddress && StreetAddress.IsDefined)
            {
                throw new ArgumentException("Cannot use both a separate street address and the visiting address as postal addresses.");
            }
            if (PostOfficeBoxAddress != null && PostOfficeBoxAddress.IsDefined && (StreetAddress.IsDefined || UseVisitingAddress))
            {
                throw new InvalidOperationException("Post office box postal address is already set. Cannot have both post office box and street address postal addresses.");
            }
        }

        public void Set(string postOfficeBox, string postalCode, List<LocalizedText> postalDistricts)
        {
            PostOfficeBoxAddress = PostOfficeBoxAddress.Create(languageCodes, postOfficeBox, postalCode, postalDistricts);
            if (((StreetAddress != null && StreetAddress.IsDefined) || UseVisitingAddress) && PostOfficeBoxAddress.IsDefined)
            {
                throw new InvalidOperationException("Street postal address is already set. Cannot have both post office box and street address postal addresses.");
            }
        }
    }
}
