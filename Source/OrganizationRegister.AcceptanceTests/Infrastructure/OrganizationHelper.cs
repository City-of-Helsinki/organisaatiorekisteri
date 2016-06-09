using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Affecto.Testing.SpecFlow;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Application.Organization;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Infrastructure
{
    internal class OrganizationHelper
    {
        public static void AssertOrganizationBasicInformation(TableRow expectedBasicInfo, IOrganization organization)
        {
            Assert.AreEqual(expectedBasicInfo["Finnish name"],
                organization.Names.Single(name => name.LanguageCode.Equals("fi", StringComparison.OrdinalIgnoreCase)).LocalizedValue, expectedBasicInfo.ToTableString());
            if (expectedBasicInfo.ContainsKey("Swedish name"))
            {
                Assert.AreEqual(expectedBasicInfo["Swedish name"],
                    organization.Names.Single(name => name.LanguageCode.Equals("sv", StringComparison.OrdinalIgnoreCase)).LocalizedValue,
                    expectedBasicInfo.ToTableString());
            }
            AssertOptionalValue(expectedBasicInfo.GetOptionalValue("Business id"), organization.BusinessId);
            AssertOptionalValue(expectedBasicInfo.GetOptionalValue("Oid"), organization.Oid);
            AssertOptionalValue(expectedBasicInfo.GetOptionalValue("Municipality code"), organization.MunicipalityCode);
            AssertOptionalValue(expectedBasicInfo.GetOptionalValue("Valid from"), organization.ValidFrom);
            AssertOptionalValue(expectedBasicInfo.GetOptionalValue("Valid to"), organization.ValidTo);
            Assert.AreEqual(expectedBasicInfo["Type"], organization.Type, expectedBasicInfo.ToTableString());
            if (expectedBasicInfo.ContainsKey("Finnish description"))
            {
                Assert.AreEqual(expectedBasicInfo["Finnish description"],
                    organization.Descriptions.Single(desc => desc.LanguageCode.Equals("fi", StringComparison.OrdinalIgnoreCase)).LocalizedValue,
                    expectedBasicInfo.ToTableString());
            }
            if (expectedBasicInfo.ContainsKey("Swedish description"))
            {
                Assert.AreEqual(expectedBasicInfo["Swedish description"],
                    organization.Descriptions.Single(desc => desc.LanguageCode.Equals("sv", StringComparison.OrdinalIgnoreCase)).LocalizedValue,
                    expectedBasicInfo.ToTableString());
            }
        }

        public static IHierarchicalOrganization GetOrganization(IReadOnlyCollection<IHierarchicalOrganization> organizations, string organizationName)
        {
            if (organizations.Any(org => org.Names.Any(name => name.LocalizedValue.Equals(organizationName))))
            {
                return organizations.Single(org => org.Names.Any(name => name.LocalizedValue.Equals(organizationName)));
            }
            IReadOnlyCollection<IHierarchicalOrganization> subOrganizations = organizations.SelectMany(org => org.SubOrganizations).ToList();
            if (subOrganizations.Any())
            {
                return GetOrganization(subOrganizations, organizationName);
            }
            throw new ArgumentException(string.Format("Organization '{0}' not found.", organizationName));
        }

        public static Guid GetOrganizationId(string organizationName)
        {
            IContainer container = ScenarioContext.Current.Get<IContainer>();
            IOrganizationService organizationService = container.Resolve<IOrganizationService>();
            return organizationService.GetActiveOrganizations().Single(o => o.Names.Any(name => name.LocalizedValue.Equals(organizationName))).Id;
        }

        private static void AssertOptionalValue(string expectedValue, DateTime? value)
        {
            if (string.IsNullOrWhiteSpace(expectedValue))
            {
                Assert.IsFalse(value.HasValue);
            }
            else
            {
                Assert.AreEqual(DateTime.ParseExact(expectedValue, "dd.MM.yyyy", CultureInfo.InvariantCulture), value.Value);
            }
        }

        private static void AssertOptionalValue(string expectedValue, string value)
        {
            if (string.IsNullOrWhiteSpace(expectedValue))
            {
                Assert.IsTrue(string.IsNullOrWhiteSpace(value));
            }
            else
            {
                Assert.AreEqual(expectedValue, value);                
            }
        }
    }
}
