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

        private Application.Organization.Organization sut;

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyIdIsNotAllowed()
        {
            sut = new Application.Organization.Organization(Guid.Empty, 1, ValidBusinessId, Oid, Type, null, CreateLocalizedTextsWithOneText(ValidLanguageCode, "nimi"),
                new List<string> { ValidLanguageCode });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyNamesCollectionIsNotAllowed()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null, new LocalizedSingleTexts(), new List<string> { ValidLanguageCode });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullNamesCollectionIsNotAllowed()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null, null, new List<string> { ValidLanguageCode });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullNameValueIsNotAllowed()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null, 
                new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "Nimi"), new LocalizedText("SE", null) }),
                new List<string> { ValidLanguageCode, "SE" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyNameValueIsNotAllowed()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null, 
                new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText("SE", string.Empty) }), new List<string> { "SE" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameCannotHaveUnsupportedLanguage()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null,
                new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText("fi", "organisaatio") }), new List<string> { "SE" });
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
                new List<string> { ValidLanguageCode });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TypeCannotBeEmpty()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, string.Empty, null, CreateLocalizedTextsWithOneText(ValidLanguageCode, "Nimi"),
                new List<string> { ValidLanguageCode });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MunicipalityTypeCannotBeWithEmptymunicipalityCode()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), ValidBusinessId, Oid, OrganizationType.Municipality, string.Empty, 
                CreateLocalizedTextsWithOneText(ValidLanguageCode, "Nimi"), new List<string> { ValidLanguageCode });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MunicipalityTypeCannotBeWithNullmunicipalityCode()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, OrganizationType.Municipality, null,
                CreateLocalizedTextsWithOneText(ValidLanguageCode, "Nimi"), new List<string> { ValidLanguageCode });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OtherThanMunicipalityTypeCannotBeWithmunicipalityCode()
        {
            sut = new Application.Organization.Organization(Guid.NewGuid(), ValidBusinessId, Oid, "Other", "12",
                CreateLocalizedTextsWithOneText(ValidLanguageCode, "Nimi"), new List<string> { ValidLanguageCode });
        }

        [TestMethod]
        public void SettingDescriptionAgainWithSameLanguageReplacesDescription()
        {
            const string finalDescription = "Yritys";
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null, 
                CreateLocalizedTextsWithOneText(ValidLanguageCode, "Affecto"), new List<string> { ValidLanguageCode });

            sut.Descriptions = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "Firma") });
            sut.Descriptions = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, finalDescription) });

            Assert.AreEqual(finalDescription, sut.GetDescription(ValidLanguageCode));
        }

        [TestMethod]
        public void SettingAddressQualifiersAgainWithSameLanguageReplacesOldInformation()
        {
            const string finalQualifier = "Portaiden päässä";
            sut = new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null, CreateLocalizedTextsWithOneText(ValidLanguageCode, "Affecto"),
                new List<string> { ValidLanguageCode });

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
        public void PhoneCallFeeIsNullAfterInitialization()
        {
            sut = CreateSut();

            Assert.IsNull(sut.PhoneCallFee);
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
        public void QualifiersAreEmptyAfterInitialization()
        {
            sut = CreateSut();

            Assert.IsFalse(sut.VisitingAddressQualifiers.Any());
        }

        [TestMethod]
        public void SettingNullPhoneNumber()
        {
            sut = CreateSut();

            sut.SetCallInformation(null, "local cost");

            Assert.IsNull(sut.PhoneNumber);
            Assert.IsNull(sut.PhoneCallFee);
        }

        [TestMethod]
        public void SettingEmptyPhoneNumber()
        {
            sut = CreateSut();

            sut.SetCallInformation(string.Empty, "local cost");

            Assert.IsNull(sut.PhoneNumber);
            Assert.IsNull(sut.PhoneCallFee);
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

            sut.Names = new List<LocalizedText> { new LocalizedText(ValidLanguageCode, string.Empty )};
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

            sut.SetPostalAddress(new List<LocalizedText> { new LocalizedText("en", "street 1") }, "12345",
                new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "Helsinki") }, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotSetPostalStreetAddressPostalDistrictWithLanguageNotInNames()
        {
            sut = CreateSut();

            sut.SetPostalAddress(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "katu 1") }, "12345",
                new List<LocalizedText> { new LocalizedText("sv", "Åbo") }, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotSetPostalPostOfficeBoxAddressPostalDistrictWithLanguageNotInNames()
        {
            sut = CreateSut();

            sut.SetPostalAddress("10", "12345", new List<LocalizedText> { new LocalizedText("sv", "Åbo") });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotUseVisitingAddressAsPostalAddressAndDefineSeparatePostalStreetAddressSimultaneously()
        {
            sut = CreateSut();

            sut.SetPostalAddress(new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "katu 1") }, "12345",
                new List<LocalizedText> { new LocalizedText(ValidLanguageCode, "Turku") }, true);
        }

        private static Application.Organization.Organization CreateSut()
        {
            return new Application.Organization.Organization(Guid.NewGuid(), 1, ValidBusinessId, Oid, Type, null, 
                CreateLocalizedTextsWithOneText(ValidLanguageCode, "Affecto"), new List<string> { ValidLanguageCode });
        }

        private static LocalizedSingleTexts CreateLocalizedTextsWithOneText(string languageCode, string text)
        {
            return new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(languageCode, text) });
        }
    }
}