using System;
using System.Data.Entity;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst
{
    public interface IStoreContext
    {
        IDbSet<AvailableServiceLanguage> ServiceLanguages { get; set; }
        IDbSet<AvailableDataLanguage> DataLanguages { get; set; }
        IDbSet<OrganizationType> OrganizationTypes { get; set; }
        IDbSet<WebPageType> WebPageTypes { get; }
        IDbSet<Organization> Organizations { get; set; }
        IDbSet<Service> Services { get; set; }
        IDbSet<Address> Addresses { get; set; }
        IDbSet<PhoneNumber> PhoneNumbers { get; set; }
        IDbSet<EmailAddress> EmailAddresses { get; set; }
        IDbSet<WebPage> WebPages { get; set; }
        IDbSet<LifeEvent> LifeEvents { get; set; }
        IDbSet<ServiceClass> ServiceClasses { get; set; }
        IDbSet<OntologyTerm> OntologyTerms { get; set; }
        IDbSet<TargetGroup> TargetGroups { get; set; }
        IDbSet<ServiceServiceClass> ServiceServiceClasses { get; set; }
        IDbSet<ServiceLifeEvent> ServiceLifeEvents { get; set; }
        IDbSet<ServiceOntologyTerm> ServiceOntologyTerms { get; set; }
        IDbSet<ServiceKeyword> ServiceKeywords { get; set; }
        IDbSet<ServiceTargetGroup> ServiceTargetGroups { get; set; }


        AvailableDataLanguage GetDataLanguage(string languageCode);
        AvailableServiceLanguage GetServiceLanguage(string languageCode);
        OrganizationType GetOrganizationType(string type);
        WebPageType GetWebPageType(string type);
        WebPageType GetWebPageType(Guid guid);

        void SaveChanges();
    }
}