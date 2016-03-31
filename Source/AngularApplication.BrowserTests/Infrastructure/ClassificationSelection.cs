using System.Linq;
using Affecto.Testing.UI.Selenium;
using OpenQA.Selenium;

namespace OrganizationRegister.AngularApplication.BrowserTests.Infrastructure
{
    internal class ClassificationSelection
    {
        private readonly IWebElement selectionElement;

        public ClassificationSelection(WebHostDriver driver, string selectionElementId)
        {
            selectionElement = driver.Value.FindElement(By.Id(selectionElementId));
        }

        public void Remove(string selection)
        {
            IWebElement listSelectionText = selectionElement.FindElements(By.XPath("//span[@class='ng-binding']")).Single(e => e.Text.Equals(selection));
            listSelectionText.FindElement(By.XPath("../span[@class='remove']")).Click();
        }
    }
}
