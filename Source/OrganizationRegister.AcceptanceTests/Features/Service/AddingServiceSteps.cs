using System.Linq;
using Affecto.Testing.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.AcceptanceTests.Infrastructure;
using OrganizationRegister.Application.Service;
using OrganizationRegister.Common;
using OrganizationRegister.Tests.Infrastructure;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Features.Service
{
    [Binding]
    [Scope(Feature = "AddingService")]
    internal class AddingServiceSteps : StepDefinition
    {
        [When(@"the following service is added to the organization:")]
        public void WhenTheFollowingServiceIsAddedToTheOrganization(Table service)
        {
            TableRow serviceData = service.Rows.Single();
            ServiceService.AddService(CurrentScenarioContext.OrganizationId, LocalizedTextHelper.CreateNamesCollection(serviceData), 
                LocalizedTextHelper.CreateAlternateNamesCollection(serviceData),
                LocalizedTextHelper.CreateDescriptionsCollection(serviceData), LocalizedTextHelper.CreateShortDescriptionsCollection(serviceData), 
                LocalizedTextHelper.CreateUserInstructionsCollection(serviceData), serviceData.SplitCommaSeparatedText("Languages"), 
                LocalizedTextHelper.CreateRequirementsCollection(serviceData));
        }

        [Then(@"the organization has the following service:")]
        public void ThenTheOrganizationHasTheFollowingService(Table expectedService)
        {
            TableRow serviceRow = expectedService.Rows.Single();
            IServiceListItem serviceListItem = ServiceService.GetServices(CurrentScenarioContext.OrganizationId).Single();
            IService service = ServiceService.GetService(CurrentScenarioContext.OrganizationId, serviceListItem.Id);
            ServiceHelper.AssertServiceBasicInformation(serviceRow, service);
            AssertEqualServiceNames(serviceListItem, service);
            Assert.IsTrue(string.IsNullOrWhiteSpace(serviceListItem.ServiceClasses));
            Assert.IsTrue(string.IsNullOrWhiteSpace(serviceListItem.OntologyTerms));
        }

        private static void AssertEqualServiceNames(IServiceListItem serviceListItem, IService service)
        {
            Assert.AreEqual(serviceListItem.Names.Count(), service.Names.Count());
            foreach (LocalizedText name in serviceListItem.Names)
            {
                Assert.IsTrue(service.Names.Contains(name));
            }
        }
    }
}
