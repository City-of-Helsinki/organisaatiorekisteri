using System;
using System.Collections.Generic;
using OrganizationRegister.Application.Localization;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Organization
{
    internal class OrganizationName : IOrganizationName
    {
        //protected readonly MandatoryLocalizedSingleTexts names;

        protected readonly LocalizedSingleTexts names;

        public OrganizationName(Guid id, IEnumerable<LocalizedText> names)
        {
            this.names = new MandatoryLocalizedSingleTexts(names);

        
            Id = id;
        }

        public Guid Id { get; private set; }

        public LocalizedSingleTexts Names
        {
            get { return names; }
        }
    }
}
