using System;
using System.Data.Entity;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst
{
    public interface IStoreContext
    {
        IDbSet<AvailableDataLanguage> DataLanguages { get; set; }
        IDbSet<OrganizationType> OrganizationTypes { get; set; }
        IDbSet<WebPageType> WebPageTypes { get; }
        IDbSet<Organization> Organizations { get; set; }
        IDbSet<Address> Addresses { get; set; }
        IDbSet<PhoneNumber> PhoneNumbers { get; set; }
        IDbSet<EmailAddress> EmailAddresses { get; set; }
        IDbSet<WebPage> WebPages { get; set; }
        IDbSet<CallChargeType> CallChargeTypes { get; set; }
        AvailableDataLanguage GetDataLanguage(string languageCode);
        OrganizationType GetOrganizationType(string type);
        WebPageType GetWebPageType(string type);
        WebPageType GetWebPageType(Guid guid);
        CallChargeType GetCallChargeType(string type);
        CallChargeType GetCallChargeType(Guid guid);

        void SaveChanges();
    }
}