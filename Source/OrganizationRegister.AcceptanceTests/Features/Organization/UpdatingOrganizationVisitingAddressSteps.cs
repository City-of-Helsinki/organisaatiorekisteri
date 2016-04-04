using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.AcceptanceTests.Infrastructure;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Common;
using OrganizationRegister.Tests.Infrastructure;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Features.Organization
{
    [Binding]
    [Scope(Feature = "UpdatingOrganizationVisitingAddress")]
    [Scope(Feature = "UpdatingOrganizationPostalAddresses")]
    internal sealed class UpdatingOrganizationVisitingAddressSteps : StepDefinition
    {
        [Given(@"the following visiting address is set to the organization:")]
        [When(@"the following visiting address is set to the organization:")]
        public void WhenTheFollowingVisitingAddressIsSetToTheOrganization(Table visitingAddress)
        {
            TableRow address = visitingAddress.Rows.Single();
            IEnumerable<LocalizedText> streetAddresses = LocalizedTextHelper.CreateStreetAddressesCollection(address);
            IEnumerable<LocalizedText> postalDistricts = LocalizedTextHelper.CreatePostalDistrictsCollection(address);
            IEnumerable<LocalizedText> additionalEntranceInstructions = LocalizedTextHelper.CreateAddressQualifiersCollection(address);
            Try(() => OrganizationService.SetOrganizationVisitingAddress(CurrentScenarioContext.OrganizationId, streetAddresses, address["Postal code"],
                postalDistricts, additionalEntranceInstructions));
        }

        [Then(@"the organization has the following visiting address:")]
        public void ThenTheOrganizationHasTheFollowingVisitingAddress(Table expectedVisitingAddress)
        {
            TableRow expectedAddress = expectedVisitingAddress.Rows.Single();
            List<LocalizedText> expectedStreetAddresses = LocalizedTextHelper.CreateStreetAddressesCollection(expectedAddress).ToList();
            List<LocalizedText> expectedPostalDistricts = LocalizedTextHelper.CreatePostalDistrictsCollection(expectedAddress).ToList();
            List<LocalizedText> expectedQualifiers = LocalizedTextHelper.CreateAddressQualifiersCollection(expectedAddress).ToList();
            
            IOrganization result = OrganizationService.GetOrganization(CurrentScenarioContext.OrganizationId);

            AddressHelper.AssertAddress(result.VisitingAddress, expectedStreetAddresses, expectedAddress["Postal code"], expectedPostalDistricts);
            
            Assert.AreEqual(expectedQualifiers.Count, result.VisitingAddressQualifiers.Count());
            foreach (LocalizedText instruction in expectedQualifiers)
            {
                if (string.IsNullOrWhiteSpace(instruction.LocalizedValue))
                {
                    Assert.IsTrue(result.VisitingAddressQualifiers.Any(i => i.LanguageCode.Equals(instruction.LanguageCode) && string.IsNullOrEmpty(i.LocalizedValue)));
                }
                else
                {
                    Assert.IsTrue(result.VisitingAddressQualifiers.Any(i => i.Equals(instruction)));                    
                }
            }
        }

        [When(@"visiting address of the organization is set as empty")]
        public void WhenVisitingAddressOfTheOrganizationIsSetAsEmpty()
        {
            OrganizationService.SetOrganizationVisitingAddress(CurrentScenarioContext.OrganizationId, null, null, null, null);
        }

        [Then(@"the organization has no visiting address")]
        public void ThenTheOrganizationHasNoVisitingAddress()
        {
            IOrganization result = OrganizationService.GetOrganization(CurrentScenarioContext.OrganizationId);
            AddressHelper.AssertUndefinedAddress(result.VisitingAddress);
            Assert.IsFalse(result.VisitingAddressQualifiers.Any(instruction => instruction.LocalizedValue != null));
        }

        [Then(@"setting the visiting address fails")]
        public void ThenSettingTheVisitingAddressFails()
        {
            AssertCaughtException<ArgumentException>();
        }
    }
}
