using System.Collections.Generic;
using OrganizationRegister.Common;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Infrastructure
{
    internal class LocalizedTextHelper
    {
        public static IEnumerable<LocalizedText> CreateDescriptionsCollection(TableRow texts)
        {
            return CreateTextCollection(texts, "Finnish description", "Swedish description");
        }

        public static IEnumerable<LocalizedText> CreateNamesCollection(TableRow texts)
        {
            return CreateTextCollection(texts, "Finnish name", "Swedish name");
        }

        public static IEnumerable<LocalizedText> CreateStreetAddressesCollection(TableRow texts)
        {
            return CreateTextCollection(texts, "Finnish street address", "Swedish street address");
        }

        public static IEnumerable<LocalizedText> CreatePostalDistrictsCollection(TableRow texts)
        {
            return CreateTextCollection(texts, "Finnish postal district", "Swedish postal district");
        }

        public static IEnumerable<LocalizedText> CreateAddressQualifiersCollection(TableRow texts)
        {
            return CreateTextCollection(texts, "Finnish qualifier", "Swedish qualifier");
        }

        public static IEnumerable<LocalizedText> CreateStreetAddressPostalDistrictsCollection(TableRow texts)
        {
            return CreateTextCollection(texts, "Finnish street address postal district", "Swedish street address postal district");
        }

        public static IEnumerable<LocalizedText> CreatePostOfficeBoxAddressPostalDistrictsCollection(TableRow texts)
        {
            return CreateTextCollection(texts, "Finnish post office box address postal district", "Swedish post office box address postal district");
        }

        public static IEnumerable<LocalizedText> CreateShortDescriptionsCollection(TableRow texts)
        {
            return CreateTextCollection(texts, "Finnish short description", "Swedish short description");
        }

        public static IEnumerable<LocalizedText> CreateAlternateNamesCollection(TableRow texts)
        {
            return CreateTextCollection(texts, "Finnish alternate name", "Swedish alternate name");
        }

        public static IEnumerable<LocalizedText> CreateUserInstructionsCollection(TableRow texts)
        {
            return CreateTextCollection(texts, "Finnish user instructions", "Swedish user instructions");
        }

        public static IEnumerable<LocalizedText> CreateRequirementsCollection(TableRow texts)
        {
            return CreateTextCollection(texts, "Finnish requirements", "Swedish requirements");
        }

        public static IEnumerable<LocalizedText> CreateKeywordsCollection(TableRow texts)
        {
            return CreateTextCollection(texts, "Finnish keywords", "Swedish keywords");
        }

        public static IEnumerable<LocalizedText> CreateHomepageUrlsCollection(TableRow texts)
        {
            return CreateTextCollection(texts, "homepage in finnish", "homepage in swedish");
        }

        public static IEnumerable<LocalizedText> CreateCallChargeInfosCollection(TableRow texts)
        {
            return CreateTextCollection(texts, "call charge info in finnish", "call charge info  in swedish");
        }

        public static IEnumerable<LocalizedText> CreateNameAbbreviationsCollection(TableRow texts)
        {
            return CreateTextCollection(texts, "Finnish name abbreviation", "Swedish name abbreviation");
        }




        private static IEnumerable<LocalizedText> CreateTextCollection(TableRow texts, string tableHeaderForFinnish, string tableHeaderForSwedish)
        {
            List<LocalizedText> textCollection = new List<LocalizedText>();
            if (texts.ContainsKey(tableHeaderForFinnish))
            {
                textCollection.Add(new LocalizedText("fi", texts[tableHeaderForFinnish]));
            }
            if (texts.ContainsKey(tableHeaderForSwedish))
            {
                textCollection.Add(new LocalizedText("sv", texts[tableHeaderForSwedish]));
            }
            return textCollection;
        }
    }
}