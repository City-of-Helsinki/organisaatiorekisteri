using System.Threading;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AngularApplication.BrowserTests.Infrastructure
{
    [Binding]
    internal class UserManagementSteps
    {
        [Given(@"the user '(.*)' / '(.*)' is logged in")]
        [When(@"the user '(.*)' / '(.*)' is logged in")]
        public void WhenTheUserIsLoggedIn(string email, string password)
        {
            LoginUser(email, password);
        }

        [Given(@"the basic user is logged in")]
        public void GivenTheBasicUserIsLoggedIn()
        {
            LoginUser(TestEnvironment.BasicTestUserEmailAddress, TestEnvironment.TestUserPassword);
        }

        [Given(@"the organization administrator user is logged in")]
        public void GivenTheOrganizationAdministratorUserIsLoggedIn()
        {
            LoginUser(TestEnvironment.OrganizationAdministratorTestUserEmailAddress, TestEnvironment.TestUserPassword);
        }

        [Given(@"the administrator user is logged in")]
        public void GivenTheAdministratorUserIsLoggedIn()
        {
            LoginUser(TestEnvironment.AdministratorTestUserEmailAddress, TestEnvironment.TestUserPassword);
        }

        [Given(@"last name '(.+)' is typed")]
        [When(@"last name '(.+)' is typed")]
        public void WhenLastNameIsTyped(string lastName)
        {
            TestEnvironment.Driver.TypeText("userLastNameInput", lastName);
        }

        [Given(@"first name '(.+)' is typed")]
        [When(@"first name '(.+)' is typed")]
        public void WhenFirstNameIsTyped(string firstName)
        {
            TestEnvironment.Driver.TypeText("userFirstNameInput", firstName);
        }

        [Given(@"email address '(.+)' is typed")]
        [When(@"email address '(.+)' is typed")]
        public void WhenEmailAddressIsTyped(string emailAddress)
        {
            TestEnvironment.Driver.TypeText("userEmailAddressInput", emailAddress);
        }

        [Given(@"password '(.+)' is typed")]
        [When(@"password '(.+)' is typed")]
        public void WhenPasswordIsTyped(string password)
        {
            TestEnvironment.Driver.TypeText("userPasswordInput", password);
        }

        [Given(@"password '(.+)' is typed again")]
        [When(@"password '(.+)' is typed again")]
        public void WhenPasswordIsTypedAgain(string password)
        {
            TestEnvironment.Driver.TypeText("userPasswordAgainInput", password);
        }

        [Given(@"organization '(.+)' is selected")]
        [When(@"organization '(.+)' is selected")]
        public void WhenOrganizationIsSelected(string organizationName)
        {
            TestEnvironment.Driver.SelectDropDownListItem("userOrganizationInput", organizationName);
        }

        [Given(@"role '(.+)' is selected")]
        [When(@"role '(.+)' is selected")]
        public void WhenRoleIsSelected(string roleName)
        {
            TestEnvironment.Driver.SelectDropDownListItem("userRoleInput", roleName);
        }

        [Given(@"the user is saved")]
        [When(@"the user is saved")]
        public void WhenTheUserIsSaved()
        {
            TestEnvironment.Driver.ClickElement("saveNewUserButton");
            Thread.Sleep(3000);
        }

        [Given(@"current user logs out")]
        [When(@"current user logs out")]
        public void WhenCurrentUserLogsOut()
        {
            Thread.Sleep(3000);
            TestEnvironment.Driver.ClickElement("logoutButton");
            Thread.Sleep(5000);
        }

        private void LoginUser(string email, string password)
        {
            TestEnvironment.Driver.NavigateTo(string.Format("{0}Login", TestEnvironment.ApplicationHomePage));
            Thread.Sleep(5000);
            TestEnvironment.Driver.TypeText("loginEmailInputField", email);
            TestEnvironment.Driver.TypeText("loginPasswordInputField", password);
            TestEnvironment.Driver.ClickElement("loginButton");
            Thread.Sleep(20000);
        }
    }
}