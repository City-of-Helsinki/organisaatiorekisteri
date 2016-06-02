namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrganizationValidity : OrganizationRegisterDbMigration
    {
        public override void Up()
        {
            AddColumn("organization", "validto", c => c.DateTime());
            AddColumn("organization", "validfrom", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("organization", "validfrom");
            DropColumn("organization", "validto");
        }
    }
}
