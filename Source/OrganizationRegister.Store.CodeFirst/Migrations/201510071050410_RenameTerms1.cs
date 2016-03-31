namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class RenameTerms1 : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            RenameColumn("webpage", "description", "name");
            RenameColumn("servicelanguagespecification", "userinstruction", "userinstructions");
            RenameColumn("servicelanguagespecification", "requirement", "requirements");
        }
        
        public override void Down()
        {
            RenameColumn("webpage", "name", "description");
            RenameColumn("servicelanguagespecification", "userinstructions", "userinstruction");
            RenameColumn("servicelanguagespecification", "requirements", "requirement");
        }
    }
}
