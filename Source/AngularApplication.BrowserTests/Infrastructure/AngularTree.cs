using System.Linq;
using Affecto.Testing.UI.Selenium;
using OpenQA.Selenium;

namespace OrganizationRegister.AngularApplication.BrowserTests.Infrastructure
{
    internal class AngularTree
    {
        private readonly IWebElement treeElement;

        public AngularTree(WebHostDriver driver, string treeElementId)
        {
            treeElement = driver.Value.FindElement(By.Id(treeElementId));
        }

        public void Select(string nodeText)
        {
            GetTreeNodeSpanElement(nodeText).Click();
        }

        public void Expand(string nodeText)
        {
            IWebElement nodeTextElement = GetTreeNodeSpanElement(nodeText);
            nodeTextElement.FindElement(By.XPath("../../i[@class='tree-branch-head']")).Click();
        }

        public void Collapse(string nodeText)
        {
            // Clicking the right place toggles expand / collapse
            Expand(nodeText);
        }

        private IWebElement GetTreeNodeSpanElement(string nodeText)
        {
            return treeElement.FindElements(By.XPath("//span")).Single(e => e.Text.Equals(nodeText));
        }
    }
}
