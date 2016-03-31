namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class UpdateForeignKeys : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            DropForeignKey("addresslanguagespecification", "addressid", "address");
            DropForeignKey("addresslanguagespecification", "languageid", "availabledatalanguage");
            DropForeignKey("organizationlanguagespecification", "organizationid", "organization");
            DropForeignKey("organization", "typeid", "organizationtype");
            DropForeignKey("organizationlanguagespecification", "languageid", "availabledatalanguage");
            DropForeignKey("servicelanguage", "serviceid", "service");
            DropForeignKey("servicelanguagespecification", "serviceid", "service");
            DropForeignKey("service", "organizationid", "organization");
            DropForeignKey("servicelanguage", "languageid", "availableservicelanguage");
            DropForeignKey("servicelanguagespecification", "languageid", "availabledatalanguage");
            AddForeignKey("addresslanguagespecification", "addressid", "address", "id");
            AddForeignKey("addresslanguagespecification", "languageid", "availabledatalanguage", "languageid");
            AddForeignKey("organizationlanguagespecification", "organizationid", "organization", "id");
            AddForeignKey("organization", "typeid", "organizationtype", "id");
            AddForeignKey("organizationlanguagespecification", "languageid", "availabledatalanguage", "languageid");
            AddForeignKey("servicelanguage", "serviceid", "service", "id");
            AddForeignKey("servicelanguagespecification", "serviceid", "service", "id");
            AddForeignKey("service", "organizationid", "organization", "id");
            AddForeignKey("servicelanguage", "languageid", "availableservicelanguage", "languageid");
            AddForeignKey("servicelanguagespecification", "languageid", "availabledatalanguage", "languageid");
        }
        
        public override void Down()
        {
            DropForeignKey("servicelanguagespecification", "languageid", "availabledatalanguage");
            DropForeignKey("servicelanguage", "languageid", "availableservicelanguage");
            DropForeignKey("service", "organizationid", "organization");
            DropForeignKey("servicelanguagespecification", "serviceid", "service");
            DropForeignKey("servicelanguage", "serviceid", "service");
            DropForeignKey("organizationlanguagespecification", "languageid", "availabledatalanguage");
            DropForeignKey("organization", "typeid", "organizationtype");
            DropForeignKey("organizationlanguagespecification", "organizationid", "organization");
            DropForeignKey("addresslanguagespecification", "languageid", "availabledatalanguage");
            DropForeignKey("addresslanguagespecification", "addressid", "address");
            AddForeignKey("servicelanguagespecification", "languageid", "availabledatalanguage", "languageid", cascadeDelete: true);
            AddForeignKey("servicelanguage", "languageid", "availableservicelanguage", "languageid", cascadeDelete: true);
            AddForeignKey("service", "organizationid", "organization", "id", cascadeDelete: true);
            AddForeignKey("servicelanguagespecification", "serviceid", "service", "id", cascadeDelete: true);
            AddForeignKey("servicelanguage", "serviceid", "service", "id", cascadeDelete: true);
            AddForeignKey("organizationlanguagespecification", "languageid", "availabledatalanguage", "languageid", cascadeDelete: true);
            AddForeignKey("organization", "typeid", "organizationtype", "id", cascadeDelete: true);
            AddForeignKey("organizationlanguagespecification", "organizationid", "organization", "id", cascadeDelete: true);
            AddForeignKey("addresslanguagespecification", "languageid", "availabledatalanguage", "languageid", cascadeDelete: true);
            AddForeignKey("addresslanguagespecification", "addressid", "address", "id", cascadeDelete: true);
        }
    }
}