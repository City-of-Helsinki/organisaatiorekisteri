using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.AcceptanceTests.Infrastructure;
using OrganizationRegister.Application.Settings;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Features
{
    [Binding]
    [Scope(Feature = "Settings")]
    internal sealed class SettingsSteps : StepDefinition
    {
        private IReadOnlyCollection<string> providerTypes;
        private IReadOnlyCollection<string> webPageTypes;
        private IList<ILanguage> languages;

        [Given(@"the following organization types exist:")]
        public void GivenTheFollowingProviderTypesExist(Table dbProviderTypes)
        {
            Repository.SetProviderTypes(dbProviderTypes.Rows.Select(pt => pt["Organization type"]));
        }

        [When(@"organization types are retrieved")]
        public void WhenProviderTypesAreRetrieved()
        {
            providerTypes = SettingsService.GetOrganizationTypes().ToList();
        }

        [Then(@"the following organization types are returned")]
        public void ThenTheFollowingProviderTypesAreReturned(Table expectedProviderTypes)
        {
            Assert.AreEqual(expectedProviderTypes.RowCount, providerTypes.Count);
            foreach (TableRow providerType in expectedProviderTypes.Rows)
            {
                Assert.IsTrue(providerTypes.Contains(providerType["Organization type"]));
            }
        }

        [Given(@"the following web page types exist:")]
        public void GivenTheFollowingWebPageTypesExist(Table dbWebPageTypes)
        {
            Repository.SetWebPageTypes(dbWebPageTypes.Rows.Select(pt => pt["Web page type"]));
        }

        [When(@"web page types are retrieved")]
        public void WhenWebPageTypesAreRetrieved()
        {
            webPageTypes = SettingsService.GetWebPageTypes().ToList();
        }

        [Then(@"the following web page types are returned")]
        public void ThenTheFollowingWebPageTypesAreReturned(Table expectedWebPageTypes)
        {
            Assert.AreEqual(expectedWebPageTypes.RowCount, webPageTypes.Count);
            foreach (TableRow webPageType in expectedWebPageTypes.Rows)
            {
                Assert.IsTrue(webPageTypes.Contains(webPageType["Web page type"]));
            }
        }

        [Given(@"the languages exist:")]
        public void GivenTheLanguagesExist(Table languagesTable)
        {
            Repository.RemoveAllLanguages();
            foreach (TableRow languageRow in languagesTable.Rows)
            {
                Repository.AddLanguage(languageRow["Language code"], languageRow["Language name"]);
            }
        }

        [Given(@"the following languages can be used with services")]
        public void GivenTheFollowingLanguagesCanBeUsedWithServices(Table languagesTable)
        {
            Repository.RemoveAllServiceLanguages();
            foreach (TableRow languageRow in languagesTable.Rows)
            {
                Repository.AddServiceLanguage(languageRow["Language code"], GetOrderNumber(languageRow));
        }
        }

        [When(@"service languages are retrieved")]
        public void WhenServiceLanguagesAreRetrieved()
        {
            languages = SettingsService.GetServiceLanguages().ToList();
        }

        [Then(@"the following service languages are returned in the following order:")]
        public void ThenTheFollowingServiceLanguagesAreReturnedInTheFollowingOrder(Table expectedLanguages)
        {
            Assert.AreEqual(expectedLanguages.RowCount, languages.Count);
            for (int i = 0; i < expectedLanguages.RowCount; i++)
            {
                ILanguage language = languages[i];
                TableRow expectedLanguage = expectedLanguages.Rows[i];
                Assert.AreEqual(expectedLanguage["Language code"], language.Code);
                Assert.AreEqual(expectedLanguage["Language name"], language.Name);

            }
        }

        private static int? GetOrderNumber(TableRow languageRow)
        {
            string orderNumber = languageRow["Order number"];
            return string.IsNullOrWhiteSpace(orderNumber) ? (int?) null : int.Parse(orderNumber);
        }
    }
}
