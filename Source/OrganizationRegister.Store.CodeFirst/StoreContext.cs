using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Affecto.EntityFramework.PostgreSql;
using OrganizationRegister.Store.CodeFirst.Model;
using OrganizationRegister.Store.CodeFirst.Model.Configuration;
using OrganizationRegister.Store.CodeFirst.Querying;

namespace OrganizationRegister.Store.CodeFirst
{
    internal class StoreContext : PostgreSqlDbContext, IStoreContext
    {
        public const string ConfigurationKey = "OrganizationRegisterContext";

        public StoreContext(string schemaName)
            : base(schemaName, true, ConfigurationKey)
        {
            if (schemaName == null)
            {
                throw new ArgumentNullException("schemaName");
            }
            Configure();
        }

        public StoreContext()
            : this("")
        {
        }

        protected StoreContext(DbConnection connection)
            : base("", true, connection, true)
        {
            Configure();
        }

        public IDbSet<Language> Languages { get; set; }
        public IDbSet<OrganizationType> OrganizationTypes { get; set; }
        public IDbSet<WebPageType> WebPageTypes { get; set; }
        public IDbSet<Organization> Organizations { get; set; }
        public IDbSet<Address> Addresses { get; set; }
        public IDbSet<PhoneNumber> PhoneNumbers { get; set; }
        public IDbSet<EmailAddress> EmailAddresses { get; set; }
        public IDbSet<WebPage> WebPages { get; set; }
        public IDbSet<AvailableDataLanguage> DataLanguages { get; set; }
        public IDbSet<CallChargeType> CallChargeTypes { get; set; }


        public OrganizationType GetOrganizationType(string type)
        {
            var query = new OrganizationTypeQuery(OrganizationTypes);
            return query.Execute(type);
        }

        public WebPageType GetWebPageType(string type)
        {
            var query = new WebPageTypeQuery(WebPageTypes);
            return query.Execute(type);
        }

        public WebPageType GetWebPageType(Guid guid)
        {
            var query = new WebPageTypeQuery(WebPageTypes);
            return query.Execute(guid);
        }


        public CallChargeType GetCallChargeType(Guid guid)
        {
            var query = new CallChargeTypeQuery(CallChargeTypes);
            return query.Execute(guid);
        }

        public CallChargeType GetCallChargeType(string type)
        {
            var query = new CallChargeTypeQuery(CallChargeTypes);
            return query.Execute(type);
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public AvailableDataLanguage GetDataLanguage(string languageCode)
        {
            var languageQuery = new LanguageQuery(DataLanguages);
            return languageQuery.Execute(languageCode) as AvailableDataLanguage;
        }

        public List<AvailableDataLanguage> GetDataLanguages()
        {
            return DataLanguages as List<AvailableDataLanguage>;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new AddressConfiguration());
            modelBuilder.Configurations.Add(new AddressLanguageSpecificationConfiguration());
            modelBuilder.Configurations.Add(new EmailAddressConfiguration());
            modelBuilder.Configurations.Add(new LanguageConfiguration());
            modelBuilder.Configurations.Add(new OrganizationConfiguration());
            modelBuilder.Configurations.Add(new OrganizationLanguageSpecificationConfiguration());
            modelBuilder.Configurations.Add(new PhoneNumberConfiguration());
            modelBuilder.Configurations.Add(new PhoneNumberLanguageSpecificationConfiguration());
            modelBuilder.Configurations.Add(new OrganizationTypeConfiguration());
            modelBuilder.Configurations.Add(new WebPageTypeConfiguration());
            modelBuilder.Configurations.Add(new WebPageConfiguration());
            modelBuilder.Configurations.Add(new AvailableDataLanguageConfiguration());
            modelBuilder.Configurations.Add(new CallChargeTypeConfiguration());
        }

        private void Configure()
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }
    }
}