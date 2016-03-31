using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.AngularApplication.BrowserTests.Infrastructure;
using OrganizationRegister.Tests.Infrastructure;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AngularApplication.BrowserTests.Features.Users
{
    [Binding]
    [Scope(Feature = "ListUsers")]
    internal sealed class ListUsersSteps
    {
        [Given(@"the user is starting to add a new user")]
        public void GivenTheUserIsStartingToAddANewUser()
        {
            TestEnvironment.Driver.NavigateTo(string.Format("{0}Organizations/{1}/NewUser",
                TestEnvironment.ApplicationHomePage,
                CurrentScenarioContext.OrganizationId.ToString("D")));
            Thread.Sleep(6000);
        }

        [Then(@"the list should contain test user")]
        public void ThenTheListShouldContainTestUser()
        {
            TestEnvironment.Driver.ElementHasText("userList", "Test User");
        }

       

        [When(@"organization '(.*)' is selected from drop down list")]
        [Then(@"organization '(.*)' is selected from drop down list")]
        public void ThenOrganizationIsSelectedFromDropDownList(string org)
        {
            TestEnvironment.Driver.SelectDropDownListItem("userOrganizationInput", org);
            Thread.Sleep(5000);
        }

        [Then(@"the list should contain the new user '(.*)'")]
        public void ThenTheListShouldContainTheNewUser(string userEmail)
        {
            Assert.IsTrue(TestEnvironment.Driver.ElementHasText("userList", userEmail));
        }

        [Then(@"the list should not contain the new user '(.*)'")]
        public void ThenTheListShouldNotContainTheNewUser(string userEmail)
        {
            Assert.IsFalse(TestEnvironment.Driver.ElementHasText("userList", userEmail));
        }

        [Given(@"user navigates to users list page")]
        [When(@"user navigates to users list page")]
        public void WhenUserNavigatesToUsersListPage()
        {
            TestEnvironment.Driver.NavigateTo(string.Format("{0}Organizations/{1}/Users",
                TestEnvironment.ApplicationHomePage,
                CurrentScenarioContext.OrganizationId.ToString("D")));
            Thread.Sleep(6000);
        }

        
        [Then(@"page should not contain organization drop down element")]
        public void ThenPageShouldNotContainOrganizationDropDownElement()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementDisplayed("userOrganizationInput"));
        }

        [Then(@"page should not contain add new user button")]
        public void ThenPageShouldNotContainAddNewUserButton()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementDisplayed("addNewUserButton"));
        }

    }
}
