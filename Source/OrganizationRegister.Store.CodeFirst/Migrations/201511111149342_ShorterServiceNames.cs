namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class ShorterServiceNames : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            AlterColumn("servicelanguagespecification", "name", c => c.String(maxLength: 100));
            AlterColumn("servicelanguagespecification", "alternatename", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("servicelanguagespecification", "alternatename", c => c.String(maxLength: 500));
            AlterColumn("servicelanguagespecification", "name", c => c.String(maxLength: 500));
        }
    }
}
