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
    [Scope(Feature = "UpdatingOrganizationContactInformation")]
    internal sealed class UpdatingOrganizationContactInformationSteps : StepDefinition
    {
        [Given(@"the following contact information is set to the organization:")]
        [When(@"the following contact information is set to the organization:")]
        public void WhenTheFollowingContactInformationIsSetToOrganization(Table contactInformation)
        {
            TableRow contactInfo = contactInformation.Rows.Single();
            Try(
                () =>
                    OrganizationService.SetOrganizationContactInformation(CurrentScenarioContext.OrganizationId, contactInfo["Phone number"],
                        contactInfo["Phone call fee"],
                        contactInfo["Email address"], CreateWebSiteCollection(contactInfo), LocalizedTextHelper.CreateHomepageUrlsCollection(contactInfo)));
        }



        [Then(@"the organization has the following contact information:")]
        public void ThenOrganizationHasTheFollowingContactInformation(Table expectedContactInformation)
        {
            TableRow expectedContactInfo = expectedContactInformation.Rows.Single();
            IOrganization result = OrganizationService.GetOrganization(CurrentScenarioContext.OrganizationId);

            Assert.AreEqual(expectedContactInfo["Phone number"], result.PhoneNumber);
            Assert.AreEqual(expectedContactInfo["Phone call fee"], result.PhoneCallFee);
            Assert.AreEqual(expectedContactInfo["Email address"], result.EmailAddress);

            Assert.AreEqual(2, result.WebPages.Count());
            Assert.IsTrue(
                result.WebPages.Any(
                    site =>
                        site.Name.Equals(expectedContactInfo["web site name"]) && site.Address.Equals(expectedContactInfo["web address"])
                            && site.Type.Equals(expectedContactInfo["web page type"])));

            Assert.IsTrue(result.WebPages.Any(site => site.Name.Equals(expectedContactInfo["second web site name"]) &&
                site.Address.Equals(expectedContactInfo["second web address"]) && site.Type.Equals(expectedContactInfo["second web page type"])));


            Assert.AreEqual(2, result.HomepageUrls.Count());

            Assert.IsTrue(result.HomepageUrls.Any(url => url.LocalizedValue.Equals(expectedContactInfo["homepage in finnish"]) &&
                url.LanguageCode.Equals("fi")));

            Assert.IsTrue(result.HomepageUrls.Any(url => url.LocalizedValue.Equals(expectedContactInfo["homepage in swedish"]) &&
               url.LanguageCode.Equals("sv")));

        }

        [Then(@"setting the contact information fails")]
        public void ThenSettingTheContactInformationFails()
        {
            AssertCaughtException<ArgumentException>();
        }

        [When(@"contact information of the organization is set as empty")]
        public void WhenOrganizationContactInformationIsSetAsEmpty()
        {
            OrganizationService.SetOrganizationContactInformation(CurrentScenarioContext.OrganizationId, null, null, null, null, null);
        }

        [Then(@"the organization has no contact information")]
        public void ThenOrganizationHasNoContactInformation()
        {
            IOrganization result = OrganizationService.GetOrganization(CurrentScenarioContext.OrganizationId);

            Assert.IsNull(result.PhoneNumber);
            Assert.IsNull(result.PhoneCallFee);
            Assert.IsNull(result.EmailAddress);
            Assert.IsFalse(result.WebPages.Any());
            
        }

        private IEnumerable<WebPage> CreateWebSiteCollection(TableRow contactInfo)
        {
            return new List<WebPage>
            {
                new WebPage(contactInfo["web site name"], contactInfo["web address"], contactInfo["web page type"]),
                new WebPage(contactInfo["second web site name"], contactInfo["second web address"], contactInfo["second web page type"])
            };
        }
    }

}
