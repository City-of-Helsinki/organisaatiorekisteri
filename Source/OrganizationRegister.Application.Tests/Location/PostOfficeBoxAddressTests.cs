using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Application.Location;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Tests.Location
{
    [TestClass]
    public class PostOfficeBoxAddressTests
    {
        private PostOfficeBoxAddress sut;
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
            sut = PostOfficeBoxAddress.Create(new List<string>(), null, null, null);
        }

        [TestMethod]
        public void PostalCodeCanBeNull()
        {
            sut = CreateSut();

            Assert.IsNull(sut.PostalCode);
        }

        [TestMethod]
        public void PostalDistrictsCanBeEmpty()
        {
            sut = CreateSut();

            Assert.IsFalse(sut.PostalDistricts.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingPostOfficeBoxAndPostalCode()
        {
            PostOfficeBoxAddress.Create(languageCodes, "10", "25101", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingPostalCodeAndPostalDistricts()
        {
            PostOfficeBoxAddress.Create(languageCodes, null, "25101", new List<LocalizedText> { new LocalizedText("fi", "kaupunki") });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingOnlyPostOfficeBox()
        {
            PostOfficeBoxAddress.Create(languageCodes, "10", null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingOnlyPostalCode()
        {
            PostOfficeBoxAddress.Create(languageCodes, null, "20611", new List<LocalizedText>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingOnlyPostalDistricts()
        {
            StreetAddress.Create(languageCodes, null, null, new List<LocalizedText> { new LocalizedText("fi", "Kunta") });
        }

        private PostOfficeBoxAddress CreateSut()
        {
            return PostOfficeBoxAddress.Create(languageCodes, null, null, null);
        }
    }
}
