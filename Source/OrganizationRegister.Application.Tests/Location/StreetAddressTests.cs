using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Application.Location;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Tests.Location
{
    [TestClass]
    public class StreetAddressTests
    {
        private StreetAddress sut;
        private List<string> languageCodes;

        [TestInitialize]
        public void Setup()
        {
            languageCodes = new List<string> { "fi", "en" };
        }
            
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThereMustBeAtLeastOneLanguageCode()
        {
            sut = StreetAddress.Create(new List<string>(), null, null, null);
        }

        [TestMethod]
        public void PostalCodeCanBeNull()
        {
            sut = CreateSut();

            Assert.IsNull(sut.PostalCode);
        }

        [TestMethod]
        public void StreetAddressesAreEmptyAfterInitialization()
        {
            sut = CreateSut();

            Assert.IsFalse(sut.StreetAddresses.Any());
        }

        [TestMethod]
        public void PostalDistrictsAreEmptyAfterInitialization()
        {
            sut = CreateSut();

            Assert.IsFalse(sut.PostalDistricts.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingStreetAddressAndPostalCode()
        {
            StreetAddress.Create(languageCodes, new List<LocalizedText> { new LocalizedText("fi", "Katu") }, "25100", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingPostalCodeAndPostalDistricts()
        {
            StreetAddress.Create(languageCodes, new List<LocalizedText>(), "25100", new List<LocalizedText> { new LocalizedText("fi", "kaupunki") });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingOnlyStreetAddress()
        {
            StreetAddress.Create(languageCodes, new List<LocalizedText> { new LocalizedText("fi", "Katu") }, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingOnlyPostalCode()
        {
            StreetAddress.Create(languageCodes, new List<LocalizedText>(), "20610", new List<LocalizedText>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingOnlyPostalDistricts()
        {
            StreetAddress.Create(languageCodes, null, null, new List<LocalizedText> { new LocalizedText("fi", "Kunta") });
        }

        private StreetAddress CreateSut()
        {
            return StreetAddress.Create(languageCodes, null, null, null);
        }
    }
}
