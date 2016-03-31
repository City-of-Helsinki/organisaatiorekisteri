namespace OrganizationRegister.Application.Settings
{
    public class LanguageFactory
    {
        public static ILanguage CreateLanguage(string languageCode, string languageName, int? orderNumber)
        {
            return new Language(languageCode, languageName, orderNumber);
        }
    }
}
