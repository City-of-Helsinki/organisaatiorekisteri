using System;
using System.Collections.Generic;

namespace OrganizationRegister.Application.Settings
{
    internal class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository repository;

        public SettingsService(ISettingsRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            this.repository = repository;
        }

        public IEnumerable<string> GetOrganizationTypes()
        {
            return repository.GetOrganizationTypeNames();
        }

        public IEnumerable<string> GetWebPageTypes()
        {
            return repository.GetWebPageTypes();
        }
    }
}
