using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Application.Location;
using OrganizationRegister.Common;
using OrganizationRegister.Store.CodeFirst;
using Address = OrganizationRegister.Store.CodeFirst.Model.Address;

namespace OrganizationRegister.Store.Tests.Model
{
    [TestClass]
    public class AddressTests
    {
        private Address sut;
        private IStoreContext context;

        [TestInitialize]
        public void Setup()
        {
            context = Substitute.For<IStoreContext>();
            sut = new Address();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingStreetAddressDataForLanguageNotInDataLanguages()
        {
            const string languageCode = "fi";
            StreetAddress address = StreetAddress.Create(new List<string> { languageCode }, new List<LocalizedText> { new LocalizedText(languageCode, "Katu 1") }, "13245",
                new List<LocalizedText> { new LocalizedText(languageCode, "city")} );
            context.GetDataLanguage(languageCode).Returns(x => { throw new ArgumentException(); });

            sut.AddAddress(address, context);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingPostOfficeBoxAddressDataForLanguageNotInDataLanguages()
        {
            const string languageCode = "fi";
            PostOfficeBoxAddress address = PostOfficeBoxAddress.Create(new List<string> { languageCode }, "10", "13241",
                new List<LocalizedText> { new LocalizedText(languageCode, "city") });
            context.GetDataLanguage(languageCode).Returns(x => { throw new ArgumentException(); });

            sut.AddAddress(address, context);
        }
    }
}
