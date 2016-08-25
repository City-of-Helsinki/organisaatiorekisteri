using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Application.Location;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Common;
using OrganizationRegister.Store.CodeFirst;
using OrganizationRegister.Store.CodeFirst.Model;
using Address = OrganizationRegister.Store.CodeFirst.Model.Address;
using WebPage = OrganizationRegister.Common.WebPage;

namespace OrganizationRegister.Store.Tests.Model
{
    [TestClass]
    public class OrganizationTests
    {
        private IStoreContext context;
        private Organization sut;

        [TestInitialize]
        public void Setup()
        {
            context = Substitute.For<IStoreContext>();
            sut = new Organization();
        }

        [TestMethod]
        public void RemovingAllWebPages()
        {
            const string webPageType = "type";
            IDbSet<CodeFirst.Model.WebPage> wwwAddresses = Substitute.For<IDbSet<CodeFirst.Model.WebPage>>();
            context.WebPages.Returns(wwwAddresses);
            context.GetWebPageType(webPageType).Returns(new WebPageType());
            sut.SetWebPages(new List<WebPage> { new WebPage("global", "something.com", webPageType), new WebPage("local", "something.uk", webPageType) }, context);

            sut.RemoveAllWebPages(context);

            context.WebPages.Received(2).Remove(Arg.Any<CodeFirst.Model.WebPage>());
        }

        [TestMethod]
        public void SettingWebPagesRemovesOldSites()
        {
            const string webPageType = "type";
            IDbSet<CodeFirst.Model.WebPage> wwwAddresses = Substitute.For<IDbSet<CodeFirst.Model.WebPage>>();
            context.WebPages.Returns(wwwAddresses);
            context.GetWebPageType(webPageType).Returns(new WebPageType());
            sut.SetWebPages(new List<WebPage> { new WebPage("global", "something.com", webPageType), new WebPage("local", "something.uk", webPageType) }, context);

            sut.SetWebPages(Enumerable.Empty<WebPage>(), context);

            context.WebPages.Received(2).Remove(Arg.Any<CodeFirst.Model.WebPage>());
        }

        [TestMethod]
        public void SettingEmailAddressRemovesOldAddress()
        {
            const string previousEmailAddress = "me@company.fi";
            IDbSet<EmailAddress> emailAddresses = Substitute.For<IDbSet<EmailAddress>>();
            context.EmailAddresses.Returns(emailAddresses);
            sut.SetEmailAddress(previousEmailAddress, context);

            sut.SetEmailAddress("me@company.com", context);

            emailAddresses.Received(1).Remove(Arg.Is<EmailAddress>(email => email.Email.Equals(previousEmailAddress)));
        }

        [TestMethod]
        public void SettingPhoneNumberRemovesOldNumber()
        {
            const string previousPhoneNumber = "112";
            IDbSet<PhoneNumber> phoneNumbers = Substitute.For<IDbSet<PhoneNumber>>();
            context.PhoneNumbers.Returns(phoneNumbers);
            sut.SetCallInformation(previousPhoneNumber, "ppm", context);

            sut.SetCallInformation("0100100", "ppm", context);

            phoneNumbers.Received(1).Remove(Arg.Is<PhoneNumber>(phoneNumber => phoneNumber.Number.Equals(previousPhoneNumber)));
        }

        [TestMethod]
        public void SettingVisitingAddressRemovesOldAddress()
        {
            IVisitingAddress visitingAddress = CreateVisitingAddressWithTwoLanguages();

            IDbSet<Address> addresses = Substitute.For<IDbSet<Address>>();
            context.Addresses.Returns(addresses);

            sut.SetVisitingAddress(visitingAddress, context);
            sut.SetVisitingAddress(visitingAddress, context);

            addresses.Received(1).Remove(Arg.Any<Address>());
        }

        [TestMethod]
        public void SettingPostalAddressesRemovesOldAddresses()
        {
            IPostalAddress postalAddress = CreatePostalAddressesWithTwoLanguages();

            IDbSet<Address> addresses = Substitute.For<IDbSet<Address>>();
            context.Addresses.Returns(addresses);

            sut.SetPostalAddresses(postalAddress, context);
            sut.SetPostalAddresses(postalAddress, context);

            addresses.Received(2).Remove(Arg.Any<Address>());
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void SettingBasicInformationDataForLanguageNotInDataLanguages()
        //{
        //    const string languageCode = "sv";
        //    IBasicInformation info = Substitute.For<IBasicInformation>();
        //    info.Names.Returns(new List<LocalizedText> { new LocalizedText(languageCode, "namn") });
        //    context.GetDataLanguage(languageCode).Returns(x => { throw new ArgumentException(); });

        //    sut.SetBasicInformation(info, context);
        //}

        private IPostalAddress CreatePostalAddressesWithTwoLanguages()
        {
            var postOfficeBoxAddress = PostOfficeBoxAddress.Create(new List<string> { "fi", "en" },
                "10", "54321", new List<LocalizedText> { new LocalizedText("fi", "Kaupunki"), new LocalizedText("en", "City") });
            var streetAddress = StreetAddress.Create(new List<string> { "fi", "en" },
                new List<LocalizedText> { new LocalizedText("fi", "Katu 1 A 2"), new LocalizedText("en", "Street 1 A 2") }, "54321",
                new List<LocalizedText> { new LocalizedText("fi", "Kaupunki"), new LocalizedText("en", "City") });

            IPostalAddress postalAddress = Substitute.For<IPostalAddress>();
            postalAddress.PostalPostOfficeBoxAddress.Returns(postOfficeBoxAddress);
            postalAddress.PostalStreetAddress.Returns(streetAddress);
            return postalAddress;
        }

        private static IVisitingAddress CreateVisitingAddressWithTwoLanguages()
        {
            var address = StreetAddress.Create(new List<string> { "fi", "en" },
                new List<LocalizedText> { new LocalizedText("fi", "Katu 1 A 2"), new LocalizedText("en", "Street 1 A 2") }, "12345",
                new List<LocalizedText> { new LocalizedText("fi", "Kaupunki"), new LocalizedText("en", "City") });

            IVisitingAddress visitingAddress = Substitute.For<IVisitingAddress>();
            visitingAddress.VisitingAddress.Returns(address);
            visitingAddress.GetVisitingAddressQualifier("fi").Returns("Sisäänkäynti on aina lukossa");
            visitingAddress.GetVisitingAddressQualifier("en").Returns((string)null);
            return visitingAddress;
        }
    }
}
