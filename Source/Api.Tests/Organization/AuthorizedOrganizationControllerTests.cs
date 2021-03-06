﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Location;
using OrganizationRegister.Api.Organization;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Application.User;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Tests.Organization
{
    [TestClass]
    public class AuthorizedOrganizationControllerTests : OrganizationControllerTests
    {
        private IOrganizationService organizationService;
        private IGroupService groupService;
        private AuthorizedOrganizationController sut;

        [TestInitialize]
        public void Setup()
        {
            SetupMappers();
            organizationService = Substitute.For<IOrganizationService>();
            groupService = Substitute.For<IGroupService>();
            sut = new AuthorizedOrganizationController(organizationService, new Lazy<IGroupService>(() => groupService), mapperFactory);
        }

        [TestMethod]
        public void CompanyOrganizationIsAddedWithoutOptionalDescriptionsAndValidity()
        {
            const string businessId = "1234567-8";
            const string englishName = "company";
            const string finnishName = "yritys";
            const string oid = "132456";
            const string type = "Liikelaitos";
            const string finnishLanguageCode = "fi";
            const string englishLanguageCode = "en";
            const bool canBeTransferredToFsc = false;
            const bool canBeResponsibleDeptForService = false;

            BasicInformation information = new BasicInformation
            {
                BusinessId = businessId,
                Names = new List<LocalizedText>
                {
                    new LocalizedText(finnishLanguageCode, finnishName), 
                    new LocalizedText(englishLanguageCode, englishName)
                },
                Type = type,
                Oid = oid,
                CanBeTransferredToFsc = canBeTransferredToFsc
            };

            sut.AddOrganization(information);

            organizationService.Received(1).AddOrganization(businessId, oid, type, null,
                Arg.Is<IEnumerable<LocalizedText>>(names => names.Count() == 2 && names.Any(name => name.LanguageCode.Equals(finnishLanguageCode) && name.LocalizedValue.Equals(finnishName))
                    && names.Any(name => name.LanguageCode.Equals(englishLanguageCode) && name.LocalizedValue.Equals(englishName))), Arg.Any<IEnumerable<LocalizedText>>(),
                    Arg.Any<DateTime?>(), Arg.Any<DateTime?>(), Arg.Any<IEnumerable<LocalizedText>>(), canBeTransferredToFsc, canBeResponsibleDeptForService);
        }

        [TestMethod]
        public void MunicipalityOrganizationIsAddedWithDescriptionsAndValidity()
        {
            const string businessId = "1234567-8";
            const string finnishDescription = "Suomen vanhin kaupunki";
            const string finnishName = "Turku";
            const string finnishAbbreviation = "TKU";
            const string municipalityCode = "091";
            const string oid = "132456";
            const string type = "Kunta";
            const string finnishLanguageCode = "fi";
            const bool canBeTransferredToFsc = false;
            const bool canBeResponsibleDeptForService = false;

            DateTime? validFrom = new DateTime(2015, 1, 1);
            DateTime? validTo = new DateTime(2016, 1, 1);

            BasicInformation information = new BasicInformation
            {
                BusinessId = businessId,
                Names = new List<LocalizedText>
                {
                    new LocalizedText(finnishLanguageCode, finnishName)
                },
                Descriptions = new List<LocalizedText>
                {
                    new LocalizedText(finnishLanguageCode, finnishDescription)
                },
                Type = type,
                MunicipalityCode = municipalityCode,
                Oid = oid,
                ValidFrom = validFrom,
                ValidTo = validTo,
                CanBeTransferredToFsc = canBeTransferredToFsc,
                NameAbbreviations = new List<LocalizedText>
                {
                    new LocalizedText(finnishLanguageCode, finnishAbbreviation)
                },
            };

            sut.AddOrganization(information);

            organizationService.Received(1).AddOrganization(businessId, oid, type, municipalityCode,
                Arg.Is<IEnumerable<LocalizedText>>(names => names.Single().Equals(new LocalizedText(finnishLanguageCode, finnishName))),
                Arg.Is<IEnumerable<LocalizedText>>(descriptions => descriptions.Single().Equals(new LocalizedText(finnishLanguageCode, finnishDescription))),
                validFrom, validTo,
                Arg.Is<IEnumerable<LocalizedText>>(
                    nameAbbreviations => nameAbbreviations.Single().Equals(new LocalizedText(finnishLanguageCode, finnishAbbreviation))), canBeTransferredToFsc, canBeResponsibleDeptForService);
        }

        [TestMethod]
        public void AddingOrganizationReturnOrganizationId()
        {
            Guid organizationId = Guid.NewGuid();

            BasicInformation company = new BasicInformation
            {
                BusinessId = "1234567-1",
                Names = new List<LocalizedText>
                {
                    new LocalizedText("fi", "firma")
                },
                Type = "Kolmas sektori",
            };
            organizationService.AddOrganization(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IEnumerable<LocalizedText>>(),
                Arg.Any<IEnumerable<LocalizedText>>(), Arg.Any<DateTime?>(), Arg.Any<DateTime?>(), Arg.Any<IEnumerable<LocalizedText>>(),false,false).Returns(organizationId);

            var result = sut.AddOrganization(company) as OkNegotiatedContentResult<Guid>;

            Assert.IsNotNull(result);
            Assert.AreEqual(organizationId, result.Content);
        }

        [TestMethod]
        public void CompanySubOrganizationIsAddedWithoutOptionalDescriptionsAndValidity()
        {
            Guid parentOrganizationId = Guid.NewGuid();
            const string businessId = "1234567-8";
            const string englishName = "company";
            const string finnishName = "yritys";
            const string oid = "132456";
            const string type = "Liikelaitos";
            const string finnishLanguageCode = "fi";
            const string englishLanguageCode = "en";
            const bool canBeTransferredToFsc = false;
            const bool canBeResponsibleDeptForService = false;


            BasicInformation information = new BasicInformation
            {
                BusinessId = businessId,
                Names = new List<LocalizedText>
                {
                    new LocalizedText(finnishLanguageCode, finnishName), 
                    new LocalizedText(englishLanguageCode, englishName)
                },
                Type = type,
                Oid = oid,
                CanBeTransferredToFsc = canBeTransferredToFsc
            };

            sut.AddSubOrganization(parentOrganizationId, information);

            organizationService.Received(1).AddSubOrganization(parentOrganizationId, businessId, oid, type, null,
                Arg.Is<IEnumerable<LocalizedText>>(names => names.Count() == 2 && names.Any(name => name.LanguageCode.Equals(finnishLanguageCode) && name.LocalizedValue.Equals(finnishName))
                    && names.Any(name => name.LanguageCode.Equals(englishLanguageCode) && name.LocalizedValue.Equals(englishName))), Arg.Any<IEnumerable<LocalizedText>>(),
                    Arg.Any<DateTime?>(), Arg.Any<DateTime?>(), Arg.Any<IEnumerable<LocalizedText>>(), canBeTransferredToFsc, canBeResponsibleDeptForService);
        }

        [TestMethod]
        public void MunicipalitySubOrganizationIsAddedWithDescriptionsAndValidity()
        {
            Guid parentOrganizationId = Guid.NewGuid();
            const string businessId = "1234567-8";
            const string finnishDescription = "Suomen vanhin kaupunki";
            const string finnishName = "Turku";
            const string finnishAbbreviation = "TKU";
            const string municipalityCode = "091";
            const string oid = "132456";
            const string type = "Kunta";
            const string finnishLanguageCode = "fi";
            const bool canBeTransferredToFsc = false;
            const bool canBeResponsibleDeptForService = false;

            DateTime? validFrom = DateTime.Today;
            DateTime? validTo = DateTime.Today.AddDays(1);

            BasicInformation information = new BasicInformation
            {
                BusinessId = businessId,
                Names = new List<LocalizedText>
                {
                    new LocalizedText(finnishLanguageCode, finnishName)
                },
                Descriptions = new List<LocalizedText>
                {
                    new LocalizedText(finnishLanguageCode, finnishDescription)
                },
                Type = type,
                MunicipalityCode = municipalityCode,
                Oid = oid,
                ValidFrom = validFrom,
                ValidTo = validTo,
                CanBeTransferredToFsc = canBeTransferredToFsc,
                NameAbbreviations = new List<LocalizedText>
                {
                    new LocalizedText(finnishLanguageCode, finnishAbbreviation)
                }
            };

            sut.AddSubOrganization(parentOrganizationId, information);

            organizationService.Received(1).AddSubOrganization(parentOrganizationId, businessId, oid, type, municipalityCode,
                Arg.Is<IEnumerable<LocalizedText>>(names => names.Single().Equals(new LocalizedText(finnishLanguageCode, finnishName))),
                Arg.Is<IEnumerable<LocalizedText>>(descriptions => descriptions.Single().Equals(new LocalizedText(finnishLanguageCode, finnishDescription))),
                validFrom, validTo, Arg.Is<IEnumerable<LocalizedText>>(
                    nameAbbreviations => nameAbbreviations.Single().Equals(new LocalizedText(finnishLanguageCode, finnishAbbreviation))), canBeTransferredToFsc, canBeResponsibleDeptForService);
        }

        [TestMethod]
        public void AddingSubOrganizationReturnOrganizationId()
        {
            Guid organizationId = Guid.NewGuid();

            BasicInformation company = new BasicInformation
            {
                BusinessId = "1234567-1",
                Names = new List<LocalizedText>
                {
                    new LocalizedText("fi", "firma")
                },
                Type = "Kolmas sektori",
            };
            organizationService.AddSubOrganization(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), 
                Arg.Any<IEnumerable<LocalizedText>>(), Arg.Any<IEnumerable<LocalizedText>>(), Arg.Any<DateTime?>(), Arg.Any<DateTime?>(), 
                Arg.Any<IEnumerable<LocalizedText>>(), false, false).Returns(organizationId);

            var result = sut.AddSubOrganization(Guid.NewGuid(), company) as OkNegotiatedContentResult<Guid>;

            Assert.IsNotNull(result);
            Assert.AreEqual(organizationId, result.Content);
        }
        
        [TestMethod]
        public void SetOrganizationBasicInformation()
        {
            Guid organizationId = Guid.NewGuid();
            const string businessId = "1234567-8";
            const string finnishDescription = "Suomen vanhin kaupunki";
            const string finnishName = "Turku";
            const string finnishAbbreviation = "TKU";
            const string municipalityCode = "091";
            const string oid = "132456";
            const string finnishLanguageCode = "fi";
            const string type = "Kunta";
            const bool canBeTransferredToFsc = false;
            const bool canBeResponsibleDeptForService = false;

            DateTime? validFrom = DateTime.Today;
            DateTime? validTo = DateTime.Today.AddDays(1);

            BasicInformation information = new BasicInformation
            {
                BusinessId = businessId,
                Names = new List<LocalizedText>
                {
                    new LocalizedText(finnishLanguageCode, finnishName)
                },
                Descriptions = new List<LocalizedText>
                {
                    new LocalizedText(finnishLanguageCode, finnishDescription)
                },
                Type = type,
                MunicipalityCode = municipalityCode,
                Oid = oid,
                ValidFrom = validFrom,
                ValidTo = validTo,
                CanBeTransferredToFsc = canBeTransferredToFsc,
                NameAbbreviations = new List<LocalizedText>
                {
                    new LocalizedText(finnishLanguageCode, finnishAbbreviation)
                }
            };

            sut.SetOrganizationBasicInformation(organizationId, information);

            organizationService.Received(1).SetOrganizationBasicInformation(organizationId, businessId, oid,
                Arg.Is<IEnumerable<LocalizedText>>(texts => texts.Count() == 1 && texts.Any(text => text.LanguageCode.Equals(finnishLanguageCode) && 
                    text.LocalizedValue.Equals(finnishName))),
                Arg.Is<IEnumerable<LocalizedText>>(texts => texts.Count() == 1 && texts.Any(text => text.LanguageCode.Equals(finnishLanguageCode) && 
                    text.LocalizedValue.Equals(finnishDescription))), type, municipalityCode, validFrom, validTo,
                 Arg.Is<IEnumerable<LocalizedText>>(texts => texts.Count() == 1 && texts.Any(text => text.LanguageCode.Equals(finnishLanguageCode) &&
                    text.LocalizedValue.Equals(finnishAbbreviation))), canBeTransferredToFsc, canBeResponsibleDeptForService);
        }

        [TestMethod]
        public void SetOrganizationContactInformation()
        {
            Guid organizationId = Guid.NewGuid();
            const string emailAddress = "me@gmail.com";
            const string phoneNumber = "112";
            const string phoneCallChargeType = "Muu";
            const string webPageUrl1 = "www.google.com";
            const string webPageUrl2 = "www.gmail.com";
            const string webPageName1 = "Google";
            const string webPageName2 = "Gmail";
            const string webPageType1 = "type1";
            const string webPageType2 = "type2";
            List<LocalizedText> homepageUrls = new List<LocalizedText> { new LocalizedText("fi", "url.fi") };
            List<LocalizedText> callChargeInfos = new List<LocalizedText> { new LocalizedText("fi", "info") };

            var information = new ContactInformation
            {
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber,
                PhoneCallChargeType = phoneCallChargeType,
                PhoneCallChargeInfos = callChargeInfos,
                WebPages = new List<WebPage> { new WebPage(webPageName1, webPageUrl1, webPageType1), new WebPage(webPageName2, webPageUrl2, webPageType2) },
                HomepageUrls = homepageUrls
            };

            sut.SetOrganizationContactInformation(organizationId, information);

            organizationService.Received(1).SetOrganizationContactInformation(organizationId, phoneNumber, phoneCallChargeType, callChargeInfos, emailAddress,
                Arg.Is<IEnumerable<WebPage>>(
                    sites => sites.Count() == 2 && sites.Any(site => site.Name.Equals(webPageName1) && site.Address.Equals(webPageUrl1) && site.Type.Equals(webPageType1)) &&
                        sites.Any(site => site.Name.Equals(webPageName2) && site.Address.Equals(webPageUrl2) && site.Type.Equals(webPageType2))), homepageUrls);
        }

        [TestMethod]
        public void SetOrganizationVisitingAddress()
        {
            Guid organizationId = Guid.NewGuid();
            const string postalCode = "12345";
            IEnumerable<LocalizedText> streetAddresses = new List<LocalizedText> { new LocalizedText("fi", "katu") };
            IEnumerable<LocalizedText> postalDistricts = new List<LocalizedText> { new LocalizedText("fi", "kaupunki") };
            IEnumerable<LocalizedText> qualifiers = new List<LocalizedText> { new LocalizedText("fi", "portaiden päässä") };

            var visitingAddress = new VisitingAddress
            {
                PostalDistricts = postalDistricts,
                StreetAddresses = streetAddresses,
                Qualifiers = qualifiers,
                PostalCode = postalCode
            };

            sut.SetOrganizationVisitingAddress(organizationId, visitingAddress);

            organizationService.Received(1).SetOrganizationVisitingAddress(organizationId, streetAddresses, postalCode, postalDistricts, qualifiers);
        }

        [TestMethod]
        public void SetOrganizationPostalAddresses()
        {
            Guid organizationId = Guid.NewGuid();
            const string streetAddressPostalCode = "13245";
            const string postOfficeBox = "124";
            const string postOfficeBoxAddressPostalCode = "46571";
            const bool useVisitingAddress = true;
            IEnumerable<LocalizedText> streetAddresses = new List<LocalizedText> { new LocalizedText("fi", "katu") };
            IEnumerable<LocalizedText> streetAddressPostalDistricts = new List<LocalizedText> { new LocalizedText("fi", "kaupunki") };
            IEnumerable<LocalizedText> postOfficeBoxAddressPostalDistricts = new List<LocalizedText> { new LocalizedText("fi", "kunta") };

            var postalAddresses = new PostalAddresses
            {
                StreetAddresses = streetAddresses,
                StreetAddressPostalCode = streetAddressPostalCode,
                StreetAddressPostalDistricts = streetAddressPostalDistricts,
                UseVisitingAddress = useVisitingAddress,
                PostOfficeBox = postOfficeBox,
                PostOfficeBoxAddressPostalCode = postOfficeBoxAddressPostalCode,
                PostOfficeBoxAddressPostalDistricts = postOfficeBoxAddressPostalDistricts
            };

            sut.SetOrganizationPostalAddresses(organizationId, postalAddresses);

            organizationService.Received(1).SetOrganizationPostalAddresses(organizationId, useVisitingAddress, streetAddresses, streetAddressPostalCode, streetAddressPostalDistricts,
                postOfficeBox, postOfficeBoxAddressPostalCode, postOfficeBoxAddressPostalDistricts);
        }


        [TestMethod]
        public void SetOrganizationAuthorizationInformation()
        {
            Guid organizationId = Guid.NewGuid();

            string groupName = "domaingroup";
            Guid roleId = Guid.NewGuid();
            Guid? groupId = Guid.NewGuid();

      
            IEnumerable<AuthorizationGroup> authorizationGroups = new List<AuthorizationGroup> { new AuthorizationGroup(groupName, roleId, groupId) };
            var authorizationInformation = new AuthorizationInformation() {AuthorizationGroups = authorizationGroups};

            sut.SetOrganizationAuthorizationInformation(organizationId, authorizationInformation);

            organizationService.Received(1).
                SetOrganizationAuthorizationInformation(organizationId, Arg.Is<IEnumerable<AuthorizationGroup>>(groups => groups.Count() == 1 && groups.Any(group => group.Name.Equals(groupName) && group.RoleId.Equals(roleId))));
        }

        [TestMethod]
        public void DeleteOrganization()
        {
            Guid organizationId = Guid.NewGuid();

            sut.DeleteOrganization(organizationId);

            organizationService.Received(1).RemoveOrganization(organizationId);
        }

        [TestMethod]
        public void DeactivateOrganization()
        {
            Guid organizationId = Guid.NewGuid();

            sut.DeactivateOrganization(organizationId);

            organizationService.Received(1).DeactivateOrganization(organizationId);
        }
    }
}