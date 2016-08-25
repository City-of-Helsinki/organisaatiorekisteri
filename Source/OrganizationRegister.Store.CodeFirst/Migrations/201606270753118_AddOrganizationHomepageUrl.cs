namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrganizationHomepageUrl : OrganizationRegisterDbMigration
    {
        public override void Up()
        {
            AddColumn("organizationlanguagespecification", "homepageurl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("organizationlanguagespecification", "homepageurl");
        }
    }
}
