namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrganizationNameAbbreviation : OrganizationRegisterDbMigration
    {
        public override void Up()
        {
            AddColumn("organizationlanguagespecification", "nameabbreviation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("organizationlanguagespecification", "nameabbreviation");
        }
    }
}
