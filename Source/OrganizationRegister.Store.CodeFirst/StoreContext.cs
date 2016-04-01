using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Affecto.EntityFramework.PostgreSql;
using OrganizationRegister.Store.CodeFirst.Model;
using OrganizationRegister.Store.CodeFirst.Model.Configuration;
using OrganizationRegister.Store.CodeFirst.Querying;

namespace OrganizationRegister.Store.CodeFirst
{
    internal class StoreContext : DbContext, IStoreContext
    {
        public const string ConfigurationKey = "ServiceRegisterContext";

        private readonly string schemaName;

        public StoreContext(string schemaName)
            : base(ConfigurationKey)
        {
            if (schemaName == null)
            {
                throw new ArgumentNullException("schemaName");
            }

            this.schemaName = schemaName;

            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        public StoreContext()
            : this("")
        {
        }

        protected StoreContext(DbConnection connection)
            : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        public IDbSet<Language> Languages { get; set; }
        public IDbSet<OrganizationType> OrganizationTypes { get; set; }
        public IDbSet<WebPageType> WebPageTypes { get; set; }
        public IDbSet<Organization> Organizations { get; set; }
        public IDbSet<Service> Services { get; set; }
        public IDbSet<Address> Addresses { get; set; }
        public IDbSet<PhoneNumber> PhoneNumbers { get; set; }
        public IDbSet<EmailAddress> EmailAddresses { get; set; }
        public IDbSet<WebPage> WebPages { get; set; }
        public IDbSet<LifeEvent> LifeEvents { get; set; }
        public IDbSet<ServiceClass> ServiceClasses { get; set; }
        public IDbSet<OntologyTerm> OntologyTerms { get; set; }
        public IDbSet<TargetGroup> TargetGroups { get; set; }
        public IDbSet<AvailableServiceLanguage> ServiceLanguages { get; set; }
        public IDbSet<AvailableDataLanguage> DataLanguages { get; set; }
        public IDbSet<ServiceServiceClass> ServiceServiceClasses { get; set; }
        public IDbSet<ServiceLifeEvent> ServiceLifeEvents { get; set; }
        public IDbSet<ServiceOntologyTerm> ServiceOntologyTerms { get; set; }
        public IDbSet<ServiceKeyword> ServiceKeywords { get; set; }
        public IDbSet<ServiceTargetGroup> ServiceTargetGroups { get; set; }

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

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public AvailableDataLanguage GetDataLanguage(string languageCode)
        {
            var languageQuery = new LanguageQuery(DataLanguages);
            return languageQuery.Execute(languageCode) as AvailableDataLanguage;
        }

        public AvailableServiceLanguage GetServiceLanguage(string languageCode)
        {
            var languageQuery = new LanguageQuery(ServiceLanguages);
            return languageQuery.Execute(languageCode) as AvailableServiceLanguage;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schemaName);
            modelBuilder.Conventions.Add(new LowerCasePropertyNameConvention());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            
            modelBuilder.Types().Configure(configuration =>
            {
                string name = configuration.ClrType.Name.ToLower();
                configuration.ToTable(name);
            });

            modelBuilder.Properties().Configure(configuration =>
            {
                string name = configuration.ClrPropertyInfo.Name.ToLower();
                configuration.HasColumnName(name);
            });

            modelBuilder.Configurations.Add(new AddressConfiguration());
            modelBuilder.Configurations.Add(new AddressLanguageSpecificationConfiguration());
            modelBuilder.Configurations.Add(new EmailAddressConfiguration());
            modelBuilder.Configurations.Add(new LanguageConfiguration());
            modelBuilder.Configurations.Add(new OrganizationConfiguration());
            modelBuilder.Configurations.Add(new OrganizationLanguageSpecificationConfiguration());
            modelBuilder.Configurations.Add(new PhoneNumberConfiguration());
            modelBuilder.Configurations.Add(new OrganizationTypeConfiguration());
            modelBuilder.Configurations.Add(new WebPageTypeConfiguration());
            modelBuilder.Configurations.Add(new WebPageConfiguration());
            modelBuilder.Configurations.Add(new AvailableServiceLanguageConfiguration());
            modelBuilder.Configurations.Add(new AvailableDataLanguageConfiguration());
            modelBuilder.Configurations.Add(new ServiceLanguageConfiguration());
            modelBuilder.Configurations.Add(new ServiceConfiguration());
            modelBuilder.Configurations.Add(new ServiceLanguageSpecificationConfiguration());
            modelBuilder.Configurations.Add(new LifeEventConfiguration());
            modelBuilder.Configurations.Add(new ServiceClassConfiguration());
            modelBuilder.Configurations.Add(new OntologyTermConfiguration());
            modelBuilder.Configurations.Add(new TargetGroupConfiguration());
            modelBuilder.Configurations.Add(new ServiceServiceClassConfiguration());
            modelBuilder.Configurations.Add(new ServiceLifeEventConfiguration());
            modelBuilder.Configurations.Add(new ServiceOntologyTermConfiguration());
            modelBuilder.Configurations.Add(new ServiceKeywordConfiguration());
            modelBuilder.Configurations.Add(new ServiceTargetGroupConfiguration());
        }
    }
}