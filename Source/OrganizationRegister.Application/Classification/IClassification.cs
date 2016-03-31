using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Classification
{
    public interface IClassification
    {
        IEnumerable<IHierarchicalClass> ServiceClasses { get; }
        IEnumerable<IClass> OntologyTerms { get; }
        IEnumerable<IHierarchicalClass> TargetGroups { get; }
        IEnumerable<IHierarchicalClass> LifeEvents { get; }
        IEnumerable<LocalizedText> Keywords { get; }
    }
}
