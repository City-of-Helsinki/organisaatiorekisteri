using System;
using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Service
{
    public interface IServiceService
    {
        Guid AddService(Guid organizationId, IEnumerable<LocalizedText> names, IEnumerable<LocalizedText> alternateNames, IEnumerable<LocalizedText> descriptions, 
            IEnumerable<LocalizedText> shortDescriptions, IEnumerable<LocalizedText> userInstructions, IEnumerable<string> languageCodes, IEnumerable<LocalizedText> requirements);
        IService GetService(Guid organizationId, Guid serviceId);
        IEnumerable<IServiceListItem> GetServices(Guid organizationId);
        void SetServiceBasicInformation(Guid organizationId, Guid serviceId, IEnumerable<LocalizedText> names, IEnumerable<LocalizedText> alternateNames, 
            IEnumerable<LocalizedText> descriptions, IEnumerable<LocalizedText> shortDescriptions, IEnumerable<LocalizedText> userInstructions, IEnumerable<string> languageCodes,
            IEnumerable<LocalizedText> requirements);
        void RemoveService(Guid organizationId, Guid serviceId);
    }
}