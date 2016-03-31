using System;
using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Location
{
    public class PostOfficeBoxAddress : Address
    {
        private PostOfficeBoxAddress(IReadOnlyCollection<string> languageCodes)
            : base(languageCodes)
        {
        }

        public string PostOfficeBox { get; private set; }

        public static PostOfficeBoxAddress Create(IReadOnlyCollection<string> languageCodes, string postOfficeBox, string postalCode, IReadOnlyCollection<LocalizedText> postalDistricts)
        {
            int postOfficeBoxNumber;
            if (!string.IsNullOrWhiteSpace(postOfficeBox) && !int.TryParse(postOfficeBox, out postOfficeBoxNumber))
            {
                throw new ArgumentException(string.Format("post office box '{0}' is not a number.", postOfficeBox), "postOfficeBox");
            }
            if (!AreAllAddressPartsDefined(postOfficeBox, postalCode, postalDistricts) && !IsNoAddressPartDefined(postOfficeBox, postalCode, postalDistricts))
            {
                throw new ArgumentException("All or none of the address parts must be given.");
            }

            var address = new PostOfficeBoxAddress(languageCodes)
            {
                postalCode = string.IsNullOrWhiteSpace(postalCode) ? null : Affecto.Identifiers.Finnish.PostOfficeBoxPostalCode.Create(postalCode),
                PostOfficeBox = postOfficeBox
            };

            address.SetLocalizedTexts(address.postalDistricts, postalDistricts);
            return address;
        }

        private static bool IsNoAddressPartDefined(string postOfficeBox, string postalCode, IReadOnlyCollection<LocalizedText> postalDistricts)
        {
            return string.IsNullOrWhiteSpace(postOfficeBox) && string.IsNullOrWhiteSpace(postalCode) && !HasItems(postalDistricts);
        }

        private static bool AreAllAddressPartsDefined(string postOfficeBox, string postalCode, IReadOnlyCollection<LocalizedText> postalDistricts)
        {
            return !string.IsNullOrWhiteSpace(postOfficeBox) && !string.IsNullOrWhiteSpace(postalCode) && HasItems(postalDistricts);
        }
    }
}
