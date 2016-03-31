using System;
using System.Linq;
using Affecto.Testing.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Application.Service;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Infrastructure
{
    internal class ServiceHelper
    {
        public static void AssertServiceBasicInformation(TableRow expectedBasicInfo, IService service)
        {
            AssertNames(expectedBasicInfo, service);
            AssertDescriptions(expectedBasicInfo, service);
            AssertShortDescriptions(expectedBasicInfo, service);
            AssertLanguages(expectedBasicInfo, service);
            AssertAlternateNames(expectedBasicInfo, service);
            AssertUserInstructions(expectedBasicInfo, service);
            AssertRequirements(expectedBasicInfo, service);
        }

        private static void AssertRequirements(TableRow expectedBasicInfo, IService service)
        {
            if (expectedBasicInfo.ContainsKey("Finnish requirements"))
            {
                Assert.AreEqual(expectedBasicInfo["Finnish requirements"],
                    service.Requirements.Single(requirement => requirement.LanguageCode.Equals("fi", StringComparison.OrdinalIgnoreCase)).LocalizedValue,
                    expectedBasicInfo.ToTableString());
            }
            if (expectedBasicInfo.ContainsKey("Swedish requirements"))
            {
                Assert.AreEqual(expectedBasicInfo["Swedish requirements"],
                    service.Requirements.Single(requirement => requirement.LanguageCode.Equals("sv", StringComparison.OrdinalIgnoreCase)).LocalizedValue,
                    expectedBasicInfo.ToTableString());
            }
        }

        private static void AssertUserInstructions(TableRow expectedBasicInfo, IService service)
        {
            if (expectedBasicInfo.ContainsKey("Finnish user instructions"))
            {
                Assert.AreEqual(expectedBasicInfo["Finnish user instructions"],
                    service.UserInstructions.Single(instruction => instruction.LanguageCode.Equals("fi", StringComparison.OrdinalIgnoreCase)).LocalizedValue,
                    expectedBasicInfo.ToTableString());
            }
            if (expectedBasicInfo.ContainsKey("Swedish user instructions"))
            {
                Assert.AreEqual(expectedBasicInfo["Swedish user instructions"],
                    service.UserInstructions.Single(instruction => instruction.LanguageCode.Equals("sv", StringComparison.OrdinalIgnoreCase)).LocalizedValue,
                    expectedBasicInfo.ToTableString());
            }
        }

        private static void AssertAlternateNames(TableRow expectedBasicInfo, IService service)
        {
            if (expectedBasicInfo.ContainsKey("Finnish alternate name"))
            {
                Assert.AreEqual(expectedBasicInfo["Finnish alternate name"],
                    service.AlternateNames.Single(name => name.LanguageCode.Equals("fi", StringComparison.OrdinalIgnoreCase)).LocalizedValue,
                    expectedBasicInfo.ToTableString());
            }
            if (expectedBasicInfo.ContainsKey("Swedish alternate name"))
            {
                Assert.AreEqual(expectedBasicInfo["Swedish alternate name"],
                    service.AlternateNames.Single(name => name.LanguageCode.Equals("sv", StringComparison.OrdinalIgnoreCase)).LocalizedValue,
                    expectedBasicInfo.ToTableString());
            }
        }

        private static void AssertLanguages(TableRow expectedBasicInfo, IService service)
        {
            string[] expectedLanguages = expectedBasicInfo.SplitCommaSeparatedText("Languages").ToArray();
            string[] actualLanguages = service.Languages.Select(l => l.Code).ToArray();
            Assert.AreEqual(expectedLanguages.Length, actualLanguages.Length);
            for (int i = 0; i < expectedLanguages.Length; i++)
            {
                Assert.AreEqual(expectedLanguages[i], actualLanguages[i]);
            }
        }

        private static void AssertShortDescriptions(TableRow expectedBasicInfo, IService service)
        {
            Assert.AreEqual(expectedBasicInfo["Finnish short description"], service.ShortDescriptions
                .Single(description => description.LanguageCode.Equals("fi", StringComparison.OrdinalIgnoreCase)).LocalizedValue, expectedBasicInfo.ToTableString());
            if (expectedBasicInfo.ContainsKey("Swedish short description"))
            {
                Assert.AreEqual(expectedBasicInfo["Swedish short description"],
                    service.ShortDescriptions.Single(description => description.LanguageCode.Equals("sv", StringComparison.OrdinalIgnoreCase)).LocalizedValue,
                    expectedBasicInfo.ToTableString());
            }
        }

        private static void AssertDescriptions(TableRow expectedBasicInfo, IService service)
        {
            Assert.AreEqual(expectedBasicInfo["Finnish description"], service.Descriptions
                .Single(description => description.LanguageCode.Equals("fi", StringComparison.OrdinalIgnoreCase)).LocalizedValue, expectedBasicInfo.ToTableString());
            if (expectedBasicInfo.ContainsKey("Swedish description"))
            {
                Assert.AreEqual(expectedBasicInfo["Swedish description"],
                    service.Descriptions.Single(description => description.LanguageCode.Equals("sv", StringComparison.OrdinalIgnoreCase)).LocalizedValue,
                    expectedBasicInfo.ToTableString());
            }
        }

        private static void AssertNames(TableRow expectedBasicInfo, IService service)
        {
            Assert.AreEqual(expectedBasicInfo["Finnish name"],
                service.Names.Single(name => name.LanguageCode.Equals("fi", StringComparison.OrdinalIgnoreCase)).LocalizedValue,
                expectedBasicInfo.ToTableString());
            if (expectedBasicInfo.ContainsKey("Swedish name"))
            {
                Assert.AreEqual(expectedBasicInfo["Swedish name"],
                    service.Names.Single(name => name.LanguageCode.Equals("sv", StringComparison.OrdinalIgnoreCase)).LocalizedValue,
                    expectedBasicInfo.ToTableString());
            }
        }
    }
}
