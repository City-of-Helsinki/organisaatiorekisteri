using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.Testing.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.AcceptanceTests.Infrastructure;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Common;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Features.Organization
{
    [Binding]
    [Scope(Feature = "AddingOrganization")]
    internal class AddingOrganizationSteps : StepDefinition
    {
        [When(@"the following municipality is added:")]
        public void WhenTheFollowingMunicipalityIsAdded(Table municipalities)
        {
            TableRow municipality = municipalities.Rows.Single();

            Try(() => OrganizationService.AddOrganization(municipality["Business id"], municipality.GetOptionalValue("Oid"), OrganizationType.Municipality,
                municipality["Municipality code"], LocalizedTextHelper.CreateNamesCollection(municipality), LocalizedTextHelper.CreateDescriptionsCollection(municipality), 
                municipality.GetOptionalFinnishDate("Valid from"), municipality.GetOptionalFinnishDate("Valid to"), LocalizedTextHelper.CreateNameAbbreviationsCollection(municipality)));
        }

        [Then(@"there are following organizations:")]
        public void ThenThereAreFollowingOrganizations(Table expectedOrganizations)
        {
            IReadOnlyCollection<IOrganizationName> organizations = OrganizationService.GetActiveOrganizations().ToList();
            Assert.AreEqual(expectedOrganizations.RowCount, organizations.Count, "Organization count mismatch.");

            foreach (TableRow expectedOrganization in expectedOrganizations.Rows)
            {
                IOrganizationName hierarchicalOrganization = organizations.Single(o => o.Names.Any(n => n.LanguageCode.Equals("fi", StringComparison.OrdinalIgnoreCase) &&
                    n.LocalizedValue.Equals(expectedOrganization["Finnish name"])));

                Assert.AreEqual(expectedOrganization["Swedish name"], 
                    hierarchicalOrganization.Names.Single(name => name.LanguageCode.Equals("sv", StringComparison.OrdinalIgnoreCase)).LocalizedValue,
                    expectedOrganization.ToTableString());

                IOrganization organization = OrganizationService.GetOrganization(hierarchicalOrganization.Id);

                OrganizationHelper.AssertOrganizationBasicInformation(expectedOrganization, organization);
            }
        }

        [Then(@"Adding the organization fails")]
        public void ThenAddingTheOrganizationFails()
        {
            AssertCaughtException<ArgumentException>();
        }

        [When(@"the following company is added as a sub organization of '(.+)'")]
        public void WhenTheFollowingCompanyIsAddedAsASubOrganizationOf(string parentOrganizationFinnishName, Table companies)
        {
            TableRow company = companies.Rows.Single();

            IEnumerable<IOrganizationName> organizations = OrganizationService.GetActiveOrganizations();
            Guid parentOrganizationId = organizations.Single(org => org.Names.Any(name => name.LocalizedValue.Equals(parentOrganizationFinnishName))).Id;
                
            Try(() => OrganizationService.AddSubOrganization(parentOrganizationId, company["Business id"], company.GetOptionalValue("Oid"), company["Type"], null,
                LocalizedTextHelper.CreateNamesCollection(company), LocalizedTextHelper.CreateDescriptionsCollection(company), company.GetOptionalFinnishDate("Valid from"),
                company.GetOptionalFinnishDate("Valid to"), LocalizedTextHelper.CreateNameAbbreviationsCollection(company)));
        }

        [Then(@"'(.+)' is a sub organization of '(.+)'")]
        public void ThenIsASubOrganizationOf(string subOrganizationFinnishName, string parentOrganizationFinnishName)
        {
            IHierarchicalOrganization hierarchicalParent = OrganizationHelper.GetOrganization(OrganizationService.GetActiveOrganizationHierarchy().ToList(), parentOrganizationFinnishName);
            IHierarchicalOrganization hierarchicalSub = 
                hierarchicalParent.SubOrganizations.Single(org => org.Names.Any(name => name.LocalizedValue.Equals(subOrganizationFinnishName)));

            IOrganization sub = OrganizationService.GetOrganization(hierarchicalSub.Id);
            Assert.IsTrue(sub.IsSubOrganization);
        }

        [Then(@"there are following main organizations:")]
        public void ThenThereAreFollowingMainOrganizations(Table expectedOrganizations)
        {
            IReadOnlyCollection<IOrganizationName> organizations = OrganizationService.GetActiveMainOrganizations().ToList();
            Assert.AreEqual(expectedOrganizations.RowCount, organizations.Count, "Organization count mismatch.");
            foreach (TableRow expectedOrganization in expectedOrganizations.Rows)
            {
                IOrganizationName organizationName = organizations.Single(o => o.Names.Any(n => n.LanguageCode.Equals("fi", StringComparison.OrdinalIgnoreCase) &&
                    n.LocalizedValue.Equals(expectedOrganization["Finnish name"])));
                Assert.AreEqual(expectedOrganization["Swedish name"],
                    organizationName.Names.Single(name => name.LanguageCode.Equals("sv", StringComparison.OrdinalIgnoreCase)).LocalizedValue,
                    expectedOrganization.ToTableString());
            }
        }
    }
}
