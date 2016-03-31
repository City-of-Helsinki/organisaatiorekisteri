using System;
using System.Collections.Generic;
using Affecto.Patterns.Cqrs;
using OrganizationRegister.Common;

namespace OrganizationRegister.Commanding.Service.Commands
{
    public class SetServiceClassification : ICommand
    {
        public Guid OrganizationId { get; private set; }
        public Guid ServiceId { get; private set; }
        public IEnumerable<Guid> ServiceClasses { get; private set; }
        public IEnumerable<Guid> OntologyTerms { get; private set; }
        public IEnumerable<Guid> TargetGroups { get; private set; }
        public IEnumerable<Guid> LifeEvents { get; private set; }
        public IEnumerable<LocalizedText> Keywords { get; private set; }

        public SetServiceClassification(Guid organizationId, Guid serviceId, IEnumerable<Guid> serviceClasses, IEnumerable<Guid> ontologyTerms, IEnumerable<Guid> targetGroups, 
            IEnumerable<Guid> lifeEvents, IEnumerable<LocalizedText> keywords)
        {
            OrganizationId = organizationId;
            ServiceId = serviceId;
            ServiceClasses = serviceClasses;
            OntologyTerms = ontologyTerms;
            TargetGroups = targetGroups;
            LifeEvents = lifeEvents;
            Keywords = keywords;
        }
    }
}