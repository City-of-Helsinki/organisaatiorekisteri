using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Application.Localization;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Tests.Organization
{
    [TestClass]
    public class OrganizationTests
    {
        private const string ValidBusinessId = "1069622-4";
        private const string Oid = "123456";
        private const string Type = "Yritys";
        private const string ValidLanguageCode = "fi";


        private const bool CanBeTransferredToFsc = false;

        private Application.Organization.Organization sut;

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyIdIsNotAllowed()
        {
            sut = new Application.Organization.Organization(Guid.Empty, 1, ValidBusinessId, Oid, Type, null, CreateLocalizedTextsWithOneText(ValidLanguageCode, "nimi"),
                new List<string> { ValidLanguageCode }, CanBeTransferredToFsc);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyNamesCollectionIsNotAllowed()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null, new LocalizedSingleTexts(), new List<string> { ValidLanguageCode }, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullNamesCollectionIsNotAllowed()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null, null, new List<string> { ValidLanguageCode }, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFinnishNameValueIsNotAllowed()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null, 
                new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "Nimi"), new LocalizedText("fi", null) }),
                new List<string> { ValidLanguageCode, "fi" }, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyNameValueIsNotAllowed()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null, 
                new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText("SE", string.Empty) }), new List<string> { "SE" }, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameCannotHaveUnsupportedLanguage()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null,
                new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText("fi", "organisaatio") }), new List<string> { "SE" }, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DescriptionCannotHaveUnsupportedLanguage()
        {
            sut = CreateSut();

            sut.Descriptions = new LocalizedSingleTexts (new List<LocalizedText> { new LocalizedText("en", "SW company") });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HomepageUrlCannotHaveUnsupportedLanguage()
        {
            sut = CreateSut();

            sut.HomepageUrls = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText("fr", "url.com") });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void QualifierCannotHaveUnsupportedLanguage()
        {
            sut = CreateSut();

            sut.VisitingAddressQualifiers = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText("en", "SW company") });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TypeCannotBeNull()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, null, null, CreateLocalizedTextsWithOneText(ValidLanguageCode, "Nimi"), 
                new List<string> { ValidLanguageCode }, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TypeCannotBeEmpty()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, string.Empty, null, CreateLocalizedTextsWithOneText(ValidLanguageCode, "Nimi"),
                new List<string> { ValidLanguageCode }, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MunicipalityTypeCannotBeWithEmptymunicipalityCode()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), ValidBusinessId, Oid, OrganizationType.Municipality, string.Empty, 
                CreateLocalizedTextsWithOneText(ValidLanguageCode, "Nimi"), new List<string> { ValidLanguageCode }, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MunicipalityTypeCannotBeWithNullmunicipalityCode()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, OrganizationType.Municipality, null,
                CreateLocalizedTextsWithOneText(ValidLanguageCode, "Nimi"), new List<string> { ValidLanguageCode }, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OtherThanMunicipalityTypeCannotBeWithmunicipalityCode()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), ValidBusinessId, Oid, "Other", "12",
                CreateLocalizedTextsWithOneText(ValidLanguageCode, "Nimi"), new List<string> { ValidLanguageCode }, false);
        }

        [TestMethod]
        public void SettingDescriptionAgainWithSameLanguageReplacesDescription()
        {
            const string finalDescription = "Yritys";
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null, 
                CreateLocalizedTextsWithOneText(ValidLanguageCode, "Affecto"), new List<string> { ValidLanguageCode }, false);

            sut.Descriptions = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "Firma") });
            sut.Descriptions = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, finalDescription) });

            Assert.AreEqual(finalDescription, sut.GetDescription(ValidLanguageCode));
        }

        [TestMethod]
        public void SettingNameAbbreviationAgainWithSameLanguageReplacesNameAbbreviation()
        {
            const string finalAbbreviation = "finalVal";
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null,
                CreateLocalizedTextsWithOneText(ValidLanguageCode, "Affecto"), new List<string> { ValidLanguageCode }, false);

            sut.NameAbbreviations = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "Val") });
            sut.NameAbbreviations = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, finalAbbreviation) });

            Assert.AreEqual(finalAbbreviation, sut.GetNameAbbreviation(ValidLanguageCode));
        }


        [TestMethod]
        public void SettingHomepageUrlAgainWitSamLanguageReplacesHomepageUrl()
        {
            const string finalUrl = "http://firma.com";
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null,
                CreateLocalizedTextsWithOneText(ValidLanguageCode, "firma"), new List<string> { ValidLanguageCode }, false);

            sut.HomepageUrls = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "http://firma.fi") });
            sut.HomepageUrls = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, finalUrl) });

            Assert.AreEqual(finalUrl, sut.GetHomepageUrl(ValidLanguageCode));
        }

        [TestMethod]
        public void SettingAddressQualifiersAgainWithSameLanguageReplacesOldInformation()
        {
            const string finalQualifier = "Portaiden päässä";
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null, CreateLocalizedTextsWithOneText(ValidLanguageCode, "Affecto"),
                new List<string> { ValidLanguageCode }, false);

            sut.VisitingAddressQualifiers = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "Kellarissa") });
            sut.VisitingAddressQualifiers = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, finalQualifier) });

            Assert.AreEqual(finalQualifier, sut.GetVisitingAddressQualifier(ValidLanguageCode));
        }

        [TestMethod]
        public void PhoneNumberIsNullAfterInitialization()
        {
            sut = CreateSut();
            
            Assert.IsNull(sut.PhoneNumber);
        }

        [TestMethod]
        public void PhoneCallCahrgegTypeIsNullAfterInitialization()
        {
            sut = CreateSut();

            Assert.IsNull(sut.PhoneCallChargeType);
        }

        [TestMethod]
        public void EmailAddressIsNullAfterInitialization()
        {
            sut = CreateSut();

            Assert.IsNull(sut.EmailAddress);
        }

        [TestMethod]
        public void WebPagesAreEmptyAfterInitialization()
        {
            sut = CreateSut();

            Assert.IsFalse(sut.WebPages.Any());
        }

        [TestMethod]
        public void DescriptionsAreEmptyAfterInitialization()
        {
            sut = CreateSut();

            Assert.IsFalse(sut.Descriptions.Any());
        }


        [TestMethod]
        public void NameAbbreviationsAreEmptyAfterInitialization()
        {
            sut = CreateSut();

            Assert.IsFalse(sut.NameAbbreviations.Any());
        }

        [TestMethod]
        public void QualifiersAreEmptyAfterInitialization()
        {
            sut = CreateSut();

            Assert.IsFalse(sut.VisitingAddressQualifiers.Any());
        }

        [TestMethod]
        public void SettingNullPhoneNumber()
        {
            sut = CreateSut();

            var callChargeInfos = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "info") });

            sut.SetCallInformation(null, "Muu", callChargeInfos);

            Assert.IsNull(sut.PhoneNumber);
            Assert.IsNull(sut.PhoneCallChargeType);
        }

        [TestMethod]
        public void SettingEmptyPhoneNumber()
        {
            sut = CreateSut();

            var callChargeInfos = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "info") });

            sut.SetCallInformation(string.Empty, "Muu", callChargeInfos);

            Assert.IsNull(sut.PhoneNumber);
            Assert.IsNull(sut.PhoneCallChargeType);
        }

        [TestMethod]
        public void SettingNullEmailAddress()
        {
            sut = CreateSut();

            sut.EmailAddress = null;

            Assert.IsNull(sut.EmailAddress);
        }

        [TestMethod]
        public void SettingEmptyEmailAddress()
        {
            sut = CreateSut();

            sut.EmailAddress = string.Empty;

            Assert.IsNull(sut.EmailAddress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotChangeToMunicipalityTypeWithNullMunicipalityCode()
        {
            sut = CreateSut();

            sut.SetType(OrganizationType.Municipality, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotChangeToMunicipalityTypeWithEmptyMunicipalityCode()
        {
            sut = CreateSut();

            sut.SetType(OrganizationType.Municipality, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotChangeFromMunicipalityTypeWithMunicipalityCode()
        {
            sut = CreateSut();
            sut.SetType(OrganizationType.Municipality, "132");

            sut.SetType("Company", "133");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingEmptyNamesCollection()
        {
            sut = CreateSut();
            sut.Names = new List<LocalizedText>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingNameWithEmptyLocalizedValue()
        {
            sut = CreateSut();
            sut.Names = new List<LocalizedText> { new LocalizedText(ValidLanguageCode, string.Empty) };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingNameWithNullLocalizedValue()
        {
            sut = CreateSut();
            sut.Names = new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "arvo"), new LocalizedText("en", null) };
        }

       
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingHomepageUrlWithNullLocalizedValue()
        {
            sut = CreateSut();
            sut.HomepageUrls = new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "url.com"), new LocalizedText("en", null) };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingHomepageUrlWithIncorrectUrl()
        {
            sut = CreateSut();
            sut.HomepageUrls = new List<LocalizedText> { new LocalizedText(ValidLanguageCode,"noturl"), new LocalizedText("en", "url.com") };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingEmptyType()
        {
            sut = CreateSut();

            sut.SetType(string.Empty, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingNullType()
        {
            sut = CreateSut();

            sut.SetType(null, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingEmptyBusinessIdentifier()
        {
            sut = CreateSut();

            sut.BusinessId = string.Empty;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotSetVisitingStreetAddressWithLanguageNotInNames()
        {
            sut = CreateSut();

            sut.SetVisitingAddress(new List<LocalizedText> { new LocalizedText("en", "street 1") }, "12345", 
                new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "Helsinki") });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotSetVisitingAddressPostalDistrictWithLanguageNotInNames()
        {
            sut = CreateSut();

            sut.SetVisitingAddress(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "katu 1") }, "12345", 
                new List<LocalizedText> { new LocalizedText("sv", "Åbo") });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotSetPostalStreetAddressWithLanguageNotInNames()
        {
            sut = CreateSut();

            sut.SetPostalAddress(false, new List<LocalizedText> { new LocalizedText("en", "street 1") }, "12345",
                new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "Helsinki") }, null, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotSetPostalStreetAddressPostalDistrictWithLanguageNotInNames()
        {
            sut = CreateSut();

            sut.SetPostalAddress(false, new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "katu 1") }, "12345",
                new List<LocalizedText> { new LocalizedText("sv", "Åbo") }, null, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotSetPostalPostOfficeBoxAddressPostalDistrictWithLanguageNotInNames()
        {
            sut = CreateSut();

            sut.SetPostalAddress(false, null, null, null, "10", "12345", new List<LocalizedText> { new LocalizedText("sv", "Åbo") });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotUseVisitingAddressAsPostalAddressAndDefineSeparatePostalStreetAddressSimultaneously()
        {
            sut = CreateSut();

            sut.SetPostalAddress(true, new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "katu 1") }, "12345",
                new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "Turku") }, null, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotUseVisitingAddressAsPostalAddressAndDefinePostOfficeBoxAddressSimultaneously()
        {
            sut = CreateSut();
            sut.SetVisitingAddress(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "katu 1") }, "12345", 
                new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "Turku") });

            sut.SetPostalAddress(true, null, null, null, "10", "12345", new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "Turku") });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotDefineSeparateStreetPostalAddressAndPostOfficeBoxAddressSimultaneously()
        {
            sut = CreateSut();

            sut.SetPostalAddress(false, new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "katu 1") }, "12345", 
                new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "Turku") }, "10", "12345", new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "Turku") });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameAbbreviationsCannotHaveUnsupportedLanguage()
        {
            sut = CreateSut();

            sut.NameAbbreviations = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText("fr", "comp") });
        }

      

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingNameAbbreviationWithNullLocalizedValue()
        {
            sut = CreateSut();
            sut.NameAbbreviations = new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "val"), new LocalizedText("en", null) };
        }


        [TestMethod]
        public void SettingValidityDoesNotSetTime()
        {
            const int year = 2010;
            const int month = 1;
            const int day = 2;

            sut = CreateSut();

            sut.SetValidity(new DateTime(year, month, day, 11, 11, 11), new DateTime(year, month, day, 10, 10, 10));

            Assert.AreEqual(new DateTime(year, month, day, 0, 0, 0), sut.ValidFrom);
            Assert.AreEqual(new DateTime(year, month, day, 0, 0, 0), sut.ValidTo);
        }

        private static Application.Organization.Organization CreateSut()
        {
            return new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null, 
                CreateLocalizedTextsWithOneText(ValidLanguageCode, "Affecto"), new List<string> { ValidLanguageCode }, false);
        }

        private static LocalizedSingleTexts CreateLocalizedTextsWithOneText(string languageCode, string text)
        {
            return new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(languageCode, text) });
        }
    }
}