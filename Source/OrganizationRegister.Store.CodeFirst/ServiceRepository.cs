using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Application.Service;
using OrganizationRegister.Application.Settings;
using OrganizationRegister.Store.CodeFirst.Model;
using OrganizationRegister.Store.CodeFirst.Querying;

namespace OrganizationRegister.Store.CodeFirst
{
    internal class ServiceRepository : IServiceRepository
    {
        private readonly IStoreContext context;
        private readonly ISettingsRepository settingsRepository;

        public ServiceRepository(IStoreContext context, ISettingsRepository settingsRepository)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (settingsRepository == null)
            {
                throw new ArgumentNullException("settingsRepository");
            }

            this.context = context;
            this.settingsRepository = settingsRepository;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void AddService(Guid organizationId, IService service)
        {
            Service dbService = new Service { Id = service.Id, OrganizationId = organizationId };
            dbService.SetBasicInformation(service, context);
            context.Services.Add(dbService);
        }

        public IService GetService(Guid organizationId, Guid serviceId)
        {
            Service dbService = GetDbService(organizationId, serviceId);
            return ServiceFactory.CreateService(dbService.ToDto(), settingsRepository.GetDataLanguageCodes());
        }

        public IEnumerable<IServiceListItem> GetServices(Guid organizationId)
        {
            var query = new OrganizationServicesQuery(context.Services);
            IReadOnlyCollection<Service> dbServices = query.Execute(organizationId).ToList();
            List<IServiceListItem> services = new List<IServiceListItem>();
            foreach (Service dbService in dbServices)
            {
                services.Add(ServiceFactory.CreateServiceListItem(dbService.Id, dbService.GetNames(), dbService.GetServiceClassNames(), dbService.GetOntologyTermNames()));
            }
            return services;
        }

        public void UpdateServiceBasicInformation(Guid organizationId, Guid serviceId, IBasicInformation information)
        {
            Service dbService = GetDbService(organizationId, serviceId);
            dbService.SetBasicInformation(information, context);
        }

        
        public void RemoveService(Guid organizationId, Guid serviceId)
        {
            Service dbService = GetDbService(organizationId, serviceId);
            dbService.RemoveAllLanguageSpecificData();
            dbService.RemoveAllLanguages();
            dbService.RemoveAllClassificationData(context);
            context.Services.Remove(dbService);
        }

        private Service GetDbService(Guid organizationId, Guid serviceId)
        {
            var serviceQuery = new ServiceQuery(context.Services);
            return serviceQuery.Execute(organizationId, serviceId);
        }
    }
}
