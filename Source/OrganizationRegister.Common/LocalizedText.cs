namespace OrganizationRegister.Common
{
    public class LocalizedText
    {
        public LocalizedText()
        {
        }

        public LocalizedText(string languageCode, string localizedValue)
        {
            LanguageCode = languageCode;
            LocalizedValue = localizedValue;
        }

        public string LanguageCode { get; set; }
        public string LocalizedValue { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is LocalizedText)
            {
                return GetHashCode().Equals(obj.GetHashCode());
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((LanguageCode != null ? LanguageCode.GetHashCode() : 0) * 397) ^ (LocalizedValue != null ? LocalizedValue.GetHashCode() : 0);
            }
        }
    }
}