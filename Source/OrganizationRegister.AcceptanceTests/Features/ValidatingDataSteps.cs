using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.AcceptanceTests.Infrastructure;
using OrganizationRegister.Application.Validation;
using OrganizationRegister.Common;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Features
{
    [Binding]
    [Scope(Feature = "ValidatingData")]
    internal sealed class ValidatingDataSteps : StepDefinition
    {
        private IBusinessIdentifierValidationResult businessIdentifierValidationResult;
        private bool isPhoneNumberValid;
        private bool isEmailValid;
        private bool isWebAddressValid;
        private bool isPostalCodeValid;
        private bool isPostOfficeBoxPostalCodeValid;

        [When(@"unique business identifier '(.+)' is validated")]
        [When(@"unique business identifier '(.+)' is validated for a new organization")]
        public void WhenUniqueBusinessIdentifierIsValidated(string businessId)
        {
            businessIdentifierValidationResult = ValidationService.ValidateUniqueBusinessIdentifier(businessId, null);
        }

        [When(@"unique business identifier '(.+)' is validated for the same organization")]
        public void WhenUniqueBusinessIdentifierIsValidatedForTheSameOrganization(string businessId)
        {
            Guid? organizationId = OrganizationService.GetActiveMainOrganizations().Single().Id;
            businessIdentifierValidationResult = ValidationService.ValidateUniqueBusinessIdentifier(businessId, organizationId);
        }

        [When(@"non-unique business identifier '(.+)' is validated for a new organization")]
        public void WhenNonUniqueBusinessIdentifierIsValidatedForANewOrganization(string businessId)
        {
            businessIdentifierValidationResult = ValidationService.ValidateBusinessIdentifier(businessId);
        }
        
        [Then(@"business identifer is valid")]
        public void ThenBusinessIdentiferIsValid()
        {
            Assert.IsTrue(businessIdentifierValidationResult.IsValid);
        }

        [Then(@"business identifer is invalid because of invalid format")]
        public void ThenBusinessIdentiferIsInvalidBecauseOfInvalidFormat()
        {
            Assert.IsFalse(businessIdentifierValidationResult.IsValid);
            Assert.AreEqual(Affecto.Identifiers.Finnish.InvalidBusinessIdentifierReason.Format, businessIdentifierValidationResult.ReasonForInvalidity);
        }

        [Then(@"business identifer is invalid because of invalid check sum digit")]
        public void ThenBusinessIdentiferIsInvalidBecauseOfInvalidCheckSumDigit()
        {
            Assert.IsFalse(businessIdentifierValidationResult.IsValid);
            Assert.AreEqual(Affecto.Identifiers.Finnish.InvalidBusinessIdentifierReason.CheckSum, businessIdentifierValidationResult.ReasonForInvalidity);
        }

        [When(@"phone number '(.+)' is validated")]
        public void WhenPhoneNumberIsValidated(string phoneNumber)
        {
            isPhoneNumberValid = ValidationService.ValidatePhoneNumber(phoneNumber);
        }

        [Then(@"phone number is valid")]
        public void ThenPhoneNumberIsValid()
        {
            Assert.IsTrue(isPhoneNumberValid);
        }

        [Then(@"phone number is invalid")]
        public void ThenPhoneNumberIsInvalid()
        {
            Assert.IsFalse(isPhoneNumberValid);
        }

        [When(@"email '(.+)' is validated")]
        public void WhenEmailIsValidated(string email)
        {
            isEmailValid = ValidationService.ValidateEmailAddress(email);
        }

        [Then(@"email is valid")]
        public void ThenEmailIsValid()
        {
            Assert.IsTrue(isEmailValid);
        }

        [Then(@"email is invalid")]
        public void ThenEmailIsInvalid()
        {
            Assert.IsFalse(isEmailValid);
        }

        [When(@"web address '(.+)' is validated")]
        public void WhenWebAddressIsValidated(string webAddress)
        {
            isWebAddressValid = ValidationService.ValidateWebAddress(webAddress);
        }

        [Then(@"web address is valid")]
        public void ThenWebAddressIsValid()
        {
            Assert.IsTrue(isWebAddressValid);
        }

        [Then(@"web address is invalid")]
        public void ThenWebAddressIsInvalid()
        {
            Assert.IsFalse(isWebAddressValid);
        }

        [Given(@"there is an active organization with business identifier '(.+)'")]
        public void GivenThereIsAnActiveOrganizationWithBusinessIdentifier(string businessId)
        {
            OrganizationService.AddOrganization(businessId, null, "Yritys", null, new List<LocalizedText> { new LocalizedText("fi", "organisaatio") }, null);
        }

        [Given(@"there is an inactive organization with business identifier '(.+)'")]
        public void GivenThereIsAnInactiveOrganizationWithBusinessIdentifier(string businessId)
        {
            Guid guid = OrganizationService.AddOrganization(businessId, null, "Yritys", null, new List<LocalizedText> { new LocalizedText("fi", "organisaatio") }, null);
            OrganizationService.DeactivateOrganization(guid);
        }

        [Then(@"business identifer is invalid because it is already used")]
        public void ThenBusinessIdentiferIsInvalidBecauseItIsAlreadyUsed()
        {
            Assert.IsFalse(businessIdentifierValidationResult.IsValid);
            Assert.AreEqual(InvalidBusinessIdentifierReason.AlreadyExists, businessIdentifierValidationResult.ReasonForInvalidity);
        }

        [When(@"postal code '(.+)' is validated")]
        public void WhenPostalCodeIsValidated(string postalCode)
        {
            isPostalCodeValid = ValidationService.ValidatePostalCode(postalCode);
        }

        [Then(@"postal code is valid")]
        public void ThenPostalCodeIsValid()
        {
            Assert.IsTrue(isPostalCodeValid);
        }

        [Then(@"postal code is invalid")]
        public void ThenPostalCodeIsInvalid()
        {
            Assert.IsFalse(isPostalCodeValid);
        }

        [When(@"post office box postal code '(.+)' is validated")]
        public void WhenPostOfficeBoxPostalCodeIsValidated(string postalCode)
        {
            isPostOfficeBoxPostalCodeValid = ValidationService.ValidatePostOfficeBoxPostalCode(postalCode);
        }

        [Then(@"post office box postal code is valid")]
        public void ThenPostOfficeBoxPostalCodeIsValid()
        {
            Assert.IsTrue(isPostOfficeBoxPostalCodeValid);
        }

        [Then(@"post office box postal code is invalid")]
        public void ThenPostOfficeBoxPostalCodeIsInvalid()
        {
            Assert.IsFalse(isPostOfficeBoxPostalCodeValid);
        }
    }
}
