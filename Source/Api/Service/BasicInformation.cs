using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Service
{
    public class BasicInformation
    {
        public IEnumerable<LocalizedText> Names { get; set; }
        public IEnumerable<LocalizedText> AlternateNames { get; set; }
        public IEnumerable<LocalizedText> Descriptions { get; set; }
        public IEnumerable<LocalizedText> ShortDescriptions { get; set; }
        public IEnumerable<LocalizedText> UserInstructions { get; set; }
        public IEnumerable<string> LanguageCodes { get; set; }
        public IEnumerable<LocalizedText> Requirements { get; set; }
    }
}