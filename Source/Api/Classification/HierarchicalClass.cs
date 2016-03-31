using System.Collections.Generic;

namespace OrganizationRegister.Api.Classification
{
    public class HierarchicalClass : Class
    {
        public IEnumerable<HierarchicalClass> SubClasses { get; set; }
    }
}