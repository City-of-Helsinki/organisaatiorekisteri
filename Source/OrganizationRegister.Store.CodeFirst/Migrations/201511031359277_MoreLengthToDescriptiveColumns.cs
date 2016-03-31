using System.Data.Entity.Migrations;

namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class MoreLengthToDescriptiveColumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("organizationlanguagespecification", "description", c => c.String(maxLength: 4000));
            AlterColumn("servicekeyword", "keyword", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("servicelanguagespecification", "description", c => c.String(maxLength: 4000));
            AlterColumn("servicelanguagespecification", "shortdescription", c => c.String(maxLength: 4000));
            AlterColumn("servicelanguagespecification", "userinstructions", c => c.String(maxLength: 4000));
            AlterColumn("servicelanguagespecification", "requirements", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("servicelanguagespecification", "requirements", c => c.String(maxLength: 1000));
            AlterColumn("servicelanguagespecification", "userinstructions", c => c.String(maxLength: 1000));
            AlterColumn("servicelanguagespecification", "shortdescription", c => c.String(maxLength: 1000));
            AlterColumn("servicelanguagespecification", "description", c => c.String(maxLength: 1000));
            AlterColumn("servicekeyword", "keyword", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("organizationlanguagespecification", "description", c => c.String(maxLength: 1000));
        }
    }
}
