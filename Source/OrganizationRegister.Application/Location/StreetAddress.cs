using System;
using System.Collections.Generic;
using OrganizationRegister.Application.Localization;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Location
{
    public class StreetAddress : Address
    {
        private readonly LocalizedSingleTexts streetAddresses;

        private StreetAddress(IReadOnlyCollection<string> languageCodes)
            : base(languageCodes)
        {
            streetAddresses = new LocalizedSingleTexts();
        }

        public static StreetAddress Create(IReadOnlyCollection<string> languageCodes, IReadOnlyCollection<LocalizedText> streetAddresses, string postalCode, 
            IReadOnlyCollection<LocalizedText> postalDistricts)
        {
            if (!AreAllAddressPartsDefined(streetAddresses, postalCode, postalDistricts) && !IsNoAddressPartDefined(streetAddresses, postalCode, postalDistricts))
            {
                throw new ArgumentException("All or none of the address parts must be given.");
            }

            var address = new StreetAddress(languageCodes)
            {
                postalCode = string.IsNullOrWhiteSpace(postalCode) ? null : Affecto.Identifiers.Finnish.PostalCode.Create(postalCode)
            };

            address.SetLocalizedTexts(address.streetAddresses, streetAddresses);
            address.SetLocalizedTexts(address.postalDistricts, postalDistricts);
            return address;
        }

        public IEnumerable<LocalizedText> StreetAddresses
        {
            get { return streetAddresses; }
        }

        public string GetStreetAddress(string languageCode)
        {
            return streetAddresses.GetValue(languageCode);
        }

        private static bool IsNoAddressPartDefined(IEnumerable<LocalizedText> streetAddresses, string postalCode, IEnumerable<LocalizedText> postalDistricts)
        {
            return !HasItems(streetAddresses) && string.IsNullOrWhiteSpace(postalCode) && !HasItems(postalDistricts);
        }

        private static bool AreAllAddressPartsDefined(IEnumerable<LocalizedText> streetAddresses, string postalCode, IEnumerable<LocalizedText> postalDistricts)
        {
            return HasItems(streetAddresses) && !string.IsNullOrWhiteSpace(postalCode) && HasItems(postalDistricts);
        }
    }
}
