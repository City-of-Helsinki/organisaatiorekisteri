namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class AddServieAndAvailableServiceLanguage : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            CreateTable(
                "availableservicelanguage",
                c => new
                    {
                        languageid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.languageid)
                .Index(t => t.languageid);
            AddForeignKey("availableservicelanguage", "languageid", "language", "id");
            
            CreateTable(
                "service",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        numericid = c.Long(nullable: false, identity: true),
                        organizationid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.organizationid);
            AddForeignKey("service", "organizationid", "organization", "id");

            CreateTable(
                "servicelanguage",
                c => new
                    {
                        serviceid = c.Guid(nullable: false),
                        languageid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.serviceid, t.languageid })
                .Index(t => t.serviceid)
                .Index(t => t.languageid);
            AddForeignKey("servicelanguage", "languageid", "language", "id", true);
            AddForeignKey("servicelanguage", "serviceid", "service", "id", true);

            CreateTable(
                "servicelanguagespecification",
                c => new
                    {
                        serviceid = c.Guid(nullable: false),
                        languageid = c.Guid(nullable: false),
                        name = c.String(maxLength: 500),
                        alternatename = c.String(maxLength: 500),
                        description = c.String(maxLength: 1000),
                        shortdescription = c.String(maxLength: 1000),
                        userinstruction = c.String(maxLength: 1000),
                        requirement = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => new { t.serviceid, t.languageid })
                .Index(t => t.serviceid)
                .Index(t => t.languageid);
            AddForeignKey("servicelanguagespecification", "languageid", "language", "id", true);
            AddForeignKey("servicelanguagespecification", "serviceid", "service", "id", true);
        }

        public override void Down()
        {
            DropForeignKey("service", "organizationid", "organization");
            DropForeignKey("servicelanguagespecification", "serviceid", "service");
            DropForeignKey("servicelanguagespecification", "languageid", "language");
            DropForeignKey("servicelanguage", "serviceid", "service");
            DropForeignKey("servicelanguage", "languageid", "availableservicelanguage");
            DropForeignKey("availableservicelanguage", "languageid", "language");
            DropIndex("servicelanguagespecification", new[] { "languageid" });
            DropIndex("servicelanguagespecification", new[] { "serviceid" });
            DropIndex("servicelanguage", new[] { "languageid" });
            DropIndex("servicelanguage", new[] { "serviceid" });
            DropIndex("service", new[] { "organizationid" });
            DropIndex("availableservicelanguage", new[] { "languageid" });
            DropTable("servicelanguagespecification");
            DropTable("servicelanguage");
            DropTable("service");
            DropTable("availableservicelanguage");
        }
    }
}
