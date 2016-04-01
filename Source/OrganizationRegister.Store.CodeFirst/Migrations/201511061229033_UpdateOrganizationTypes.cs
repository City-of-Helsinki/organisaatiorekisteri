using System;

namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class UpdateOrganizationTypes : ServiceRegisterDbMigration
    {
        public override void Up()
        {

            // insert new values
            Sql(string.Format("INSERT INTO {0} (id, name, sourceid, ordernumber) VALUES('{1}', '{2}', '{3}', '{4}')",
            FormatTableNameWithSchemaNameAndQuotes("organizationtype"), Guid.NewGuid().ToString("D"), "Valtio", "http://urn./URN:NBN::au:ptvl:TT1.1", 10));

            Sql(string.Format("INSERT INTO {0} (id, name, sourceid, ordernumber) VALUES('{1}', '{2}', '{3}', '{4}')",
            FormatTableNameWithSchemaNameAndQuotes("organizationtype"), Guid.NewGuid().ToString("D"), "Kunta", "http://urn./URN:NBN::au:ptvl:TT1.2", 20));

            Sql(string.Format("INSERT INTO {0} (id, name, sourceid, ordernumber) VALUES('{1}', '{2}', '{3}', '{4}')",
            FormatTableNameWithSchemaNameAndQuotes("organizationtype"), Guid.NewGuid().ToString("D"), "Alueellinen yhteistoimintaorganisaatio", "http://urn./URN:NBN::au:ptvl:TT1.3", 30));

            Sql(string.Format("INSERT INTO {0} (id, name, sourceid, ordernumber) VALUES('{1}', '{2}', '{3}', '{4}')",
            FormatTableNameWithSchemaNameAndQuotes("organizationtype"), Guid.NewGuid().ToString("D"), "Yritykset", "http://urn./URN:NBN::au:ptvl:TT2.1", 40));

            Sql(string.Format("INSERT INTO {0} (id, name, sourceid, ordernumber) VALUES('{1}', '{2}', '{3}', '{4}')",
                FormatTableNameWithSchemaNameAndQuotes("organizationtype"), Guid.NewGuid().ToString("D"), "Järjestöt", "http://urn./URN:NBN::au:ptvl:TT2.2", 50));

            // update type in organizations

            //"Kolmas sektori"
            UpdateOrganizationType("Kolmas sektori", "Järjestöt");

            // "Kunnan liikelaitos"
            UpdateOrganizationType("Kunnan liikelaitos", "Kunta");

            // "Kunta"
            UpdateOrganizationType("Kunta", "Kunta");

            // "Kuntayhtymä"
            UpdateOrganizationType("Kuntayhtymän liikelaitos", "Alueellinen yhteistoimintaorganisaatio");

            // "Valtio"
            // "Valtion aluehallinto"
            // "Valtion keskushallinto"
            // "Valtion liikelaitos"
            // "Valtion paikallishallinto"
            UpdateOrganizationType("Valtio", "Valtio");
            UpdateOrganizationType("Valtion paikallishallinto", "Valtio");
            UpdateOrganizationType("Valtion aluehallinto", "Valtio");
            UpdateOrganizationType("Valtion keskushallinto", "Valtio");

            //"Yritys"
            UpdateOrganizationType("Yritys", "Yritykset");


            // delete old types
            Sql(string.Format("delete from {0} where sourceid is null ", FormatTableNameWithSchemaNameAndQuotes("organizationtype")));

        }

        public override void Down()
        {
            Sql(string.Format("delete from {0} where sourceid is not null ", FormatTableNameWithSchemaNameAndQuotes("organizationtype")));
        }


        private void UpdateOrganizationType(string fromType, string toType)
        {

            Sql(string.Format("update {0} set typeid = (select id from {1} where name = '{2}' and sourceid is not null) where typeid in (select id from {1} where name = '{3}' and sourceid is null)", FormatTableNameWithSchemaNameAndQuotes("organization"), FormatTableNameWithSchemaNameAndQuotes("organizationtype"),toType, fromType));
        }

    }
}
