using System;
using System.Collections.Generic;
using OrganizationRegister.Api.Classification;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Service
{
    public class Service
    {
        public Guid Id { get; set; }
        public long NumericId { get; set; }
        public IEnumerable<LocalizedText> Names { get; set; }
        public IEnumerable<LocalizedText> AlternateNames { get; set; }
        public IEnumerable<LocalizedText> Descriptions { get; set; }
        public IEnumerable<LocalizedText> ShortDescriptions { get; set; }
        public IEnumerable<LocalizedText> UserInstructions { get; set; }
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<LocalizedText> Requirements { get; set; }
        public IEnumerable<HierarchicalClass> ServiceClasses { get; set; }
        public IEnumerable<Class> OntologyTerms { get; set; }
        public IEnumerable<HierarchicalClass> TargetGroups { get; set; }
        public IEnumerable<HierarchicalClass> LifeEvents { get; set; }
        public IEnumerable<LocalizedText> Keywords { get; set; }
    }
}