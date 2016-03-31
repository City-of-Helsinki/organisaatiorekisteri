namespace OrganizationRegister.Application.Settings
{
    internal class Language : ILanguage
    {
        public string Code { get; protected set;  }
        public string Name { get; protected set; }
        public int? OrderNumber { get; private set; }

        public Language(string languageCode, string languageName, int? orderNumber)
        {
            Code = languageCode;
            Name = languageName;
            OrderNumber = orderNumber;
        }
    }
}
