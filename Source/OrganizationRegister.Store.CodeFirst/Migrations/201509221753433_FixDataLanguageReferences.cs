namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class FixDataLanguageReferences : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            AddColumn("organization", "streetaddressaspostaladdress", c => c.Boolean());
            DropForeignKey("organizationlanguagespecification", "languageid", "language");
            DropForeignKey("servicelanguagespecification", "languageid", "language");
            DropForeignKey("addresslanguagespecification", "languageid", "language");
            AddForeignKey("organizationlanguagespecification", "languageid", 
                "availabledatalanguage", "languageid", true, "FK_organizationdatalanguage");
            AddForeignKey("servicelanguagespecification", "languageid",
                "availabledatalanguage", "languageid", true, "FK_servicedatalanguage");
            AddForeignKey("addresslanguagespecification", "languageid",
                "availabledatalanguage", "languageid", true, "FK_addressdatalanguage");
        }

        public override void Down()
        {
            DropColumn("organization", "streetaddressaspostaladdress");
            DropForeignKey("organizationlanguagespecification", "FK_organizationdatalanguage");
            DropForeignKey("servicelanguagespecification", "FK_servicedatalanguage");
            DropForeignKey("addresslanguagespecification", "FK_addressdatalanguage");
            AddForeignKey("organizationlanguagespecification", "languageid",
                "language", "id", true);
            AddForeignKey("servicelanguagespecification", "languageid",
                "language", "id", true);
            AddForeignKey("addresslanguagespecification", "languageid",
                "language", "id", true);
        }
    }
}
