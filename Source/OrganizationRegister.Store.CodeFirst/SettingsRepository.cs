using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Application.Settings;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst
{
    internal class SettingsRepository : ISettingsRepository
    {
        private readonly IStoreContext context;

        public SettingsRepository(IStoreContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this.context = context;
        }

        public IReadOnlyCollection<string> GetOrganizationTypeNames()
        {
            return context.OrganizationTypes.OrderBy(pt => pt.OrderNumber).Select(pt => pt.Name).ToList();
        }

        public IReadOnlyCollection<string> GetDataLanguageCodes()
        {
            return context.DataLanguages.Select(language => language.Language.Code).ToList();
        }

        public IReadOnlyCollection<string> GetWebPageTypes()
        {
            return context.WebPageTypes.Select(t => t.Type).ToList();
        }

        public IReadOnlyCollection<AvailableDataLanguage> GetDataLanguages()
        {
            return context.DataLanguages.ToList();
        }

    }
}