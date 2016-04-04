using System;
using System.Collections.Generic;

namespace OrganizationRegister.Tests.Infrastructure
{
    public class LanguageHelper
    {
        public static string GetLanguageNames(string languageCodeList)
        {
            List<string> languageNames = new List<string>();
            foreach (string code in languageCodeList.Split(new[] { ", " }, StringSplitOptions.None))
            {
                switch (code)
                {
                    case "fi":
                        languageNames.Add("suomi");
                        break;
                    case "sv":
                        languageNames.Add("ruotsi");
                        break;
                    case "en":
                        languageNames.Add("englanti");
                        break;
                    default:
                        throw new NotImplementedException(string.Format("No name defined for language '{0}'", code));

                }
            }
            return string.Join(", ", languageNames);
        }

        public static IEnumerable<string> GetLanguageCodes(IReadOnlyCollection<string> languageNames)
        {
            List<string> codes = new List<string>();
            foreach (string name in languageNames)
            {
                switch (name)
                {
                    case "suomi":
                        codes.Add("fi");
                        break;
                    case "ruotsi":
                        codes.Add("sv");
                        break;
                    case "englanti":
                        codes.Add("en");
                        break;
                    default:
                        throw new NotImplementedException(string.Format("No code defined for language '{0}'", name));
                }
            }
            return codes;
        }
    }
}
