using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.AcceptanceTests.Infrastructure;
using OrganizationRegister.Application.Location;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Common;
using OrganizationRegister.Tests.Infrastructure;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Features.Organization
{
    [Binding]
    [Scope(Feature = "UpdatingOrganizationPostalAddresses")]
    internal sealed class UpdatingOrganizationPostalAddressesSteps : StepDefinition
    {
        [Given(@"the following postal street address is set to the organization:")]
        [When(@"the following postal street address is set to the organization:")]
        public void WhenTheFollowingPostalStreetAddressIsSetToTheOrganization(Table postalAddress)
        {
            TableRow address = postalAddress.Rows.Single();
            IEnumerable<LocalizedText> streetAddresses = LocalizedTextHelper.CreateStreetAddressesCollection(address);
            IEnumerable<LocalizedText> postalDistricts = LocalizedTextHelper.CreatePostalDistrictsCollection(address);
            Try(() => OrganizationService.SetOrganizationPostalAddresses(CurrentScenarioContext.OrganizationId, false, streetAddresses, address["Postal code"],
                postalDistricts, null, null, null));
        }

        [Then(@"the organization has the following postal street address:")]
        public void ThenTheOrganizationHasTheFollowingPostalStreetAddress(Table expectedPostalAddress)
        {
            TableRow expectedAddress = expectedPostalAddress.Rows.Single();
            List<LocalizedText> expectedStreetAddresses = LocalizedTextHelper.CreateStreetAddressesCollection(expectedAddress).ToList();
            List<LocalizedText> expectedPostalDistricts = LocalizedTextHelper.CreatePostalDistrictsCollection(expectedAddress).ToList();

            StreetAddress result = OrganizationService.GetOrganization(CurrentScenarioContext.OrganizationId).PostalStreetAddress;
            AddressHelper.AssertAddress(result, expectedStreetAddresses, expectedAddress["Postal code"], expectedPostalDistricts);
        }

        [When(@"postal address of the organization is set as empty")]
        public void WhenPostalAddressOfTheOrganizationIsSetAsEmpty()
        {
            OrganizationService.SetOrganizationPostalAddresses(CurrentScenarioContext.OrganizationId, false, null, null, null, null, null, null);
        }

        [Then(@"the organization has no postal address")]
        public void ThenTheOrganizationHasNoPostalAddress()
        {
            IOrganization organization = OrganizationService.GetOrganization(CurrentScenarioContext.OrganizationId);
            Assert.IsFalse(organization.UseVisitingAddressAsPostalAddress);
            AssertUndefinedPostalAddress(organization);
        }

        [Then(@"setting the postal address fails")]
        public void ThenSettingThePostalAddressFails()
        {
            AssertCaughtException<Exception>();
        }

        [Given(@"the following postal post office box address is set to the organization:")]
        [When(@"the following postal post office box address is set to the organization:")]
        public void WhenTheFollowingPostalPostOfficeBoxAddressIsSetToTheOrganization(Table postalAddress)
        {
            TableRow address = postalAddress.Rows.Single();
            IEnumerable<LocalizedText> postalDistricts = LocalizedTextHelper.CreatePostalDistrictsCollection(address);
            Try(() => OrganizationService.SetOrganizationPostalAddresses(CurrentScenarioContext.OrganizationId, false, null, null, null, address["post office box"], 
                address["Postal code"], postalDistricts));
        }

        [Then(@"the organization has the following postal post office box address:")]
        public void ThenTheOrganizationHasTheFollowingPostalPostOfficeBoxAddress(Table expectedPostalAddress)   
        {
            TableRow expectedAddress = expectedPostalAddress.Rows.Single();
            List<LocalizedText> expectedPostalDistricts = LocalizedTextHelper.CreatePostalDistrictsCollection(expectedAddress).ToList();

            PostOfficeBoxAddress result = OrganizationService.GetOrganization(CurrentScenarioContext.OrganizationId).PostalPostOfficeBoxAddress;
            AddressHelper.AssertAddress(result, expectedAddress["post office box"], expectedAddress["Postal code"], expectedPostalDistricts);
        }

        [When(@"the following postal address is set to the organization:")]
        public void WhenTheFollowingPostalAddressIsSetToTheOrganization(Table postalAddress)
        {
            TableRow address = postalAddress.Rows.Single();
            IEnumerable<LocalizedText> streetAddresses = LocalizedTextHelper.CreateStreetAddressesCollection(address);
            IEnumerable<LocalizedText> streetAddressPostalDistricts = LocalizedTextHelper.CreateStreetAddressPostalDistrictsCollection(address);
            IEnumerable<LocalizedText> postOfficeBoxAddressPostalDistricts = LocalizedTextHelper.CreatePostOfficeBoxAddressPostalDistrictsCollection(address);
            Try(() => OrganizationService.SetOrganizationPostalAddresses(CurrentScenarioContext.OrganizationId, false, streetAddresses, address["Street address postal code"],
                streetAddressPostalDistricts, address["post office box"], address["post office box address postal code"], postOfficeBoxAddressPostalDistricts));
        }

        [Given(@"the postal address of the organization is set to be the same as the visting address")]
        [When(@"the postal address of the organization is set to be the same as the visting address")]
        public void WhenThePostalAddressOfTheOrganizationIsSetToBeTheSameAsTheVistingAddress()
        {
            Try(() => OrganizationService.SetOrganizationPostalAddresses(CurrentScenarioContext.OrganizationId, true, null, null, null, null, null, null));
        }

        [Then(@"the postal address of the organization is the same as the visiting address")]
        public void ThenThePostalAddressOfTheOrganizationIsTheSameAsTheVisitingAddress()
        {
            IOrganization organization = OrganizationService.GetOrganization(CurrentScenarioContext.OrganizationId);
            Assert.IsTrue(organization.UseVisitingAddressAsPostalAddress);
            AssertUndefinedPostalAddress(organization);
        }

        [Then(@"the postal address of the organization is not the same as the visiting address")]
        public void ThenThePostalAddressOfTheOrganizationIsNotTheSameAsTheVisitingAddress()
        {
            IOrganization organization = OrganizationService.GetOrganization(CurrentScenarioContext.OrganizationId);
            Assert.IsFalse(organization.UseVisitingAddressAsPostalAddress);
        }
        
        private static void AssertUndefinedPostalAddress(IPostalAddress organization)
        {
            AddressHelper.AssertUndefinedAddress(organization.PostalStreetAddress);
            AddressHelper.AssertUndefinedAddress(organization.PostalPostOfficeBoxAddress);
        }
    }
}
