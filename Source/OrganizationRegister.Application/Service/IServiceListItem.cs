using System;
using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Service
{
    public interface IServiceListItem
    {
        Guid Id { get; }
        IEnumerable<LocalizedText> Names { get; }
        string ServiceClasses { get; }
        string OntologyTerms { get; }
    }
}
