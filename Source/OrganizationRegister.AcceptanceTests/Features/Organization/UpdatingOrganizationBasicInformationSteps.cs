using System;
using System.Linq;
using Affecto.Testing.SpecFlow;
using OrganizationRegister.AcceptanceTests.Infrastructure;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Tests.Infrastructure;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Features.Organization
{
    [Binding]
    [Scope(Feature = "UpdatingOrganizationBasicInformation")]
    internal sealed class UpdatingOrganizationBasicInformationSteps : StepDefinition
    {
        [Given(@"the following company is added:")]
        public void GivenTheFollowingCompanyIsAdded(Table basicInformation)
        {
            TableRow info = basicInformation.Rows.Single();
            OrganizationService.AddOrganization(info["Business id"], info.GetOptionalValue("Oid"), "Yritys", null, LocalizedTextHelper.CreateNamesCollection(info), null,
                info.GetOptionalFinnishDate("Valid from"), info.GetOptionalFinnishDate("Valid to"), LocalizedTextHelper.CreateNameAbbreviationsCollection(info));
        }

        [When(@"the following basic information is set to the previously added organization:")]
        [When(@"the following basic information is set to the organization:")]
        [Given(@"the following basic information is set to the organization:")]
        public void WhenTheFollowingBasicInformationIsSetToTheOrganization(Table basicInformation)
        {
            TableRow info = basicInformation.Rows.Single();
            Try(() => OrganizationService.SetOrganizationBasicInformation(CurrentScenarioContext.OrganizationId, info["Business id"], info.GetOptionalValue("Oid"), 
                LocalizedTextHelper.CreateNamesCollection(info), LocalizedTextHelper.CreateDescriptionsCollection(info), info["Type"], info.GetOptionalValue("Municipality code"),
                info.GetOptionalFinnishDate("Valid from"), info.GetOptionalFinnishDate("Valid to"), LocalizedTextHelper.CreateNameAbbreviationsCollection(info)));
        }

        [Then(@"the organization has the following basic information:")]
        public void ThenTheOrganizationHasTheFollowingBasicInformation(Table basicInformation)
        {
            IOrganization result = OrganizationService.GetOrganization(CurrentScenarioContext.OrganizationId);
            OrganizationHelper.AssertOrganizationBasicInformation(basicInformation.Rows.Single(), result);
        }

        [Then(@"setting the basic information fails")]
        public void ThenSettingTheBasicInformationFails()
        {
            AssertCaughtException<ArgumentException>();
        }

        [Given(@"the following company is added as a sub organization of '(.+)'")]
        public void GivenTheFollowingCompanyIsAddedAsASubOrganizationOf(string parentOrganizationName, Table basicInformation)
        {
            TableRow info = basicInformation.Rows.Single();
            IHierarchicalOrganization parent = OrganizationHelper.GetOrganization(OrganizationService.GetActiveOrganizationHierarchy().ToList(), parentOrganizationName);
            OrganizationService.AddSubOrganization(parent.Id, info["Business id"], info.GetOptionalValue("Oid"), "Yritys", null,
                LocalizedTextHelper.CreateNamesCollection(info), LocalizedTextHelper.CreateDescriptionsCollection(info), info.GetOptionalFinnishDate("Valid from"),
                info.GetOptionalFinnishDate("Valid to"), null);
        }

        [When(@"the following basic information is set to organization '(.+)':")]
        public void WhenTheFollowingBasicInformationIsSetToOrganization(string organizationName, Table basicInformation)
        {
            TableRow info = basicInformation.Rows.Single();
            IOrganizationName organization = OrganizationService.GetActiveOrganizations().Single(o => o.Names.Any(name => name.LocalizedValue.Equals(organizationName)));
            Try(() => OrganizationService.SetOrganizationBasicInformation(organization.Id, info["Business id"], info.GetOptionalValue("Oid"),
                LocalizedTextHelper.CreateNamesCollection(info), LocalizedTextHelper.CreateDescriptionsCollection(info), info["Type"],
                info.GetOptionalValue("Municipality code"), info.GetOptionalFinnishDate("Valid from"), info.GetOptionalFinnishDate("Valid to"),null));
        }

        [Then(@"the organization '(.+)' has the following basic information:")]
        public void ThenTheOrganizationHasTheFollowingBasicInformation(string organizationName, Table basicInformation)
        {
            IOrganizationName organization = OrganizationService.GetActiveOrganizations().Single(o => o.Names.Any(name => name.LocalizedValue.Equals(organizationName)));
            OrganizationHelper.AssertOrganizationBasicInformation(basicInformation.Rows.Single(), OrganizationService.GetOrganization(organization.Id));
        }
    }
}
