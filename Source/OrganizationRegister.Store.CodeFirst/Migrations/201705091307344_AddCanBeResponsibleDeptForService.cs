namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCanBeResponsibleDeptForService : OrganizationRegisterDbMigration
    {
        public override void Up()
        {
            AddColumn("organization", "canberesponsibledeptforservice", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("organization", "canberesponsibledeptforservice");
        }
    }
}
