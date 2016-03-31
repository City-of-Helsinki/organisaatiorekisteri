namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class AddOntologyTermLowerCaseName : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            AddColumn("ontologyterm", "lowercasename", c => c.String(maxLength: 200));
            SetLowerCaseNames();
        }

        public override void Down()
        {
            DropColumn("ontologyterm", "lowercasename");
        }

        private void SetLowerCaseNames()
        {
            Sql(string.Format("UPDATE {0} SET lowercasename = lower(name);", FormatTableNameWithSchemaNameAndQuotes("ontologyterm")));
        }
    }
}
