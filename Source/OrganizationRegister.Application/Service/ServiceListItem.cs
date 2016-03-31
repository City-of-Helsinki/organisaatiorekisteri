using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Application.Localization;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Service
{
    internal class ServiceListItem : IServiceListItem
    {
        private const string ListSeparator = ", ";

        private readonly MandatoryLocalizedSingleTexts names;

        public ServiceListItem(Guid id, IEnumerable<LocalizedText> names, IEnumerable<string> serviceClassNames, IEnumerable<string> ontologyTerms)
        {
            Id = id;
            this.names = new MandatoryLocalizedSingleTexts(names);
            ServiceClasses = string.Join(ListSeparator, serviceClassNames.OrderBy(@class => @class));
            OntologyTerms = string.Join(ListSeparator, ontologyTerms.OrderBy(term => term));
        }

        public Guid Id { get; private set; }

        public IEnumerable<LocalizedText> Names
        {
            get { return names; }
        }

        public string ServiceClasses { get; private set; }
        public string OntologyTerms { get; private set; }
    }
}
