using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.AcceptanceTests.Infrastructure;
using OrganizationRegister.Application.Organization;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Features.Organization
{
    [Binding]
    [Scope(Feature = "GettingOrganization")]
    internal sealed class GettingOrganizationSteps : StepDefinition
    {
        private Guid organizationId;
        private IOrganizationName organizationNameResult = null;

        [When(@"the name of organization '(.+)' is requested")]
        public void WhenTheNameOfOrganizationIsRequested(string organizationName)
        {
            organizationId = OrganizationHelper.GetOrganizationId(organizationName);
            Try(() => organizationNameResult = OrganizationService.GetOrganizationName(organizationId));
        }

        [Then(@"the following information is retrieved:")]
        public void ThenTheFollowingInformationIsRetrieved(Table organizationNameData)
        {
            Assert.IsNotNull(organizationNameResult);
            Assert.IsNotNull(organizationNameResult.Names);

            Assert.AreEqual(organizationId, organizationNameResult.Id);
            Assert.AreEqual(2, organizationNameResult.Names.Count());
            Assert.AreEqual(organizationNameData.Rows[0]["Finnish name"], organizationNameResult.Names.GetValue("fi"));
            Assert.AreEqual(organizationNameData.Rows[0]["Swedish name"], organizationNameResult.Names.GetValue("sv"));
        }
    }
}