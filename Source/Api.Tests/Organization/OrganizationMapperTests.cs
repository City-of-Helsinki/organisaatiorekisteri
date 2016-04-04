using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Organization;
using OrganizationRegister.Application.Location;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Common;
using StreetAddress = OrganizationRegister.Application.Location.StreetAddress;

namespace OrganizationRegister.Api.Tests.Organization
{
    [TestClass]
    public class OrganizationMapperTests
    {
        private OrganizationMapper sut;
        private IOrganization source;
        private Api.Organization.Organization destination;

        [TestInitialize]
        public void Setup()
        {
            sut = new OrganizationMapper();
            source = Substitute.For<IOrganization>();
        }

        [TestMethod]
        public void IdIsMapped()
        {
            Guid id = Guid.NewGuid();
            source.Id.Returns(id);

            destination = sut.Map(source);

            Assert.AreEqual(id, destination.Id);
        }

        [TestMethod]
        public void NumericIdIsMapped()
        {
            const long id = 5;
            source.NumericId.Returns(id);

            destination = sut.Map(source);

            Assert.AreEqual(id, destination.NumericId);
        }

        [TestMethod]
        public void BusinessIdIsMapped()
        {
            const string businessId = "1234567-1";
            source.BusinessId.Returns(businessId);

            destination = sut.Map(source);

            Assert.AreEqual(businessId, destination.BusinessId);
        }

        [TestMethod]
        public void OidIsMapped()
        {
            const string oid = "12345";
            source.Oid.Returns(oid);

            destination = sut.Map(source);

            Assert.AreEqual(oid, destination.Oid);
        }

        [TestMethod]
        public void TypeIsMapped()
        {
            const string type = "City";
            source.Type.Returns(type);

            destination = sut.Map(source);

            Assert.AreEqual(type, destination.Type);
        }

        [TestMethod]
        public void NamesAreMapped()
        {
            IEnumerable<LocalizedText> names = new List<LocalizedText>
            {
                new LocalizedText("fi", "Yritys"),
                new LocalizedText("en", "Company")
            };
            source.Names.Returns(names);

            destination = sut.Map(source);

            Assert.AreEqual(names.Count(), destination.Names.Count());
            Assert.AreSame(names.First(), destination.Names.First());
            Assert.AreSame(names.Last(), destination.Names.Last());
        }

        [TestMethod]
        public void DescriptionsAreMapped()
        {
            IEnumerable<LocalizedText> descriptions = new List<LocalizedText>
            {
                new LocalizedText("fi", "Tekee softaa"),
                new LocalizedText("en", "SW company")
            };
            source.Descriptions.Returns(descriptions);

            destination = sut.Map(source);

            Assert.AreEqual(descriptions.Count(), destination.Descriptions.Count());
            Assert.AreSame(descriptions.First(), destination.Descriptions.First());
            Assert.AreSame(descriptions.Last(), destination.Descriptions.Last());
        }

        [TestMethod]
        public void MunicipalityCodeIsMapped()
        {
            const string municipalityCode = "12";
            source.MunicipalityCode.Returns(municipalityCode);

            destination = sut.Map(source);

            Assert.AreEqual(municipalityCode, destination.MunicipalityCode);
        }

        [TestMethod]
        public void PHoneNumberIsMapped()
        {
            const string phoneNumber = "0100100";
            source.PhoneNumber.Returns(phoneNumber);

            destination = sut.Map(source);

            Assert.AreEqual(phoneNumber, destination.PhoneNumber);
        }

        [TestMethod]
        public void PhoneCallFeeIsMapped()
        {
            const string phoneCallFee = "ppm";
            source.PhoneCallFee.Returns(phoneCallFee);

            destination = sut.Map(source);

            Assert.AreEqual(phoneCallFee, destination.PhoneCallFee);
        }

        [TestMethod]
        public void EmailAddressIsMapped()
        {
            const string email = "info@city.gov";
            source.EmailAddress.Returns(email);

            destination = sut.Map(source);

            Assert.AreEqual(email, destination.EmailAddress);
        }

        [TestMethod]
        public void WebPagesAreMapped()
        {
            IEnumerable<WebPage> webPages = new List<WebPage>
            {
                new WebPage("Front page", "www.company.fi", "type"),
                new WebPage("Contact information", "www.companty.fi/contacts", "type")
            };
            source.WebPages.Returns(webPages);

            destination = sut.Map(source);

            Assert.AreEqual(webPages.Count(), destination.WebPages.Count());
            Assert.AreSame(webPages.First(), destination.WebPages.First());
            Assert.AreSame(webPages.Last(), destination.WebPages.Last());
        }

        [TestMethod]
        public void VisitingAddressStreetIsMapped()
        {
            const string street = "katu 1 A 2";
            source.VisitingAddress.Returns(CreateStreetAddress(street, "12345", "Tampere"));

            destination = sut.Map(source);

            Assert.AreEqual(street, destination.VisitingAddress.StreetAddresses.Single().LocalizedValue);
        }

        [TestMethod]
        public void VisitingAddressPostalCodeIsMapped()
        {
            const string postalCode = "12345";
            source.VisitingAddress.Returns(CreateStreetAddress("some street", postalCode, "Tampere"));

            destination = sut.Map(source);

            Assert.AreEqual(postalCode, destination.VisitingAddress.PostalCode);
        }

        [TestMethod]
        public void VisitingAddressPostalDistrictIsMapped()
        {
            const string postalDistrict = "Tampere";
            source.VisitingAddress.Returns(CreateStreetAddress("some street", "12345", postalDistrict));

            destination = sut.Map(source);

            Assert.AreEqual(postalDistrict, destination.VisitingAddress.PostalDistricts.Single().LocalizedValue);
        }

        [TestMethod]
        public void VisitingAddressQualifiersAreMapped()
        {
            IEnumerable<LocalizedText> qualifiers = new List<LocalizedText>
            {
                new LocalizedText("fi", "Kadulla"),
                new LocalizedText("en", "On the street")
            };
            source.VisitingAddressQualifiers.Returns(qualifiers);

            destination = sut.Map(source);

            Assert.AreEqual(qualifiers.Count(), destination.VisitingAddressQualifiers.Count());
            Assert.AreSame(qualifiers.First(), destination.VisitingAddressQualifiers.First());
            Assert.AreSame(qualifiers.Last(), destination.VisitingAddressQualifiers.Last());
        }

        [TestMethod]
        public void PostalStreetAddressStreetIsMapped()
        {
            const string street = "katu 1 A 2";
            source.PostalStreetAddress.Returns(CreateStreetAddress(street, "12345", "Tampere"));

            destination = sut.Map(source);

            Assert.AreEqual(street, destination.PostalStreetAddress.StreetAddresses.Single().LocalizedValue);
        }

        [TestMethod]
        public void PostalStreetAddressPostalCodeIsMapped()
        {
            const string postalCode = "12345";
            source.PostalStreetAddress.Returns(CreateStreetAddress("some street", postalCode, "Tampere"));

            destination = sut.Map(source);

            Assert.AreEqual(postalCode, destination.PostalStreetAddress.PostalCode);
        }

        [TestMethod]
        public void PostalStreetAddressPostalDistrictIsMapped()
        {
            const string postalDistrict = "Tampere";
            source.PostalStreetAddress.Returns(CreateStreetAddress("some street", "12345", postalDistrict));

            destination = sut.Map(source);

            Assert.AreEqual(postalDistrict, destination.PostalStreetAddress.PostalDistricts.Single().LocalizedValue);
        }

        [TestMethod]
        public void PostalPostOfficeBoxAddressPostOfficeBoxIsMapped()
        {
            const string postOfficeBox = "12";
            source.PostalPostOfficeBoxAddress.Returns(CreatePostOfficeBoxAddress(postOfficeBox, "12341", "Tampere"));

            destination = sut.Map(source);

            Assert.AreEqual(postOfficeBox, destination.PostalPostOfficeBoxAddress.PostOfficeBox);
        }

        [TestMethod]
        public void PostalPostOfficeBoxAddressPostalCodeIsMapped()
        {
            const string postalCode = "12341";
            source.PostalPostOfficeBoxAddress.Returns(CreatePostOfficeBoxAddress("12", postalCode, "Tampere"));

            destination = sut.Map(source);

            Assert.AreEqual(postalCode, destination.PostalPostOfficeBoxAddress.PostalCode);
        }

        [TestMethod]
        public void PostalPostOfficeBoxAddressPostalDistrictIsMapped()
        {
            const string postalDistrict = "Tampere";
            source.PostalPostOfficeBoxAddress.Returns(CreatePostOfficeBoxAddress("12", "12341", postalDistrict));

            destination = sut.Map(source);

            Assert.AreEqual(postalDistrict, destination.PostalPostOfficeBoxAddress.PostalDistricts.Single().LocalizedValue);
        }

        [TestMethod]
        public void UseVisitingAddressAsPostalAddressIsMapped()
        {
            source.UseVisitingAddressAsPostalAddress.Returns(true);

            destination = sut.Map(source);

            Assert.IsTrue(destination.UseVisitingAddressAsPostalAddress);
        }

        [TestMethod]
        public void IsSubOrganizationIsMapped()
        {
            source.IsSubOrganization.Returns(true);

            destination = sut.Map(source);

            Assert.IsTrue(destination.IsSubOrganization);
        }

        private static StreetAddress CreateStreetAddress(string street, string postalCode, string postalDistrict)
        {
            return StreetAddress.Create(new List<string> { "fi" }, new List<LocalizedText> { new LocalizedText("fi", street) },
                postalCode, new List<LocalizedText> { new LocalizedText("fi", postalDistrict) });
        }

        private PostOfficeBoxAddress CreatePostOfficeBoxAddress(string postOfficeBox, string postalCode, string postalDistrict)
        {
            return PostOfficeBoxAddress.Create(new List<string> { "fi" }, postOfficeBox, postalCode, new List<LocalizedText> { new LocalizedText("fi", postalDistrict) });
        }
    }
}