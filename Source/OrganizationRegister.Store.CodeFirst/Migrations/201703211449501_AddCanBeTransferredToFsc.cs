namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCanBeTransferredToFsc : OrganizationRegisterDbMigration
    {
        public override void Up()
        {
            AddColumn("organization", "canbetransferredtofsc", c => c.Boolean(nullable: false, defaultValue:true));
        }
        
        public override void Down()
        {
            DropColumn("organization", "canbetransferredtofsc");
        }
    }
}
