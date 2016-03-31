namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class AddServiceClass : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            CreateTable(
                "serviceclass",
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
            DropTable("serviceclass");
        }
    }
}
