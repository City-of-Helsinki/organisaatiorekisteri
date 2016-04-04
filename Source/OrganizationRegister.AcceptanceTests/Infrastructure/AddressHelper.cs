using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Application.Location;
using OrganizationRegister.Common;

namespace OrganizationRegister.AcceptanceTests.Infrastructure
{
    internal class AddressHelper
    {
        public static void AssertAddress(StreetAddress result, IReadOnlyCollection<LocalizedText> expectedStreetAddresses, string expectedPostalCode,
            IReadOnlyCollection<LocalizedText> expectedPostalDistricts)
        {
            Assert.AreEqual(expectedPostalCode, result.PostalCode);
            AssertStreetAddresses(result, expectedStreetAddresses);
            AssertPostalDistricts(result, expectedPostalDistricts);
        }

        public static void AssertAddress(PostOfficeBoxAddress result, string expectedPostOfficeBox, string expectedPostalCode, IReadOnlyCollection<LocalizedText> expectedPostalDistricts)
        {
            Assert.AreEqual(expectedPostOfficeBox, result.PostOfficeBox);
            Assert.AreEqual(expectedPostalCode, result.PostalCode);
            AssertPostalDistricts(result, expectedPostalDistricts);
        }

        public static void AssertUndefinedAddress(StreetAddress result)
        {
            Assert.IsNull(result.PostalCode);
            Assert.IsFalse(result.StreetAddresses.Any());
            Assert.IsFalse(result.PostalDistricts.Any());
        }

        public static void AssertUndefinedAddress(PostOfficeBoxAddress result)
        {
            Assert.IsNull(result.PostOfficeBox);
            Assert.IsNull(result.PostalCode);
            Assert.IsFalse(result.PostalDistricts.Any());
        }

        private static void AssertStreetAddresses(StreetAddress result, IReadOnlyCollection<LocalizedText> expectedStreetAddresses)
        {
            Assert.AreEqual(expectedStreetAddresses.Count, result.StreetAddresses.Count());
            foreach (LocalizedText streetAddress in expectedStreetAddresses)
            {
                Assert.IsTrue(result.StreetAddresses.Any(l => l.Equals(streetAddress)));
            }
        }

        private static void AssertPostalDistricts(Address result, IReadOnlyCollection<LocalizedText> expectedPostalDistricts)
        {
            Assert.AreEqual(expectedPostalDistricts.Count, result.PostalDistricts.Count());
            foreach (LocalizedText postalDistrict in expectedPostalDistricts)
            {
                Assert.IsTrue(result.PostalDistricts.Any(l => l.Equals(postalDistrict)));
            }
        }
    }
}
