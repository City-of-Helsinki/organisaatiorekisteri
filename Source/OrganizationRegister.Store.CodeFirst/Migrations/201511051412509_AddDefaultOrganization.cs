using System;

namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class AddDefaultOrganization : ServiceRegisterDbMigration
    {
        private static readonly Guid OrganizationId = Guid.Parse("7B45E3BC-EDA9-4F6B-97BB-E9354DB660B5");

        public override void Up()
        {
            AddOrganizationIfNotExists(OrganizationId.ToString("D"), "Valtio", "Väestörekisterikeskus", "0245437-2");
        }

        public override void Down()
        {
        }

        private void AddOrganizationIfNotExists(string id, string type, string name, string businessId)
        {
            string sql = "INSERT INTO {0} (id, typeid, businessid)"
                + " SELECT '{2}', id, '{3}'"
                + " FROM {1}"
                + " WHERE name = '{4}' AND NOT EXISTS (SELECT id FROM {0} WHERE id = '{2}');";

            Sql(string.Format(sql, FormatTableNameWithSchemaNameAndQuotes("organization"), FormatTableNameWithSchemaNameAndQuotes("organizationtype"), id,
                businessId, type));

            sql = "INSERT INTO {0} (organizationid, languageid, name)"
                + " SELECT '{2}', id, '{3}'"
                + " FROM {1}"
                + " WHERE code = 'fi' AND NOT EXISTS (SELECT organizationid FROM {0} WHERE organizationid = '{2}');";

            Sql(string.Format(sql, FormatTableNameWithSchemaNameAndQuotes("organizationlanguagespecification"),
                FormatTableNameWithSchemaNameAndQuotes("language"), id, name));
        }
    }
}