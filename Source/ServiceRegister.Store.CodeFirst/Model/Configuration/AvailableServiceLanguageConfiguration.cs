﻿using System.Data.Entity.ModelConfiguration;

namespace ServiceRegister.Store.CodeFirst.Model.Configuration
{
    internal class AvailableServiceLanguageConfiguration : EntityTypeConfiguration<AvailableServiceLanguage>
    {
        public AvailableServiceLanguageConfiguration()
        {
            HasKey(serviceLanguage => serviceLanguage.LanguageId);
            HasRequired(serviceLanguage => serviceLanguage.Language).WithOptional();
        }
    }
}
