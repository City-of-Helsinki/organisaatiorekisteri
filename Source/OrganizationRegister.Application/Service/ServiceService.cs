using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Application.Classification;
using OrganizationRegister.Application.Settings;
using OrganizationRegister.Common;
using Language = OrganizationRegister.Common.Language;

namespace OrganizationRegister.Application.Service
{
    internal class ServiceService : IServiceService
    {
        private readonly IServiceRepository serviceRepository;
        private readonly ISettingsRepository settingsRepository;
        private readonly IClassificationRepository classificationRepository;

        public ServiceService(IServiceRepository serviceRepository, ISettingsRepository settingsRepository, IClassificationRepository classificationRepository)
        {
            if (serviceRepository == null)
            {
                throw new ArgumentNullException("serviceRepository");
            }
            if (settingsRepository == null)
            {
                throw new ArgumentNullException("settingsRepository");
            }

            this.serviceRepository = serviceRepository;
            this.settingsRepository = settingsRepository;
            this.classificationRepository = classificationRepository;
        }

        public Guid AddService(Guid organizationId, IEnumerable<LocalizedText> names, IEnumerable<LocalizedText> alternateNames, IEnumerable<LocalizedText> descriptions, 
            IEnumerable<LocalizedText> shortDescriptions, IEnumerable<LocalizedText> userInstructions, IEnumerable<string> languageCodes, IEnumerable<LocalizedText> requirements)
        {
            IReadOnlyCollection<string> dataLanguageCodes = settingsRepository.GetDataLanguageCodes();
            IEnumerable<Language> serviceLanguages = GetServiceLanguages(languageCodes);
            var id = Guid.NewGuid();
            var service = new Service(id, names, descriptions, shortDescriptions, serviceLanguages, dataLanguageCodes)
            {
                AlternateNames = alternateNames,
                UserInstructions = userInstructions,
                Requirements = requirements
            };
            serviceRepository.AddService(organizationId, service);
            serviceRepository.SaveChanges();
            return id;
        }

        public IService GetService(Guid organizationId, Guid serviceId)
        {
            return serviceRepository.GetService(organizationId, serviceId);
        }

        public IEnumerable<IServiceListItem> GetServices(Guid organizationId)
        {
            return serviceRepository.GetServices(organizationId);
        }

        public void SetServiceBasicInformation(Guid organizationId, Guid serviceId, IEnumerable<LocalizedText> names, IEnumerable<LocalizedText> alternateNames, 
            IEnumerable<LocalizedText> descriptions, IEnumerable<LocalizedText> shortDescriptions, IEnumerable<LocalizedText> userInstructions, IEnumerable<string> languageCodes,
            IEnumerable<LocalizedText> requirements)
        {
            Service service = GetService(organizationId, serviceId) as Service;
            service.Names = names;
            service.AlternateNames = alternateNames;
            service.Descriptions = descriptions;
            service.ShortDescriptions = shortDescriptions;
            service.UserInstructions = userInstructions;
            service.Languages = GetServiceLanguages(languageCodes);
            service.Requirements = requirements;
            serviceRepository.UpdateServiceBasicInformation(organizationId, serviceId, service);
            serviceRepository.SaveChanges();
        }

        public void RemoveService(Guid organizationId, Guid serviceId)
        {
            serviceRepository.RemoveService(organizationId, serviceId);
            serviceRepository.SaveChanges();
        }

        private IEnumerable<Language> GetServiceLanguages(IEnumerable<string> languageCodes)
        {
            IReadOnlyCollection<ILanguage> serviceLanguages = settingsRepository.GetServiceLanguages();
            return languageCodes.Select(code => new Language(code, serviceLanguages.Single(l => l.Code.Equals(code, StringComparison.OrdinalIgnoreCase)).Name));
        }
    }
}
