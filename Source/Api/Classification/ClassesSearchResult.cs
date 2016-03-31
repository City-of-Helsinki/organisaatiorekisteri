using System.Collections.Generic;

namespace OrganizationRegister.Api.Classification
{
    public class ClassesSearchResult
    {
        public IEnumerable<Class> Classes { get; set; }
        public string SearchText { get; set; }
    }
}