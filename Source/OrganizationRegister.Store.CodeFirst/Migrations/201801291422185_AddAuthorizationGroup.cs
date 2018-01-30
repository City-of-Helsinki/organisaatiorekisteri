namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthorizationGroup : OrganizationRegisterDbMigration
    {
        public override void Up()
        {
            CreateTable(
                "authorizationgroup",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        organizationid = c.Guid(nullable: false),
                        groupid = c.Guid(nullable: false),
                        groupname = c.String(nullable:false),
                        roleid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.organizationid);

            AddForeignKey("authorizationgroup", "organizationid", "organization", "id");

        }
        
        public override void Down()
        {
            DropForeignKey("authorizationgroup", "organizationid", "organization");
            DropIndex("authorizationgroup", new[] { "organizationid" });
            DropTable("authorizationgroup");
        }
    }
}
