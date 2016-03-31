using System;
using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Service
{
    public class Classification
    {
        public IEnumerable<Guid> ServiceClasses { get; set; }
        public IEnumerable<Guid> OntologyTerms { get; set; }
        public IEnumerable<Guid> TargetGroups { get; set; }
        public IEnumerable<Guid> LifeEvents { get; set; }
        public IEnumerable<LocalizedText> Keywords { get; set; }
    }
}