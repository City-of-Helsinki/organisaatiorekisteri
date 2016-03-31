namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class AddLifeEvent : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            CreateTable(
                "lifeevent",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        name = c.String(maxLength: 200),
                        sourceid = c.String(maxLength: 200),
                        sourceparentid = c.String(maxLength: 200),
                        ordernumber = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("lifeevent");
        }
    }
}
