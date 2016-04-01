using System;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class Initial : OrganizationRegisterDbMigration
    {
        public override void Up()
        {
            CreateTable(
                "address",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        postalcode = c.String(),
                        postofficebox = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "addresslanguagespecification",
                c => new
                    {
                        addressid = c.Guid(nullable: false),
                        languageid = c.Guid(nullable: false),
                        streetaddress = c.String(),
                        postaldistrict = c.String(),
                        qualifier = c.String(),
                    })
                .PrimaryKey(t => new { t.addressid, t.languageid })
                .Index(t => t.addressid)
                .Index(t => t.languageid);
            AddForeignKey("addresslanguagespecification", "languageid", "availabledatalanguage", "languageid");
            AddForeignKey("addresslanguagespecification", "addressid", "address", "id");

            CreateTable(
                "availabledatalanguage",
                c => new
                    {
                        languageid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.languageid)
                .Index(t => t.languageid);
            AddForeignKey("availabledatalanguage", "languageid", "language", "id");

            CreateTable(
                "language",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        code = c.String(nullable: false),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "emailaddress",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "organization",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        typeid = c.Guid(nullable: false),
                        parentorganizationid = c.Guid(),
                        emailaddressid = c.Guid(),
                        phonenumberid = c.Guid(),
                        visitingaddressid = c.Guid(),
                        numericid = c.Long(nullable: false, identity: true),
                        businessid = c.String(),
                        municipalitycode = c.Int(),
                        oid = c.String(),
                        active = c.Boolean(nullable: false),
                        streetaddressaspostaladdress = c.Boolean(),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.typeid)
                .Index(t => t.parentorganizationid)
                .Index(t => t.emailaddressid)
                .Index(t => t.phonenumberid)
                .Index(t => t.visitingaddressid);
            AddForeignKey("organization", "emailaddressid", "emailaddress", "id");
            AddForeignKey("organization", "parentorganizationid", "organization", "id");
            AddForeignKey("organization", "phonenumberid", "phonenumber", "id");
            AddForeignKey("organization", "typeid", "organizationtype", "id");
            AddForeignKey("organization", "visitingaddressid", "address", "id");

            CreateTable(
                "organizationlanguagespecification",
                c => new
                    {
                        organizationid = c.Guid(nullable: false),
                        languageid = c.Guid(nullable: false),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => new { t.organizationid, t.languageid })
                .Index(t => t.organizationid)
                .Index(t => t.languageid);
            AddForeignKey("organizationlanguagespecification", "languageid", "availabledatalanguage", "languageid");
            AddForeignKey("organizationlanguagespecification", "organizationid", "organization", "id");

            CreateTable(
                "phonenumber",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        number = c.String(nullable: false),
                        phonecallfee = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "organizationtype",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        name = c.String(nullable: false),
                        sourceid = c.String(),
                        ordernumber = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "webpage",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        name = c.String(nullable: false),
                        url = c.String(nullable: false),
                        typeid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.typeid);
            AddForeignKey("webpage", "typeid", "webpagetype", "id");

            CreateTable(
                "webpagetype",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "organization_postaladdress",
                c => new
                    {
                        organizationid = c.Guid(nullable: false),
                        addressid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.organizationid, t.addressid })
                .Index(t => t.organizationid)
                .Index(t => t.addressid);
            AddForeignKey("organization_postaladdress", "organizationid", "organization", "id");
            AddForeignKey("organization_postaladdress", "addressid", "address", "id");

            CreateTable(
                "organization_webaddress",
                c => new
                    {
                        organizationid = c.Guid(nullable: false),
                        webaddressid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.organizationid, t.webaddressid })
                .Index(t => t.organizationid)
                .Index(t => t.webaddressid);
            AddForeignKey("organization_webaddress", "organizationid", "organization", "id");
            AddForeignKey("organization_webaddress", "webaddressid", "webpage", "id");

            AddSettings();
        }

        public override void Down()
        {
            DropForeignKey("organization", "visitingaddressid", "address");
            DropForeignKey("organization_webaddress", "webaddressid", "webpage");
            DropForeignKey("organization_webaddress", "organizationid", "organization");
            DropForeignKey("webpage", "typeid", "webpagetype");
            DropForeignKey("organization", "typeid", "organizationtype");
            DropForeignKey("organization_postaladdress", "addressid", "address");
            DropForeignKey("organization_postaladdress", "organizationid", "organization");
            DropForeignKey("organization", "phonenumberid", "phonenumber");
            DropForeignKey("organization", "parentorganizationid", "organization");
            DropForeignKey("organizationlanguagespecification", "organizationid", "organization");
            DropForeignKey("organizationlanguagespecification", "languageid", "availabledatalanguage");
            DropForeignKey("organization", "emailaddressid", "emailaddress");
            DropForeignKey("addresslanguagespecification", "addressid", "address");
            DropForeignKey("addresslanguagespecification", "languageid", "availabledatalanguage");
            DropForeignKey("availabledatalanguage", "languageid", "language");
            DropIndex("organization_webaddress", new[] { "webaddressid" });
            DropIndex("organization_webaddress", new[] { "organizationid" });
            DropIndex("organization_postaladdress", new[] { "addressid" });
            DropIndex("organization_postaladdress", new[] { "organizationid" });
            DropIndex("webpage", new[] { "typeid" });
            DropIndex("organizationlanguagespecification", new[] { "languageid" });
            DropIndex("organizationlanguagespecification", new[] { "organizationid" });
            DropIndex("organization", new[] { "visitingaddressid" });
            DropIndex("organization", new[] { "phonenumberid" });
            DropIndex("organization", new[] { "emailaddressid" });
            DropIndex("organization", new[] { "parentorganizationid" });
            DropIndex("organization", new[] { "typeid" });
            DropIndex("availabledatalanguage", new[] { "languageid" });
            DropIndex("addresslanguagespecification", new[] { "languageid" });
            DropIndex("addresslanguagespecification", new[] { "addressid" });
            DropTable("organization_webaddress");
            DropTable("organization_postaladdress");
            DropTable("webpagetype");
            DropTable("webpage");
            DropTable("organizationtype");
            DropTable("phonenumber");
            DropTable("organizationlanguagespecification");
            DropTable("organization");
            DropTable("emailaddress");
            DropTable("language");
            DropTable("availabledatalanguage");
            DropTable("addresslanguagespecification");
            DropTable("address");
        }

        private void AddSettings()
        {
            AddOrganizationTypes();
            AddLanguages();
            AddWebPageTypes();
        }

        private void AddWebPageTypes()
        {
            Sql(string.Format("INSERT INTO {0}(id, type) VALUES('{1}', '{2}')",
                FormatTableNameWithSchemaNameAndQuotes("webpagetype"), Guid.NewGuid().ToString("D"), "Sosiaalisen median palvelu"));
            Sql(string.Format("INSERT INTO {0}(id, type) VALUES('{1}', '{2}')",
                FormatTableNameWithSchemaNameAndQuotes("webpagetype"), Guid.NewGuid().ToString("D"), "Kotisivu"));
        }

        private void AddLanguages()
        {
            string finnishId = Guid.NewGuid().ToString("D");
            string swedishId = Guid.NewGuid().ToString("D");
            string englishId = Guid.NewGuid().ToString("D");

            Sql(string.Format("INSERT INTO {0}(id, code, name) VALUES('{1}', '{2}', '{3}')",
                FormatTableNameWithSchemaNameAndQuotes("language"), finnishId, "fi", "suomi"));
            Sql(string.Format("INSERT INTO {0}(id, code, name) VALUES('{1}', '{2}', '{3}')",
                FormatTableNameWithSchemaNameAndQuotes("language"), swedishId, "sv", "ruotsi"));
            Sql(string.Format("INSERT INTO {0}(id, code, name) VALUES('{1}', '{2}', '{3}')",
                FormatTableNameWithSchemaNameAndQuotes("language"), englishId, "en", "englanti"));

            Sql(string.Format("INSERT INTO {0}(languageid) VALUES('{1}')", FormatTableNameWithSchemaNameAndQuotes("availabledatalanguage"), finnishId));
            Sql(string.Format("INSERT INTO {0}(languageid) VALUES('{1}')", FormatTableNameWithSchemaNameAndQuotes("availabledatalanguage"), swedishId));
            Sql(string.Format("INSERT INTO {0}(languageid) VALUES('{1}')", FormatTableNameWithSchemaNameAndQuotes("availabledatalanguage"), englishId));
        }

        private void AddOrganizationTypes()
        {
            Sql(string.Format("INSERT INTO {0}(id, name, sourceid, ordernumber) VALUES('{1}', '{2}', '{3}', {4})",
                FormatTableNameWithSchemaNameAndQuotes("organizationtype"), Guid.NewGuid().ToString("D"), "Yritykset", "http://urn./URN:NBN::au:ptvl:TT2.1", 40));
            Sql(string.Format("INSERT INTO {0}(id, name, sourceid, ordernumber) VALUES('{1}', '{2}', '{3}', {4})",
                FormatTableNameWithSchemaNameAndQuotes("organizationtype"), Guid.NewGuid().ToString("D"), "Järjestöt", "http://urn./URN:NBN::au:ptvl:TT2.2", 50));
            Sql(string.Format("INSERT INTO {0}(id, name, sourceid, ordernumber) VALUES('{1}', '{2}', '{3}', {4})",
                FormatTableNameWithSchemaNameAndQuotes("organizationtype"), Guid.NewGuid().ToString("D"), "Alueellinen yhteistoimintaorganisaatio",
                "http://urn./URN:NBN::au:ptvl:TT1.3", 30));
            Sql(string.Format("INSERT INTO {0}(id, name, sourceid, ordernumber) VALUES('{1}', '{2}', '{3}', {4})",
                FormatTableNameWithSchemaNameAndQuotes("organizationtype"), Guid.NewGuid().ToString("D"), "Valtio", "http://urn./URN:NBN::au:ptvl:TT1.1", 10));
            Sql(string.Format("INSERT INTO {0}(id, name, sourceid, ordernumber) VALUES('{1}', '{2}', '{3}', {4})",
                FormatTableNameWithSchemaNameAndQuotes("organizationtype"), Guid.NewGuid().ToString("D"), "Kunta", "http://urn./URN:NBN::au:ptvl:TT1.2", 20));
        }
    }
}
