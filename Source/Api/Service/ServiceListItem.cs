using System;
using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Service
{
    public class ServiceListItem
    {
        public Guid Id { get; set; }
        public IEnumerable<LocalizedText> Names { get; set; }
        public string ServiceClasses { get; set; }
        public string OntologyTerms { get; set; }
    }
}