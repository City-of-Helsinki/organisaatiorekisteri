namespace ServiceRegister.Store.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOntologyTerm : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ontologyterm",
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
            DropTable("ontologyterm");
        }
    }
}
