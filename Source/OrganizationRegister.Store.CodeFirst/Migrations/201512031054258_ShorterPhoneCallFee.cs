namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class ShorterPhoneCallFee : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            AlterColumn("phonenumber", "phonecallfee", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("phonenumber", "phonecallfee", c => c.String(maxLength: 500));
        }
    }
}
