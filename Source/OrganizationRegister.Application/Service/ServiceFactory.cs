using System;
using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Service
{
    public class ServiceFactory
    {
        public static IServiceListItem CreateServiceListItem(Guid id, IEnumerable<LocalizedText> names, IEnumerable<string> serviceClasses, IEnumerable<string> ontologyTerms)
        {
            return new ServiceListItem(id, names, serviceClasses, ontologyTerms);
        }

        public static IService CreateService(Dto.Service service, IEnumerable<string> dataLanguageCodes)
        {
            return new Service(service.Id, service.NumericId, service.Names, service.Descriptions, service.ShortDescriptions, service.Languages, dataLanguageCodes)
            {
                AlternateNames = service.AlternateNames,
                UserInstructions = service.UserInstructions,
                Requirements = service.Requirements,
                ServiceClasses = service.ServiceClasses,
                TargetGroups = service.TargetGroups,
                LifeEvents = service.LifeEvents,
                OntologyTerms = service.OntologyTerms,
                Keywords = service.Keywords
            };
        }
    }
}
