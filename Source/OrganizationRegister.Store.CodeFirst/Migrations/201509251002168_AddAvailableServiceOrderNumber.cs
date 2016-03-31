using Affecto.EntityFramework.PostgreSql;

namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class AddAvailableServiceOrderNumber : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            AddColumn("availableservicelanguage", "ordernumber", c => c.Int());

            SetAvailableServiceLanguageOrder("fi", 10);
            SetAvailableServiceLanguageOrder("sv", 20);
            SetAvailableServiceLanguageOrder("en", 30);
        }

        public override void Down()
        {
            DropColumn("availableservicelanguage", "ordernumber");
        }

        private void SetAvailableServiceLanguageOrder(string languageCode, int orderNumber)
        {
            Sql(string.Format("UPDATE \"{0}\".\"availableservicelanguage\" SET ordernumber = {1} WHERE languageid = ( SELECT sl.languageid FROM \"{0}\".\"availableservicelanguage\" sl INNER JOIN \"{0}\".\"language\" l on sl.languageid = l.id WHERE l.code = '{2}');",
                    PostgreSqlConfiguration.Settings.Schemas["ServiceRegisterContext"], orderNumber, languageCode));
        }
    }
}
