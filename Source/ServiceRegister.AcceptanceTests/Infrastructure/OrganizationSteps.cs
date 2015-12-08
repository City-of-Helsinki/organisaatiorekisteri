﻿using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.Testing.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceRegister.Application.Organization;
using ServiceRegister.Common;
using ServiceRegister.Tests.Infrastructure;
using TechTalk.SpecFlow;

namespace ServiceRegister.AcceptanceTests.Infrastructure
{
    [Binding]
    internal sealed class OrganizationSteps : StepDefinition
    {
        private const string LanguageCode = "fi";

        [Given(@"the following company is added:")]
        [When(@"the following company is added:")]
        public void WhenTheFollowingCompanyIsAdded(Table companies)
        {
            TableRow company = companies.Rows.Single();
            Try(() => OrganizationService.AddOrganization(company.GetOptionalValue("Business id"), company.GetOptionalValue("Oid"), company["Type"], null,
                LocalizedTextHelper.CreateNamesCollection(company), LocalizedTextHelper.CreateDescriptionsCollection(company)));
        }

        [Given(@"there is an organization")]
        public void GivenThereIsAnOrganization()
        {
            Guid organizationId = OrganizationService.AddOrganization("1234567-1", "123", "Yritys", null, 
                new List<LocalizedText> { new LocalizedText("fi", "Firma"), new LocalizedText("sv", "Bolaget") }, Enumerable.Empty<LocalizedText>());
            CurrentScenarioContext.OrganizationId = organizationId;
        }

        [Given(@"there is an organization '(.+)' with business id '(.+)'")]
        public void GivenThereIsAnOrganizationWithBusinessId(string organizationName, string businessId)
        {
            OrganizationService.AddOrganization(businessId, "", "Yritys", null, new List<LocalizedText> { new LocalizedText(LanguageCode, organizationName) }, null);
        }

        [Then(@"there are following organizations:")]
        public void ThenThereAreFollowingOrganizations(Table expectedOrganizations)
        {
            IReadOnlyCollection<IHierarchicalOrganization> organizations = OrganizationService.GetActiveOrganizationHierarchy().ToList();
            Assert.AreEqual(expectedOrganizations.RowCount, organizations.Count);
            foreach (TableRow expectedOrganization in expectedOrganizations.Rows)
            {
                Assert.IsTrue(organizations.Any(o => o.Names.Single().LocalizedValue.Equals(expectedOrganization["Name"])));
            }
        }

        [Given(@"there is an organization '(.+)' which is a sub organization of '(.+)'")]
        public void GivenThereIsAnOrganizationWhichIsASubOrganizationOf(string subOrganizationFinnishName, string organizationFinnishName)
        {
            Guid parentId = OrganizationHelper.GetOrganizationId(organizationFinnishName);
            IOrganization parent = OrganizationService.GetOrganization(parentId);
            OrganizationService.AddSubOrganization(parent.Id, parent.BusinessId, parent.Oid, parent.Type, parent.MunicipalityCode, 
                new List<LocalizedText> { new LocalizedText(LanguageCode, subOrganizationFinnishName) }, null);
        }

        [Then(@"there are no organizations")]
        public void ThenThereAreNoOrganizations()
        {
            Assert.IsFalse(OrganizationService.GetActiveOrganizationHierarchy().Any());
        }
    }
}