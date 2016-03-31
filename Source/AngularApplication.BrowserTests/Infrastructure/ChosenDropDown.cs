using System.Collections.Generic;
using System.Linq;
using Affecto.Testing.UI.Selenium;
using OpenQA.Selenium;

namespace OrganizationRegister.AngularApplication.BrowserTests.Infrastructure
{
    internal class ChosenDropDown
    {
        private readonly IWebElement chosenElement;

        public ChosenDropDown(WebHostDriver driver, string chosenControlId)
        {
            chosenElement = driver.FindElementByIdWithWait(string.Format("{0}_chosen", chosenControlId.Trim()));
        }
        public void Select(string optionText)
        {
            chosenElement.Click();
            ClickOption(optionText);
        }

        public void Clear()
        {
            // Cannot use a foreach loop because the collection changes after closing selections
            string closeLinkXPath = "//a[@class=\"search-choice-close\"]";
            IWebElement closeLink = chosenElement.FindElements(By.XPath(closeLinkXPath)).FirstOrDefault();
            while (closeLink != null)
            {
                closeLink.Click();
                closeLink = chosenElement.FindElements(By.XPath(closeLinkXPath)).FirstOrDefault();
            }
        }

        public bool IsSelected(string optionText)
        {
            IReadOnlyCollection<IWebElement> selectedOptions = chosenElement.FindElements(By.XPath("//li[@class=\"search-choice\"]/span"));
            return selectedOptions.Select(option => option.Text).Any(text => text.Equals(optionText));
        }

        private void ClickOption(string optionText)
        {
            chosenElement.FindElement(By.XPath(string.Format("./div[contains(@class,'chosen-drop')]/ul/li[.=\"{0}\"]", optionText))).Click();
        }
    }
}
