namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class RenameTerms : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            RenameTable("providertype", "organizationtype");
            RenameColumn("organization", "providertypeid", "typeid");
            RenameColumn("addresslanguagespecification", "postoffice", "postaldistrict");
            RenameColumn("addresslanguagespecification", "specifier", "qualifier");
            RenameColumn("organization", "municipalityid", "municipalitycode");
            RenameColumn("phonenumber", "callchargeinfo", "phonecallfee");
        }
        
        public override void Down()
        {
            RenameColumn("addresslanguagespecification", "postaldistrict", "postoffice");
            RenameColumn("addresslanguagespecification", "qualifier", "specifier");
            RenameColumn("organization", "municipalitycode", "municipalityid");
            RenameColumn("phonenumber", "phonecallfee", "callchargeinfo");
            RenameColumn("organization", "typeid", "providertypeid");
            RenameTable("organizationtype", "providertype");
        }
    }
}