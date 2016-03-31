namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class RenameMoreTerms : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            RenameTable("webaddress", "webpage");
            RenameColumn("organization", "streetaddressid", "visitingaddressid");
            RenameColumn("address", "postbox", "postofficebox");
            RenameColumn("language", "description", "name");
            RenameColumn("emailaddress", "address", "email");
            AlterColumn("language", "code", c => c.String(nullable: false, maxLength: 3));
        }
        
        public override void Down()
        {
            RenameColumn("address", "postofficebox", "postbox");
            RenameColumn("language", "name", "description");
            RenameColumn("emailaddress", "email", "address");
            AlterColumn("language", "code", c => c.String(nullable: false, maxLength: 2));
            RenameColumn("organization", "visitingaddressid", "streetaddressid");
            RenameTable("webpage", "webaddress");
        }
    }
}