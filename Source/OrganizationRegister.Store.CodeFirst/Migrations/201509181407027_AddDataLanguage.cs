using System;
using Affecto.EntityFramework.PostgreSql;

namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class AddDataLanguage : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            CreateTable(
                "availabledatalanguage",
                c => new
                    {
                        languageid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.languageid)
                .Index(t => t.languageid);
            AddForeignKey("availabledatalanguage", "languageid", "language", "id");

            DefineLanguages();
        }

        private void DefineLanguages()
        {
            Sql(string.Format("DELETE FROM \"{0}\".\"language\"", PostgreSqlConfiguration.Settings.Schemas["ServiceRegisterContext"]));

            AddServiceLanguage("fi", "suomi", true);
            AddServiceLanguage("sv", "ruotsi");
            AddServiceLanguage("en", "englanti");
            AddServiceLanguage("ru", "venäjä");
            AddServiceLanguage("et", "viro");
            AddServiceLanguage("se", "pohjoissaame");
            AddServiceLanguage("so", "somali");
            AddServiceLanguage("de", "saksa");
            AddServiceLanguage("fr", "ranska");
            AddServiceLanguage("es", "espanja");
            AddServiceLanguage("it", "italia");
            AddServiceLanguage("zh", "kiina");
            AddServiceLanguage("ja", "japani");
            AddServiceLanguage("tr", "turkki");
            AddServiceLanguage("ku", "kurdi");
            AddServiceLanguage("ar", "arabia");
            AddServiceLanguage("bg", "bulgaria");
            AddServiceLanguage("fa", "farsi (persia)");
            AddServiceLanguage("prs", "berberi");
            AddServiceLanguage("fad", "dari");
        }

        private void AddServiceLanguage(string languageCode, string languageName, bool useAlsoAsDataLanguage = false)
        {
            string languageId = Guid.NewGuid().ToString("D");

            Sql(string.Format("INSERT INTO \"{0}\".\"language\"(id, code, name) VALUES('{1}', '{2}', '{3}')", 
                PostgreSqlConfiguration.Settings.Schemas["ServiceRegisterContext"], languageId, languageCode, languageName));
            Sql(string.Format("INSERT INTO \"{0}\".\"availableservicelanguage\"(languageid) VALUES('{1}')", PostgreSqlConfiguration.Settings.Schemas["ServiceRegisterContext"], languageId));

            if (useAlsoAsDataLanguage)
            {
                Sql(string.Format("INSERT INTO \"{0}\".\"availabledatalanguage\"(languageid) VALUES('{1}')", PostgreSqlConfiguration.Settings.Schemas["ServiceRegisterContext"], languageId));
            }
        }

        public override void Down()
        {
            DropForeignKey("availabledatalanguage", "languageid", "language");
            DropIndex("availabledatalanguage", new[] { "languageid" });
            DropTable("availabledatalanguage");

            Sql(string.Format("DELETE FROM \"{0}\".\"availableservicelanguage\"", PostgreSqlConfiguration.Settings.Schemas["ServiceRegisterContext"]));
            Sql(string.Format("DELETE FROM \"{0}\".\"language\"", PostgreSqlConfiguration.Settings.Schemas["ServiceRegisterContext"]));
            Sql(string.Format("INSERT INTO \"{0}\".\"language\"(id, code, name) VALUES('{1}', '{2}', '{3}')",
                PostgreSqlConfiguration.Settings.Schemas["ServiceRegisterContext"], Guid.NewGuid().ToString("D"), "FI", "Finnish"));
        }
    }
}
