using System.Linq;
using Affecto.Testing.SpecFlow;
using OrganizationRegister.AcceptanceTests.Infrastructure;
using OrganizationRegister.Application.Service;
using OrganizationRegister.Tests.Infrastructure;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Features.Service
{
    [Binding]
    [Scope(Feature = "UpdatingServiceBasicInformation")]
    internal sealed class UpdatingServiceBasicInformationSteps : StepDefinition
    {
        [When(@"the following basic information is set to the service")]
        [Given(@"the following basic information is set to the service")]
        public void WhenTheFollowingBasicInformationIsSetToTheService(Table basicInfo)
        {
            TableRow serviceData = basicInfo.Rows.Single();
            ServiceService.SetServiceBasicInformation(CurrentScenarioContext.OrganizationId, CurrentScenarioContext.ServiceId, LocalizedTextHelper.CreateNamesCollection(serviceData),
                LocalizedTextHelper.CreateAlternateNamesCollection(serviceData), LocalizedTextHelper.CreateDescriptionsCollection(serviceData), 
                LocalizedTextHelper.CreateShortDescriptionsCollection(serviceData), LocalizedTextHelper.CreateUserInstructionsCollection(serviceData), 
                serviceData.SplitCommaSeparatedText("Languages"), LocalizedTextHelper.CreateRequirementsCollection(serviceData));
        }

        [Then(@"the organization has the following service:")]
        public void ThenTheOrganizationHasTheFollowingService(Table basicInfo)
        {
            TableRow serviceRow = basicInfo.Rows.Single();
            IService service = ServiceService.GetService(CurrentScenarioContext.OrganizationId, CurrentScenarioContext.ServiceId);
            ServiceHelper.AssertServiceBasicInformation(serviceRow, service);
        }
    }
}
