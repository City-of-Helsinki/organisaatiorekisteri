using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.AngularApplication.BrowserTests.Infrastructure;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AngularApplication.BrowserTests.Features.Organization
{
    [Binding]
    [Scope(Feature = "OrganizationTreeTests")]
    internal sealed class OrganizationTreeTests
    {

        [Given(@"the organization tree is visible")]
        public void GivenTheOrganizationTreeIsVisible()
        {
            var organizationTree = new AngularTree(TestEnvironment.Driver, "organizationTree");
            Assert.IsNotNull(organizationTree);
        }

        [Given(@"user starts to edit organization tree")]
        public void GivenUserStartsToEditOrganizationTree()
        {
            TestEnvironment.Driver.ClickElement("editStructureButton");
        }

        [Then(@"cancel button is visible")]
        public void ThenCancelButtonIsVisible()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementDisplayed("quitEditStructureButton"));
        }

        [When(@"user cancels editing the structure")]
        public void WhenUserCancelsEditingTheStructure()
        {
            TestEnvironment.Driver.ClickElement("quitEditStructureButton");
        }

        [Then(@"cancel button is not visible")]
        public void ThenCancelButtonIsNotVisible()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementDisplayed("quitEditStructureButton"));
        }

        [When(@"user starts to create new main organization")]
        public void WhenUserStartsToCreateNewMainOrganization()
        {
            TestEnvironment.Driver.ClickElement("addOrganizationButton");
            Thread.Sleep(5000);
        }

        [When(@"user starts to create new suborganization")]
        public void WhenUserStartsToCreateNewSuborganization()
        {
            TestEnvironment.Driver.ClickElement("addSubOrganizationButton");
            Thread.Sleep(5000);
        }


        [Then(@"new main organization can be created")]
        public void ThenNewMainOrganizationCanBeCreated()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementDisplayed("mainOrganizationHeader"));
        }

        [Then(@"new suborganization can be created")]
        public void ThenNewSuborganizationCanBeCreated()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementDisplayed("subOrganizationHeader"));
        }

        [Then(@"user cancels creating new organization")]
        public void ThenUserCancelsCreatingNewOrganization()
        {
            TestEnvironment.Driver.ClickElement("cancelAddingOrganizationButton");
            Thread.Sleep(5000);
        }


    }
}
