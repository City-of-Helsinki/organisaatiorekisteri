namespace ServiceRegister.Store.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShorterServiceShortDescription : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            AlterColumn("servicelanguagespecification", "shortdescription", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("servicelanguagespecification", "shortdescription", c => c.String(maxLength: 4000));
        }
    }
}
