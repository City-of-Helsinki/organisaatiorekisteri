using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.AngularApplication.BrowserTests.Infrastructure;
using OrganizationRegister.Tests.Infrastructure;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AngularApplication.BrowserTests.Features.Users
{
    [Binding]
    [Scope(Feature = "AddingUser")]
    internal sealed class AddingUserSteps
    {
        [Given(@"the user is starting to add a new user")]
        [When(@"the user is starting to add a new user")]
        public void GivenTheUserIsStartingToAddANewUser()
        {
            TestEnvironment.Driver.NavigateTo(string.Format("{0}Organizations/{1}/NewUser", 
                TestEnvironment.ApplicationHomePage,
                CurrentScenarioContext.OrganizationId.ToString("D")));
            Thread.Sleep(5000);
        }

        [Then(@"the user cannot be saved")]
        public void ThenTheUserCannotBeSaved()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementEnabled("saveNewUserButton"));
        }

        [Then(@"the user can be saved")]
        public void ThenTheUserCanBeSaved()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("saveNewUserButton"));
        }

        [When(@"focus is moved out of the email address field")]
        public void WhenFocusIsMovedOutOfTheEmailAddressField()
        {
            TestEnvironment.Driver.ClickElement("userPhoneNumberInput");
        }

        [Then(@"email address error message is displayed")]
        public void ThenEmailAddressErrorMessageIsDisplayed()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementDisplayed("userEmailAddressErrorMessage"));
        }

        [Then(@"email address error message is not displayed")]
        public void ThenEmailAddressErrorMessageIsNotDisplayed()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementDisplayed("userEmailAddressErrorMessage"));
        }

        [When(@"phone number '(.+)' is typed")]
        public void WhenPhoneNumberIsTyped(string phoneNumber)
        {
            TestEnvironment.Driver.TypeText("userPhoneNumberInput", phoneNumber);
        }

        [When(@"focus is moved out of the phone number field")]
        public void WhenFocusIsMovedOutOfThePhoneNumberField()
        {
            TestEnvironment.Driver.ClickElement("userEmailAddressInput");
        }

        [Then(@"phone number error message is displayed")]
        public void ThenPhoneNumberErrorMessageIsDisplayed()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementDisplayed("userPhoneNumberErrorMessage"));
        }

        [Then(@"phone number error message is not displayed")]
        public void ThenPhoneNumberErrorMessageIsNotDisplayed()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementDisplayed("userPhoneNumberErrorMessage"));
        }

        [Then(@"password mismatch error message is displayed")]
        public void ThenPasswordMismatchErrorMessageIsDisplayed()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementDisplayed("userConfirmedPasswordErrorMessage"));
        }

        [Then(@"password mismatch error message is not displayed")]
        public void ThenPasswordMismatchErrorMessageIsNotDisplayed()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementDisplayed("userConfirmedPasswordErrorMessage"));
        }

        [Then(@"password strength error message is displayed")]
        public void ThenPasswordStregthErrorMessageIsDisplayed()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementDisplayed("userPasswordErrorMessage"));
        }

        [Then(@"password strength error message is not displayed")]
        public void ThenPasswordStregthErrorMessageIsNotDisplayed()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementDisplayed("userPasswordErrorMessage"));
        }

        [Then(@"insufficient permissions error is displayed")]
        public void InsufficientPermissionsErrorIsDisplayed()
        {
            Assert.IsTrue(TestEnvironment.Driver.Value.Url.Contains("INSUFFICIENT_PERMISSIONS"));
        }

        [Then(@"administrator role is not available")]
        public void AdministratorRoleIsNotAvailable()
        {
            Assert.IsFalse(TestEnvironment.Driver.GetDropDownListContent("userRoleInput").Contains("PTV-pääkäyttäjä"));
        }
       
        [Then(@"the user '(.+)' / '(.+)' is logged in")]
        public void TheUserIsLoggedIn(string firstName, string lastName)
        {
            Assert.IsTrue(TestEnvironment.Driver.ElementHasText("userName", string.Format("{0} {1}", lastName, firstName)));
        }
    }
}