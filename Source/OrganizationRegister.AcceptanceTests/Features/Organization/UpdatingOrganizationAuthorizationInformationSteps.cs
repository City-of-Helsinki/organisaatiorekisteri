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
    [Scope(Feature = "UpdatingOrganizationAuthorizationInformation")]
    internal sealed class UpdatingOrganizationAuthorizationInformationSteps : StepDefinition
    {
        [Given(@"the following authorization information is set to the organization:")]
        [When(@"the following authorization information is set to the organization:")]
        public void WhenTheFollowingAuthorizationInformationIsSetToOrganization(Table authorizationInformation)
        {
            TableRow info = authorizationInformation.Rows.Single();
            Try(
                () =>
                    OrganizationService.SetOrganizationAuthorizationInformation(CurrentScenarioContext.OrganizationId, CreateAuthorizationGroupCollection(info)));
        }

        [Then(@"the organization has the following authorization information:")]
        public void ThenOrganizationHasTheFollowingAuthorizationInformation(Table expectedAuthorizationInformation)
        {
            TableRow expectedInfo = expectedAuthorizationInformation.Rows.Single();
            IOrganization result = OrganizationService.GetOrganization(CurrentScenarioContext.OrganizationId);

           
            Assert.AreEqual(2, result.AuthorizationGroups.Count());
            Assert.IsTrue(
                result.AuthorizationGroups.Any(
                    group =>
                        group.Name.Equals(expectedInfo["group name"]) && group.RoleId.Equals(Guid.Parse(expectedInfo["role id"])) && group.GroupId.Equals(Guid.Parse(expectedInfo["group id"]))));

            Assert.IsTrue(
                result.AuthorizationGroups.Any(
                    group =>
                        group.Name.Equals(expectedInfo["second group name"]) && group.RoleId.Equals(Guid.Parse(expectedInfo["second role id"])) && group.GroupId.Equals(Guid.Parse(expectedInfo["second group id"]))));
        }

        [When(@"authorization information of the organization is set as empty")]
        public void WhenOrganizationAuthorizationInformationIsSetAsEmpty()
        {
            OrganizationService.SetOrganizationAuthorizationInformation(CurrentScenarioContext.OrganizationId, null);
        }

        [Then(@"the organization has no authorization information")]
        public void ThenOrganizationHasNoAuthorizationInformation()
        {
            IOrganization result = OrganizationService.GetOrganization(CurrentScenarioContext.OrganizationId);
            Assert.IsFalse(result.AuthorizationGroups.Any());
        }

        private IEnumerable<AuthorizationGroup> CreateAuthorizationGroupCollection(TableRow authorizationInfo)
        {
            return new List<AuthorizationGroup>
            {
                new AuthorizationGroup(authorizationInfo["group name"], Guid.Parse(authorizationInfo["role id"]), Guid.Parse(authorizationInfo["group id"])),
                new AuthorizationGroup(authorizationInfo["second group name"], Guid.Parse(authorizationInfo["second role id"]), Guid.Parse(authorizationInfo["second group id"]))
            };
        }
    }

}
