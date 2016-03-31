using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.AngularApplication.BrowserTests.Infrastructure;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AngularApplication.BrowserTests.Features.Organization
{
    [Binding]
    [Scope(Feature = "AddingOrganization")]
    internal sealed class AddingOrganizationSteps
    {
        [Given(@"the user is starting to add a new organization")]
        public void GivenTheUserIsStartingToAddANewOrganization()
        {
            TestEnvironment.Driver.NavigateTo(string.Format("{0}Organizations", TestEnvironment.ApplicationHomePage));
            Thread.Sleep(5000);
        }

        [Then(@"organization cannot be saved")]
        public void ThenOrganizationCannotBeSaved()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementEnabled("addOrganizationFirstStepSaveAndQuitButton"));
            Assert.IsFalse(TestEnvironment.Driver.IsElementEnabled("addOrganizationFirstStepSaveAndNextButton"));
        }

        [Given(@"provider type '(.+)' is selected")]
        [When(@"provider type '(.+)' is selected")]
        public void WhenProviderTypeIsSelected(string providerType)
        {
            SetProviderType(providerType);
        }

        [When(@"municipality code '(.+)' is typed")]
        public void WhenMunicipalityIdIsTyped(string municipalityCode)
        {
            TestEnvironment.Driver.TypeText("organizationMunicipalityCode", municipalityCode);
        }

        [Given(@"name '(.+)' is typed")]
        [When(@"name '(.+)' is typed")]
        public void WhenNameIsTyped(string name)
        {
            SetOrganizationName(name);
        }

        [When(@"business id '(.+)' is typed")]
        public void WhenBusinessIdIsTyped(string businessId)
        {
            SetOrganizationBusinessId(businessId);
        }

        [Then(@"organization can be saved")]
        public void ThenOrganizationCanBeSaved()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("addOrganizationFirstStepSaveAndQuitButton"));
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("addOrganizationFirstStepSaveAndNextButton"));
        }

        [When(@"focus is moved out of the business id form field")]
        public void WhenFocusIsMovedToTheNextFormField()
        {
            TestEnvironment.Driver.ClickElement("organizationOid");
            Thread.Sleep(2000);
        }

        [Then(@"business id error message is displayed")]
        public void ThenBusinessIdErrorMessageIsDisplayed()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementDisplayed("organizationBusinessIdErrorMessage"));
        }

        [Then(@"business id error message is not displayed")]
        public void ThenBusinessIdErrorMessageIsNotDisplayed()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementDisplayed("organizationBusinessIdErrorMessage"));
        }

        [Given(@"mandatory fields are filled")]
        public void GivenMandatoryFieldsAreFilled()
        {
            SetProviderType("Yritykset");
            SetOrganizationName("Firma A");
            SetOrganizationBusinessId("1572860-0");
        }
        

        [Then(@"step two of the form is available")]
        [Given(@"step two of the form is available")]
        public void ThenStepTwoOfTheFormIsAvailable()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("addOrganizationFirstStepSaveAndNextButton"));
        }

        [Then(@"mandatory information is saved and user moves to contact information step")]
        [When(@"mandatory information is saved and user moves to contact information step")]
        public void MandatoryInformationIsSavedAndUserMovesToContactInformationStep()
        {
            TestEnvironment.Driver.ClickElement("addOrganizationFirstStepSaveAndNextButton");
            Thread.Sleep(2000);
        }

        [When(@"phone number '(.*)' is typed")]
        public void WhenPhoneNumberIsTyped(string phoneNumber)
        {
            TestEnvironment.Driver.TypeText("phoneNumber", phoneNumber);
        }

        [When(@"focus is moved out of the phone number form field")]
        public void WhenFocusIsMovedOutOfThePhoneNumberFormField()
        {
            TestEnvironment.Driver.ClickElement("phoneCallFee");
            Thread.Sleep(2000);
        }

        [Then(@"phone error message is shown")]
        public void ThenPhoneErrorMessageIsShown()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementDisplayed("phoneNumberErrorMessage"));
        }

        [When(@"phone call price '(.*)' is typed")]
        public void WhenPhoneCallPriceIsTyped(string price)
        {
            TestEnvironment.Driver.TypeText("phoneCallFee", price);
        }

        [When(@"focus is moved from phone number price form field")]
        public void WhenFocusIsMovedFromPhoneNumberPriceFormField()
        {
            TestEnvironment.Driver.ClickElement("email");
            Thread.Sleep(2000);
        }

        [When(@"email '(.*)' is typed")]
        public void WhenEmailIsTyped(string email)
        {
            TestEnvironment.Driver.TypeText("email", email);
        }

        [When(@"focus is moved from email form field")]
        public void WhenFocusIsMovedFromEmailFormField()
        {
            TestEnvironment.Driver.ClickElement("phoneCallFee");
            Thread.Sleep(2000);
        }

        [Then(@"email error message is shown")]
        public void ThenEmailErrorMessageIsShown()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementDisplayed("emailErrorMessage"));
        }

        [Then(@"email error message is not shown")]
        public void ThenEmailErrorMessageIsNotShown()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementDisplayed("emailErrorMessage"));
        }

        [Then(@"municipality code field is not visible")]
        public void ThenMunicipalityCodeFieldIsNotVisible()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementDisplayed("organizationTypeDiv"));
        }

        [Then(@"municipality code field is visible")]
        public void ThenMunicipalityCodeFieldIsVisible()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementDisplayed("organizationTypeDiv"));
        }

        [Then(@"contact information cannot be saved")]
        public void ThenContactInformationCannotBeSaved()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementEnabled("saveOrganizationContactInformationAndMoveToNextStep"));
        }

        [Then(@"contact information can be saved")]
        public void ThenContactInformationCanBeSaved()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("saveOrganizationContactInformationAndMoveToNextStep"));
        }

        [When(@"mandatory information is saved and user moves to step two")]
        public void WhenMandatoryInformationIsSavedAndUserMovesToStepTwo()
        {
            TestEnvironment.Driver.ClickElement("addOrganizationFirstStepSaveAndNextButton");
            Thread.Sleep(3000);
        }

        [Then(@"save and move to the step three is available")]
        public void ThenSaveAndMoveToTheStepThreeIsAvailable()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("saveOrganizationContactInformationAndMoveToNextStep"));
        }

        [Then(@"go back to step one is available")]
        public void ThenGoBackToStepOneIsAvailable()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("saveOrganizationContactInformationAndMoveToPreviousStep"));
        }

        [Then(@"save step two and quit is available")]
        public void ThenSaveStepTwoAndQuitIsAvailable()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("saveOrganizationContactInformationAndQuit"));
        }

        [Then(@"user moves to step three")]
        [When(@"user moves to step three")]
        public void WhenUserMovesToStepThree()
        {
            TestEnvironment.Driver.ClickElement("saveOrganizationContactInformationAndMoveToNextStep");
            Thread.Sleep(3000);
        }

        [Then(@"move to the step four is available")]
        public void ThenMoveToTheStepFourIsAvailable()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("saveOrganizationVisitingAddressAndMoveToNextStep"));
        }

        [Then(@"go back to step two is available")]
        public void ThenGoBackToStepTwoIsAvailable()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("saveOrganizationVisitingAddressAndMoveToPreviousStep"));
        }

        [Then(@"save step three and quit is available")]
        public void ThenSaveStepThreeAndQuitIsAvailable()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("saveOrganizationVisitingAddressAndQuit"));
        }


        [Then(@"user moves to step four")]
        [When(@"user moves to step four")]
        public void WhenUserMovesToStepFour()
        {
            TestEnvironment.Driver.ClickElement("saveOrganizationVisitingAddressAndMoveToNextStep");
            Thread.Sleep(3000);
        }

        [Then(@"step four can be saved")]
        public void ThenStepFourCanBeSaved()
        {
            TestEnvironment.Driver.IsElementEnabled("saveOrganizationPostalAddressAndQuit");
        }

        [Then(@"go back to step three is available")]
        public void ThenGoBackToStepThreeIsAvailable()
        {
            TestEnvironment.Driver.IsElementEnabled("saveOrganizationPostalAddressAndMoveToPreviousStep");
        }

        [When(@"user goes back to step three")]
        public void WhenUserGoesBackToStepThree()
        {
            TestEnvironment.Driver.ClickElement("saveOrganizationPostalAddressAndMoveToPreviousStep");
            Thread.Sleep(2000);
        }

        [When(@"user goes back to step two")]
        public void WhenUserGoesBackToStepTwo()
        {
            TestEnvironment.Driver.ClickElement("saveOrganizationVisitingAddressAndMoveToPreviousStep");
            Thread.Sleep(2000);
        }

        [Then(@"step two can be saved")]
        public void ThenStepTwoCanBeSaved()
        {
            TestEnvironment.Driver.IsElementEnabled("saveOrganizationContactInformationAndQuit");
        }

        [When(@"user goes back to step one")]
        public void WhenUserGoesBackToStepOne()
        {
            TestEnvironment.Driver.ClickElement("saveOrganizationContactInformationAndMoveToPreviousStep");
            Thread.Sleep(2000);
        }

        [Then(@"move to the step two is available")]
        public void ThenMoveToTheStepTwoIsAvailable()
        {
            TestEnvironment.Driver.IsElementEnabled("addOrganizationFirstStepSaveAndNextButton");
        }

        [Then(@"step one can be saved")]
        public void ThenStepOneCanBeSaved()
        {
            TestEnvironment.Driver.IsElementEnabled("addOrganizationFirstStepSaveAndQuitButton");
        }

        [When(@"focus is moved to web address name")]
        public void WhenFocusIsMovedToWebAddressName()
        {
            TestEnvironment.Driver.ClickElement("webPageName");
            Thread.Sleep(2000);
        }

        [Then(@"web page type '(.*)' is selected")]
        public void ThenWebPageTypeIsSelected(string webPageType)
        {
            TestEnvironment.Driver.SelectDropDownListItem("webPageType", webPageType);
        }


        [Then(@"web address cannot be saved")]
        public void ThenWebAddressCannotBeSaved()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementEnabled("add-new-website"));
        }

        [When(@"web address '(.*)' is typed")]
        public void WhenWebAddressIsTyped(string webAddress)
        {
            TestEnvironment.Driver.TypeText("webPage", webAddress);
            
        }

        [When(@"web address name '(.*)' is typed")]
        public void WhenWebAddressNameIsTyped(string webAddressName)
        {
            TestEnvironment.Driver.TypeText("webPageName", webAddressName);
        }

        [Then(@"web address error message is shown")]
        public void ThenWebAddressErrorMessageIsShown()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("webPageErrorMessage"));
        }

        [Then(@"web address can not be saved")]
        public void ThenWebAddressCanNotBeSaved()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementEnabled("add-new-website"));
        }

        [Then(@"web address can be saved")]
        public void ThenWebAddressCanBeSaved()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("add-new-website"));
        }

        [When(@"web address is saved")]
        [Then(@"web address is saved")]
        public void WhenWebAddressIsSaved()
        {
            TestEnvironment.Driver.ClickElement("add-new-website");
            Thread.Sleep(2000);
        }

        [When(@"web address name '(.*)' is typed on the list item")]
        public void WhenWebAddressNameIsTypedOnTheListItem(string addressName)
        {
            TestEnvironment.Driver.TypeText("edited-website", addressName);
            Thread.Sleep(2000);
        }

        [Then(@"web address list should contain '(.*)'")]
        public void ThenWebAddressListShouldContain(string text)
        {
            Assert.IsTrue(TestEnvironment.Driver.ElementHasText("addedWebPages", text));
        }

        [When(@"user clicks edit on the list item '(.*)'")]
        public void WhenUserClicksEditOnTheListItem(string item)
        {
            TestEnvironment.Driver.ClickElement("edit-website");
        }

        [Then(@"web address list item can be saved")]
        public void ThenWebAddressListItemCanBeSaved()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("save-website"));
        }

        [Then(@"web address list item editing can be canceled")]
        public void ThenWebAddressListItemEditingCanBeCanceled()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("cancel-edit-website"));
        }

        [Then(@"web address list item can not be saved")]
        public void ThenWebAddressListItemCanNotBeSaved()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementEnabled("save-website"));
        }

        [When(@"web address list item editing is canceled")]
        public void WhenWebAddressListItemEditingIsCanceled()
        {
            TestEnvironment.Driver.ClickElement("cancel-edit-website");
        }

        [When(@"web address list item '(.*)' is deleted")]
        public void WhenWebAddressListItemIsDeleted(string item)
        {
            TestEnvironment.Driver.ClickElement("remove-website");
            Thread.Sleep(2000);
        }


        [Then(@"list should not contain item '(.*)'")]
        public void ThenListShouldNotContainItem(string item)
        {
            Assert.IsFalse(TestEnvironment.Driver.ElementHasText("step2", item));
        }

        [Then(@"user saves the contact information and moves to the step three of the form")]
        public void ThenUserMovesToTheStepThreeOfTheForm()
        {
            TestEnvironment.Driver.ClickElement("saveOrganizationContactInformationAndMoveToNextStep");
        }


        [Then(@"street address '(.*)' is typed")]
        public void ThenStreetAddressIsTyped(string address)
        {
            TestEnvironment.Driver.TypeText("visitingStreetAddress", address);
        }

        [Then(@"street address information cannot be saved")]
        public void ThenStreetAddressInformationCannotBeSaved()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementEnabled("saveOrganizationVisitingAddressAndMoveToNextStep"));
        }

        [When(@"postal code '(.*)' is typed")]
        public void WhenPostalCodeIsTyped(string postalCode)
        {
            TestEnvironment.Driver.TypeText("visitingAddressPostalCode", postalCode);
        }

        [When(@"postal location '(.*)' is typed")]
        public void WhenPostalLocationIsTyped(string location)
        {
            TestEnvironment.Driver.TypeText("visitingAddressPostalDistrict", location);
        }

        [Then(@"street address information can be saved")]
        public void ThenStreetAddressInformationCanBeSaved()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("saveOrganizationVisitingAddressAndMoveToNextStep"));
        }

        [Then(@"street address information is saved")]
        public void ThenStreetAddressInformationIsSaved()
        {
            TestEnvironment.Driver.ClickElement("saveOrganizationVisitingAddressAndMoveToNextStep");
        }

        [Then(@"select postal address type dropdown should be available")]
        public void ThenSelectPostalAddressTypeDropdownShouldBeAvailable()
        {
            TestEnvironment.Driver.IsElementEnabled("postalAddressType");
        }


        [Then(@"dropdown should contain '(.*)'")]
        public void ThenDropdownShouldContain(string item)
        {
            TestEnvironment.Driver.SelectDropDownListItem("postalAddressType", item);
        }

        [When(@"item '(.*)' is selected")]
        public void WhenItemIsSelected(string item)
        {
            TestEnvironment.Driver.SelectDropDownListItem("postalAddressType", item);
            TestEnvironment.Driver.ClickElement("addOrganizationPostalAddress");
            Thread.Sleep(2000);
        }

        [Then(@"add new postal box address should be available")]
        public void ThenAddNewPostalBoxAddressShouldBeAvailable()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("postboxArea"));
        }

        [Then(@"another item can be selected from dropdown")]
        public void ThenAnotherItemCanBeSelectedFromDropdown()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("postalAddressType"));
        }

        [Then(@"another item cannot be selected from dropdown")]
        public void ThenAnotherItemCannotBeSelectedFromDropdown()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementEnabled("postalAddressType"));
        }

        [When(@"postbox '(.*)' is typed")]
        public void WhenPostboxIsTyped(string poBox)
        {
            TestEnvironment.Driver.TypeText("po-box", poBox);
            Thread.Sleep(2000);
        }

        [When(@"postbox postal code '(.*)' is typed")]
        public void WhenPostboxPostalCodeIsTyped(string code)
        {
            TestEnvironment.Driver.TypeText("postcode", code);
            TestEnvironment.Driver.ClickElement("city");
            Thread.Sleep(2000);
        }
        
        [When(@"city '(.*)' is typed")]
        public void WhenCityIsTyped(string city)
        {
            TestEnvironment.Driver.TypeText("city", city);
            Thread.Sleep(2000);
        }

        [Then(@"postal address information cannot be saved")]
        public void ThenPostalAddressInformationCannotBeSaved()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementEnabled("saveOrganizationPostalAddressAndQuit"));
        }

        [Then(@"postal address information can be saved")]
        public void ThenPostalAddressInformationCanBeSaved()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("saveOrganizationPostalAddressAndQuit"));
        }

        [Then(@"postbox error message is shown")]
        public void ThenPostboxErrorMessageIsShown()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementDisplayed("po-boxErrorMessage"));
        }

        [Then(@"postal code error message is shown")]
        public void ThenPostalCodeErrorMessageIsShown()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementDisplayed("postcodeErrorMessage"));
        }


        [Then(@"postbox error message is not shown")]
        public void ThenPostboxErrorMessageIsNotShown()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementDisplayed("po-boxErrorMessage"));
        }

        [Then(@"postal code error message is not shown")]
        public void ThenPostalCodeErrorMessageIsNotShown()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementDisplayed("postcodeErrorMessage"));
        }


        [Then(@"add new separate postal address should be available")]
        public void ThenAddNewSeparatePostalAddressShouldBeAvailable()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("separatePostalAddressArea"));
        }

        [Then(@"this postal box address can be deleted")]
        public void ThenThisPostalBoxAddressCanBeDeleted()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("removePostboxPostalAddress"));
        }

        [Then(@"this separate postal address can be deleted")]
        public void ThenThisSeparatePostalAddressCanBeDeleted()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("removeSeparateStreetPostalAddress"));
        }

        [When(@"separate street address '(.*)' is typed")]
        public void WhenSeparateStreetAddressIsTyped(string address)
        {
            TestEnvironment.Driver.TypeText("separateStreetPostalAddress", address);
        }

        [When(@"separate street address postal code '(.*)' is typed")]
        public void WhenSeparateStreetAddressPostalCodeIsTyped(string postalCode)
        {
            TestEnvironment.Driver.TypeText("postalStreetAddressPostalCode", postalCode);
            TestEnvironment.Driver.ClickElement("postalStreetAddressPostalDistrict");
            Thread.Sleep(2000);
        }

        [When(@"separate postal address district '(.*)' is typed")]
        public void WhenSeparatePostalAddressDistrictIsTyped(string district)
        {
            TestEnvironment.Driver.TypeText("postalStreetAddressPostalDistrict", district);
            
        }

        [Then(@"separate street address postal code error message is not shown")]
        public void ThenSeparateStreetAddressPostalCodeErrorMessageIsNotShown()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementDisplayed("separatePostalStreetAddressPostalCodeErrorMessage"));
        }

        [Then(@"separate street address postal code error message is shown")]
        public void ThenSeparateStreetAddressPostalCodeErrorMessageIsShown()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementDisplayed("separatePostalStreetAddressPostalCodeErrorMessage"));
        }

        [Then(@"save postal address information and go back is not available")]
        public void ThenSavePostalAddressInformationAndGoBackIsNotAvailable()
        {
            Assert.IsFalse(TestEnvironment.Driver.IsElementEnabled("saveOrganizationPostalAddressAndMoveToPreviousStep"));
        }

        [Then(@"save postal address information and go back is available")]
        public void ThenSavePostalAddressInformationAndGoBackIsAvailable()
        {
            Assert.IsTrue(TestEnvironment.Driver.IsElementEnabled("saveOrganizationPostalAddressAndMoveToPreviousStep"));
        }

        [When(@"web address type '(.+)' is selected")]
        public void WhenWebAddressTypeIsSelected(string webAddressType)
        {
            TestEnvironment.Driver.SelectDropDownListItem("webPageType", webAddressType);
        }

        private static void SetOrganizationBusinessId(string businessId)
        {
            TestEnvironment.Driver.TypeText("organizationBusinessId", businessId);
        }

        private static void SetProviderType(string providerType)
        {
            TestEnvironment.Driver.SelectDropDownListItem("organizationType", providerType);
        }

        private static void SetOrganizationName(string name)
        {
            TestEnvironment.Driver.TypeText("organizationName", name);
        }
    }
}