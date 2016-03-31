namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class AddServiceClassification : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            CreateTable(
                "servicekeyword",
                c => new
                {
                    id = c.Guid(nullable: false),
                    languageid = c.Guid(nullable: false),
                    serviceid = c.Guid(nullable: false),
                    keyword = c.String(nullable: false, maxLength: 500),
                })
                .PrimaryKey(t => t.id)

                .Index(t => t.languageid)
                .Index(t => t.serviceid);

            AddForeignKey("servicekeyword", "languageid", "availabledatalanguage", "languageid", false, "FK_language");
            AddForeignKey("servicekeyword", "serviceid", "service", "id", false, "FK_service");

            CreateTable(
                "servicelifeevent",
                c => new
                {
                    serviceid = c.Guid(nullable: false),
                    lifeeventid = c.Guid(nullable: false),
                })
                .PrimaryKey(t => new { t.serviceid, t.lifeeventid })

                .Index(t => t.serviceid)
                .Index(t => t.lifeeventid);

            AddForeignKey("servicelifeevent", "lifeeventid", "lifeevent", "id", false, "FK_lifeevent");
            AddForeignKey("servicelifeevent", "serviceid", "service", "id", false, "FK_service");

            CreateTable(
                "serviceontologyterm",
                c => new
                {
                    serviceid = c.Guid(nullable: false),
                    ontologytermid = c.Guid(nullable: false),
                })
                .PrimaryKey(t => new { t.serviceid, t.ontologytermid })

                .Index(t => t.serviceid)
                .Index(t => t.ontologytermid);

            AddForeignKey("serviceontologyterm", "ontologytermid", "ontologyterm", "id", false, "FK_ontologyterm");
            AddForeignKey("serviceontologyterm", "serviceid", "service", "id", false, "FK_service");



            CreateTable(
                "serviceserviceclass",
                c => new
                {
                    serviceid = c.Guid(nullable: false),
                    serviceclassid = c.Guid(nullable: false),
                })
                .PrimaryKey(t => new { t.serviceid, t.serviceclassid })

                .Index(t => t.serviceid)
                .Index(t => t.serviceclassid);

            AddForeignKey("serviceserviceclass", "serviceclassid", "serviceclass", "id", false, "FK_serviceclass");
            AddForeignKey("serviceserviceclass", "serviceid", "service", "id", false, "FK_service");


            CreateTable(
                "servicetargetgroup",
                c => new
                {
                    serviceid = c.Guid(nullable: false),
                    targetgroupid = c.Guid(nullable: false),
                })
                .PrimaryKey(t => new { t.serviceid, t.targetgroupid })

                .Index(t => t.serviceid)
                .Index(t => t.targetgroupid);

            AddForeignKey("servicetargetgroup", "targetgroupid", "targetgroup", "id", false, "FK_targetgroup");
            AddForeignKey("servicetargetgroup", "serviceid", "service", "id", false, "FK_service");

        }

        public override void Down()
        {
            DropForeignKey("servicekeyword", "serviceid", "service");
            DropForeignKey("servicetargetgroup", "serviceid", "service");
            DropForeignKey("servicetargetgroup", "targetgroupid", "targetgroup");
            DropForeignKey("serviceserviceclass", "serviceid", "service");
            DropForeignKey("serviceserviceclass", "serviceclassid", "serviceclass");
            DropForeignKey("serviceontologyterm", "serviceid", "service");
            DropForeignKey("serviceontologyterm", "ontologytermid", "ontologyterm");
            DropForeignKey("servicelifeevent", "serviceid", "service");
            DropForeignKey("servicelifeevent", "lifeeventid", "lifeevent");
            DropForeignKey("servicekeyword", "languageid", "availabledatalanguage");
            DropIndex("servicetargetgroup", new[] { "targetgroupid" });
            DropIndex("servicetargetgroup", new[] { "serviceid" });
            DropIndex("serviceserviceclass", new[] { "serviceclassid" });
            DropIndex("serviceserviceclass", new[] { "serviceid" });
            DropIndex("serviceontologyterm", new[] { "ontologytermid" });
            DropIndex("serviceontologyterm", new[] { "serviceid" });
            DropIndex("servicelifeevent", new[] { "lifeeventid" });
            DropIndex("servicelifeevent", new[] { "serviceid" });
            DropIndex("servicekeyword", new[] { "serviceid" });
            DropIndex("servicekeyword", new[] { "languageid" });
            DropTable("servicetargetgroup");
            DropTable("serviceserviceclass");
            DropTable("serviceontologyterm");
            DropTable("servicelifeevent");
            DropTable("servicekeyword");
        }
    }
}
