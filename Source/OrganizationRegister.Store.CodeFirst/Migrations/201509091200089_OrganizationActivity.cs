namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class OrganizationActivity : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            AddColumn("organization", "active", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("organization", "active");
        }
    }
}