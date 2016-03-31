using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.AngularApplication.BrowserTests.Infrastructure;
using OrganizationRegister.Tests.Infrastructure;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AngularApplication.BrowserTests.Features.Service
{
    [Binding]
    [Scope(Feature = "AddingService")]
    internal sealed class AddingServiceSteps 
    {
        [Given(@"the user is starting to add a new service")]
        public void GivenTheUserIsStartingToAddANewService()
        {
            TestEnvironment.Driver.NavigateTo(string.Format("{0}Organizations/{1}/NewService", TestEnvironment.ApplicationHomePage,
                CurrentScenarioContext.OrganizationId.ToString("D")));
            Thread.Sleep(5000);
        }
        
        [When(@"service name '(.+)' has been entered")]
        public void WhenServiceNameHasBeenEntered(string serviceName)
        {
            TypeServiceName(serviceName);
        }

        [When(@"the short description '(.+)' has been entered for the service")]
        public void WhenTheShortDescriptionHasBeenEnteredForTheService(string shortDescription)
        {
            TypeServiceShortDescription(shortDescription);
        }

        [When(@"the service description '(.+)' has been entered")]
        public void WhenTheServiceDescriptionHasBeenEntered(string description)
        {
            TypeServiceDescription(description);
        }

        [When(@"'(.+)' as language has been selected")]
        public void WhenAsLanguageHasBeenSelected(string language)
        {
            SelectServiceLanguage(language);
        }

        [Then(@"service cannot be saved")]
        public void ThenServiceCannotBeSaved()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementEnabled("addServiceFirstStepSaveAndQuitButton"));
        }
        
        [Then(@"service can be saved")]
        public void ThenServiceCanBeSaved()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("addServiceFirstStepSaveAndQuitButton"));
        }

        [When(@"adding a service is cancelled")]
        public void WhenAddingAServiceIsCancelled()
        {
            TestEnvironment.Driver.ClickElement("cancelAddingServiceButton");
        }

        [Then(@"the user is redirected to the organization's service list")]
        public void ThenTheUserIsRedirectedToTheOrganizationServiceList()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementDisplayed("organizationServiceListTable"));
        }

        [Given(@"the following service basic information is typed:")]
        public void GivenTheFollowingServiceBasicInformationIsTyped(Table basicInformation)
        {
            TableRow info = basicInformation.Rows.Single();
            TypeServiceName(info["Name"]);
            TypeServiceShortDescription(info["Short description"]);
            TypeServiceDescription(info["Description"]);
            SelectServiceLanguage(info["Language"]);
        }

        [When(@"the service is saved")]
        public void WhenTheServiceIsSaved()
        {
            TestEnvironment.Driver.ClickElement("addServiceFirstStepSaveAndQuitButton");
            Thread.Sleep(5000);
        }

        [Then(@"the list contains service '(.+)'")]
        public void ThenTheListContainsService(string serviceName)
        {
            Assert.IsTrue(TestEnvironment.Driver.IsTableCellLinkPresent("organizationServiceListTable", serviceName));
        }

        [Given(@"service basic information is filled")]
        public void GivenServiceBasicInformationIsFilled()
        {
            TypeServiceName("name");
            TypeServiceDescription("description");
            TypeServiceShortDescription("short");
            SelectServiceLanguage("suomi");
        }

        [Given(@"the user moves forward to the classification section")]
        public void GivenTheUserMovesForwardToTheClassificationSection()
        {
            TestEnvironment.Driver.ClickElement("addServiceSecondStepSaveAndMoveToNextStepButton");
        }

        [Then(@"classification information cannot be saved")]
        public void ThenClassificationInformationCannotBeSaved()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementEnabled("addServiceThirdStepSaveAndQuitButton"));
        }

        [Given(@"service class '(.+)' is selected")]
        [When(@"service class '(.+)' is selected")]
        public void WhenServiceClassIsSelected(string serviceClass)
        {
            var serviceClassTree = new AngularTree(TestEnvironment.Driver, "serviceClassTree");
            serviceClassTree.Select(serviceClass);
            TestEnvironment.Driver.ClickElement("addServiceClassArrow");
        }

        [Given(@"ontology term '(.+)' is selected")]
        [When(@"ontology term '(.+)' is selected")]
        public void WhenOntologyTermIsSelected(string ontologyTerm)
        {
            TestEnvironment.Driver.TypeText("ontologyTermSearchInput", ontologyTerm);
            TestEnvironment.Driver.SelectDropDownListItem("ontologyTermList", ontologyTerm);
            TestEnvironment.Driver.ClickElement("addOntologyTermArrow");
        }

        [Given(@"target group '(.+)' is selected")]
        [When(@"target group '(.+)' is selected")]
        public void WhenTargetGroupIsSelected(string targetGroup)
        {
            var targetGroupTree = new AngularTree(TestEnvironment.Driver, "targetGroupTree");
            targetGroupTree.Select(targetGroup);
            TestEnvironment.Driver.ClickElement("addTargetGroupArrow");
        }

        [Then(@"classification information can be saved")]
        public void ThenClassificationInformationCanBeSaved()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("addServiceThirdStepSaveAndQuitButton"));
        }

        [When(@"'(.+)' is typed to the service class search field")]
        public void WhenIsTypedToTheServiceClassSearchField(string searchCriteria)
        {
            TestEnvironment.Driver.TypeText("serviceClassSearchInput", searchCriteria);
        }

        [Then(@"service class '(.+)' is selected")]
        public void ThenServiceClassIsSelected(string serviceClass)
        {
            Assert.IsTrue(TestEnvironment.Driver.ElementHasText("selectedServiceClassesList", serviceClass));
        }

        [When(@"service class selection '(.+)' is removed")]
        public void WhenServiceClassSelectionIsRemoved(string serviceClass)
        {
            var classificationSelection = new ClassificationSelection(TestEnvironment.Driver, "selectedServiceClassesList");
            classificationSelection.Remove(serviceClass);
            Thread.Sleep(500);
        }

        [Then(@"service class '(.+)' is not selected")]
        public void ThenServiceClassIsNotSelected(string serviceClass)
        {
            Assert.IsFalse(TestEnvironment.Driver.ElementHasText("selectedServiceClassesList", serviceClass));
        }

        [Then(@"ontology term '(.+)' is selected")]
        public void ThenOntologyTermIsSelected(string ontologyTerm)
        {
            Assert.IsTrue(TestEnvironment.Driver.ElementHasText("selectedOntologyTermsList", ontologyTerm));
        }

        [Then(@"ontology term '(.+)' is not selected")]
        public void ThenOntologyTermIsNotSelected(string ontologyTerm)
        {
            Assert.IsFalse(TestEnvironment.Driver.ElementHasText("selectedOntologyTermsList", ontologyTerm));
        }

        [When(@"ontology term selection '(.+)' is removed")]
        public void WhenOntologyTermSelectionIsRemoved(string ontologyTerm)
        {
            var classificationSelection = new ClassificationSelection(TestEnvironment.Driver, "selectedOntologyTermsList");
            classificationSelection.Remove(ontologyTerm);
            Thread.Sleep(500);
        }

        [When(@"'(.+)' is typed to the target group search field")]
        public void WhenIsTypedToTheTargetGroupSearchField(string searchCriteria)
        {
            TestEnvironment.Driver.TypeText("targetGroupSearchInput", searchCriteria);
        }

        [Then(@"target group '(.+)' is selected")]
        public void ThenTargetGroupIsSelected(string targetGroup)
        {
            Assert.IsTrue(TestEnvironment.Driver.ElementHasText("selectedTargetGroupsList", targetGroup));
        }

        [When(@"target group selection '(.+)' is removed")]
        public void WhenTargetGroupSelectionIsRemoved(string targetGroup)
        {
            var classificationSelection = new ClassificationSelection(TestEnvironment.Driver, "selectedTargetGroupsList");
            classificationSelection.Remove(targetGroup);
            Thread.Sleep(500);
        }

        [Then(@"target group '(.+)' is not selected")]
        public void ThenTargetGroupIsNotSelected(string targetGroup)
        {
            Assert.IsFalse(TestEnvironment.Driver.ElementHasText("selectedTargetGroupsList", targetGroup));
        }

        [When(@"'(.+)' is typed to the life event search field")]
        public void WhenIsTypedToTheLifeEventSearchField(string searchCriteria)
        {
            TestEnvironment.Driver.TypeText("lifeEventSearchInput", searchCriteria);
        }

        [When(@"life event '(.+)' is selected")]
        public void WhenLifeEventIsSelected(string lifeEvent)
        {
            var lifeEventTree = new AngularTree(TestEnvironment.Driver, "lifeEventTree");
            lifeEventTree.Select(lifeEvent);
            TestEnvironment.Driver.ClickElement("addLifeEventArrow");
        }

        [Then(@"life event '(.+)' is selected")]
        public void ThenLifeEventIsSelected(string lifeEvent)
        {
            Assert.IsTrue(TestEnvironment.Driver.ElementHasText("selectedLifeEventsList", lifeEvent));
        }

        [When(@"life event selection '(.+)' is removed")]
        public void WhenLifeEventSelectionIsRemoved(string lifeEvent)
        {
            var classificationSelection = new ClassificationSelection(TestEnvironment.Driver, "selectedLifeEventsList");
            classificationSelection.Remove(lifeEvent);
            Thread.Sleep(500);
        }

        [Then(@"life event '(.+)' is not selected")]
        public void ThenLifeEventIsNotSelected(string lifeEvent)
        {
            Assert.IsFalse(TestEnvironment.Driver.ElementHasText("selectedLifeEventsList", lifeEvent));
        }

        [When(@"service classification is saved")]
        public void WhenServiceClassificationIsSaved()
        {
            TestEnvironment.Driver.ClickElement("addServiceThirdStepSaveAndQuitButton");
        }

        [Then(@"the listed service contains ontology terms '(.+)'")]
        public void ThenTheListedServiceContainsOntologyTerms(string ontologyTerms)
        {
            TestEnvironment.Driver.ElementHasText("organizationServiceListTable", ontologyTerms);
        }

        [Then(@"the listed service contains service classes '(.+)'")]
        public void ThenTheListedServiceContainsServiceClasses(string serviceClasses)
        {
            TestEnvironment.Driver.ElementHasText("organizationServiceListTable", serviceClasses);
        }

        [When(@"service class '(.+)' is collapsed")]
        public void WhenServiceClassIsCollapsed(string serviceClass)
        {
            var serviceClassTree = new AngularTree(TestEnvironment.Driver, "serviceClassTree");
            serviceClassTree.Collapse(serviceClass);
        }

        [When(@"service class '(.+)' is expanded")]
        public void WhenServiceClassIsExpanded(string serviceClass)
        {
            var serviceClassTree = new AngularTree(TestEnvironment.Driver, "serviceClassTree");
            serviceClassTree.Expand(serviceClass);
        }

        [When(@"target group '(.+)' is collapsed")]
        public void WhenTargetGroupIsCollapsed(string targetGroup)
        {
            var tree = new AngularTree(TestEnvironment.Driver, "targetGroupTree");
            tree.Collapse(targetGroup);
        }

        [When(@"target group '(.+)' is expanded")]
        public void WhenTargetGroupIsExpanded(string targetGroup)
        {
            var tree = new AngularTree(TestEnvironment.Driver, "targetGroupTree");
            tree.Expand(targetGroup);
        }

        [When(@"life event '(.+)' is collapsed")]
        public void WhenLifeEventIsCollapsed(string lifeEvent)
        {
            var tree = new AngularTree(TestEnvironment.Driver, "lifeEventTree");
            tree.Collapse(lifeEvent);
        }

        [When(@"life event '(.+)' is expanded")]
        public void WhenLifeEventIsExpanded(string lifeEvent)
        {
            var tree = new AngularTree(TestEnvironment.Driver, "lifeEventTree");
            tree.Expand(lifeEvent);
        }

        private static void SelectServiceLanguage(string language)
        {
            ChosenDropDown dropDown = new ChosenDropDown(TestEnvironment.Driver, "serviceLanguages");
            dropDown.Select(language);
        }

        private static void TypeServiceShortDescription(string shortDescription)
        {
            TestEnvironment.Driver.TypeText("serviceShortDescription", shortDescription);
        }

        private static void TypeServiceDescription(string description)
        {
            TestEnvironment.Driver.TypeText("serviceDescription", description);
        }

        private static void TypeServiceName(string serviceName)
        {
            TestEnvironment.Driver.TypeText("serviceName", serviceName);
        }
    }
}