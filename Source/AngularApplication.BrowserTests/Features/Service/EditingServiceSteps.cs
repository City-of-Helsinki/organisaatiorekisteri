using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Affecto.Testing.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.AngularApplication.BrowserTests.Infrastructure;
using OrganizationRegister.Tests.Infrastructure;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AngularApplication.BrowserTests.Features.Service
{
    [Binding]
    [Scope(Feature = "EditingService")]
    public class EditingServiceSteps
    {
        [Given(@"organization services page is visible")]
        public void GivenOrganizationServicesPageIsVisible()
        {
            TestEnvironment.Driver.NavigateTo(string.Format("{0}Organizations/{1}/Services", 
                TestEnvironment.ApplicationHomePage, CurrentScenarioContext.OrganizationId));
            TestEnvironment.Driver.Value.Navigate().Refresh();
            Thread.Sleep(5000);
        }

        [Given(@"the service '(.+)' is selected")]
        [When(@"the service '(.+)' is selected")]
        public void GivenTheServiceIsSelected(string serviceName)
        {
            TestEnvironment.Driver.ClickLink(serviceName);
            Thread.Sleep(2000);
        }

        [Given(@"the basic information is put in edit mode")]
        [When(@"the basic information is put in edit mode")]
        public void WhenTheBasicInformationIsPutInEditMode()
        {
            TestEnvironment.Driver.ClickElement("editServiceBasicInformationButton");
            Thread.Sleep(1000);
        }

        [When(@"The following information is edited")]
        public void WhenTheFollowingInformationIsEdited(Table basicInformation)
        {
            TableRow info = basicInformation.Rows.Single();
            TestEnvironment.Driver.TypeText("serviceName", info["Service name"]);
            TestEnvironment.Driver.TypeText("serviceAlternateName", info["Alternate name"]);
            TestEnvironment.Driver.TypeText("serviceShortDescription", info["Short description"]);
            TestEnvironment.Driver.TypeText("serviceDescription", info["Description"]);
            TestEnvironment.Driver.TypeText("serviceRequirement", info["Requirements"]);
            TestEnvironment.Driver.TypeText("serviceUserInstruction", info["User instructions"]);

            var languagesElement = new ChosenDropDown(TestEnvironment.Driver, "serviceLanguages");
            languagesElement.Clear();
            // todo: selecting multiple options not working?
            languagesElement.Select(info.SplitCommaSeparatedText("Languages").Single());
        }

        [Then(@"following service information is displayed")]
        public void ThenFollowingServiceInformationIsDisplayed(Table basicInformation)
        {
            TableRow info = basicInformation.Rows.Single();
            Assert.IsTrue(TestEnvironment.Driver.ElementHasText("viewServiceName", info["Service name"]));
            Assert.IsTrue(TestEnvironment.Driver.ElementHasText("viewServiceAlternateName", info["Alternate name"]));
            Assert.IsTrue(TestEnvironment.Driver.ElementHasText("viewServiceShortDescription", info["Short description"]));
            Assert.IsTrue(TestEnvironment.Driver.ElementHasText("viewServiceDescription", info["Description"]));
            Assert.IsTrue(TestEnvironment.Driver.ElementHasText("viewServiceLanguages", info["Languages"]));
            Assert.IsTrue(TestEnvironment.Driver.ElementHasText("viewServiceRequirements", info["Requirements"]));
            Assert.IsTrue(TestEnvironment.Driver.ElementHasText("viewServiceUserInstructions", info["User instructions"]));
        }

        [Then(@"following service information is displayed in edit mode")]
        public void ThenFollowingServiceInformationIsDisplayedInEditMode(Table basicInformation)
        {
            TableRow info = basicInformation.Rows.Single();
            Assert.IsTrue(TestEnvironment.Driver.InputHasText("serviceName", info["Service name"]));
            Assert.IsTrue(TestEnvironment.Driver.InputHasText("serviceAlternateName", info["Alternate name"]));
            Assert.IsTrue(TestEnvironment.Driver.InputHasText("serviceShortDescription", info["Short description"]));
            Assert.IsTrue(TestEnvironment.Driver.InputHasText("serviceDescription", info["Description"]));
            Assert.IsTrue(TestEnvironment.Driver.InputHasText("serviceRequirement", info["Requirements"]));
            Assert.IsTrue(TestEnvironment.Driver.InputHasText("serviceUserInstruction", info["User instructions"]));

            var languagesElement = new ChosenDropDown(TestEnvironment.Driver, "serviceLanguages");
            foreach (string language in info.SplitCommaSeparatedText("Languages"))
            {
                Assert.IsTrue(languagesElement.IsSelected(language));
            }
        }

        [Then(@"save button is disabled")]
        public void ThenSaveButtonIsDisabled()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementEnabled("saveServiceBasicInformationButton"));
        }

        [When(@"the service information is saved")]
        public void ThenTheServiceInformationIsSaved()
        {
            TestEnvironment.Driver.ClickElement("saveServiceBasicInformationButton");
            Thread.Sleep(5000);
        }

        [Then(@"following service classification information is displayed")]
        public void ThenFollowingServiceClassificationInformationIsDisplayed(Table table)
        {
            TableRow classificationData = table.Rows.Single();
            string keywords = classificationData["Keywords"];
            AssertServiceClassification(classificationData, "serviceClassReadOnlyList", "ontologyTermReadOnlyList", "targetGroupReadOnlyList", "lifeEventReadOnlyList");
            Assert.IsTrue(TestEnvironment.Driver.ElementHasText("keywordReadOnlyList", keywords));
        }

        [Given(@"the classification information is put in edit mode")]
        public void GivenTheClassificationInformationIsPutInEditMode()
        {
            TestEnvironment.Driver.ClickElement("editClassificationAndOnthologyTermsButton");
            Thread.Sleep(1000);
        }

        [Then(@"following service classification information is displayed in edit mode")]
        public void ThenFollowingServiceClassificationInformationIsDisplayedInEditMode(Table table)
        {
            TableRow classificationData = table.Rows.Single();
            string keywords = classificationData["Keywords"];
            AssertServiceClassification(classificationData, "selectedServiceClassesList", "selectedOntologyTermsList", "selectedTargetGroupsList", "selectedLifeEventsList");
            Assert.IsTrue(TestEnvironment.Driver.InputHasText("serviceKeywords", keywords));
        }

        [When(@"service keywords are cleared")]
        public void WhenServiceKeywordsAreCleared()
        {
            TestEnvironment.Driver.TypeText("serviceKeywords", "");
        }

        [When(@"service class '(.+)' is removed")]
        public void WhenServiceClassIsRemoved(string serviceClass)
        {
            var classificationSelection = new ClassificationSelection(TestEnvironment.Driver, "selectedServiceClassesList");
            classificationSelection.Remove(serviceClass);
        }

        [When(@"ontology term '(.+)' is removed")]
        public void WhenOntologyTermIsRemoved(string ontologyTerm)
        {
            var classificationSelection = new ClassificationSelection(TestEnvironment.Driver, "selectedOntologyTermsList");
            classificationSelection.Remove(ontologyTerm);
        }

        [When(@"target group '(.+)' is removed")]
        public void WhenTargetGroupIsRemoved(string targetGroup)
        {
            var classificationSelection = new ClassificationSelection(TestEnvironment.Driver, "selectedTargetGroupsList");
            classificationSelection.Remove(targetGroup);
        }

        [When(@"life event '(.+)' is removed")]
        public void WhenLifeEventIsRemoved(string lifeEvent)
        {
            var classificationSelection = new ClassificationSelection(TestEnvironment.Driver, "selectedLifeEventsList");
            classificationSelection.Remove(lifeEvent);
        }

        [When(@"the service classification information is saved")]
        public void WhenTheServiceClassificationInformationIsSaved()
        {
            TestEnvironment.Driver.ClickElement("saveClassificationAndOnthologyTermsButton");
            Thread.Sleep(5000);
        }

        [Then(@"service has no service class '(.+)'")]
        public void ThenServiceHasNoServiceClass(string serviceClass)
        {
            Assert.IsFalse(TestEnvironment.Driver.ElementHasText("serviceClassReadOnlyList", serviceClass));
        }

        [Then(@"service has no ontology term '(.+)'")]
        public void ThenServiceHasNoOntologyTerm(string ontologyTerm)
        {
            Assert.IsFalse(TestEnvironment.Driver.ElementHasText("ontologyTermReadOnlyList", ontologyTerm));
        }

        [Then(@"service has no target group '(.+)'")]
        public void ThenServiceHasNoTargetGroup(string targetGroup)
        {
            Assert.IsFalse(TestEnvironment.Driver.ElementHasText("targetGroupReadOnlyList", targetGroup));
        }

        [Then(@"service has no life event '(.+)'")]
        public void ThenServiceHasNoLifeEvent(string lifeEvent)
        {
            Assert.IsFalse(TestEnvironment.Driver.ElementHasText("lifeEventReadOnlyList", lifeEvent));
        }

        [When(@"basic information editing is cancelled")]
        public void WhenBasicInformationEditingIsCancelled()
        {
            TestEnvironment.Driver.ClickElement("cancelEditingServiceBasicInformation");
        }

        [When(@"classification information editing is cancelled")]
        public void WhenClassificationInformationEditingIsCancelled()
        {
            TestEnvironment.Driver.ClickElement("cancelEditingServiceClassificationInformation");
        }

        [When(@"service language '(.+)' is added")]
        public void WhenServiceLanguageIsAdded(string language)
        {
            var languagesElement = new ChosenDropDown(TestEnvironment.Driver, "serviceLanguages");
            languagesElement.Select(language);
        }

        private static void AssertServiceClassification(TableRow classificationData, string serviceClassListElementId, string ontologyTermListElementId, 
            string targetGroupListElementId, string lifeEventListElementId)
        {
            IReadOnlyCollection<string> serviceClasses = classificationData.SplitCommaSeparatedText("Service classes");
            IReadOnlyCollection<string> ontologyTerms = classificationData.SplitCommaSeparatedText("Ontology terms");
            IReadOnlyCollection<string> targetGroups = classificationData.SplitCommaSeparatedText("Target groups");
            IReadOnlyCollection<string> lifeEvents = classificationData.SplitCommaSeparatedText("Life events");

            foreach (string serviceClass in serviceClasses)
            {
                Assert.IsTrue(TestEnvironment.Driver.ElementHasText(serviceClassListElementId, serviceClass));
            }
            foreach (string ontologyTerm in ontologyTerms)
            {
                Assert.IsTrue(TestEnvironment.Driver.ElementHasText(ontologyTermListElementId, ontologyTerm));
            }
            foreach (string targetGroup in targetGroups)
            {
                Assert.IsTrue(TestEnvironment.Driver.ElementHasText(targetGroupListElementId, targetGroup));
            }
            foreach (string lifeEvent in lifeEvents)
            {
                Assert.IsTrue(TestEnvironment.Driver.ElementHasText(lifeEventListElementId, lifeEvent));
            }
        }
    }
}