using System;

namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class Initial : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            CreateTable(
                "address",
                c => new
                {
                    id = c.Guid(nullable: false),
                    postalcode = c.String(maxLength: 100),
                    postbox = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "addresslanguagespecification",
                c => new
                {
                    addressid = c.Guid(nullable: false),
                    languageid = c.Guid(nullable: false),
                    streetaddress = c.String(maxLength: 200),
                    postoffice = c.String(maxLength: 100),
                    specifier = c.String(maxLength: 500),
                })
                .PrimaryKey(t => new { t.addressid, t.languageid })
                .Index(t => t.addressid)
                .Index(t => t.languageid);

            AddForeignKey("addresslanguagespecification", "languageid", "language", "id", true, "FK_language");
            AddForeignKey("addresslanguagespecification", "addressid", "address", "id", true, "FK_address");

            CreateTable(
                "language",
                c => new
                {
                    id = c.Guid(nullable: false),
                    code = c.String(nullable: false, maxLength: 2),
                    description = c.String(nullable: false, maxLength: 100),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "emailaddress",
                c => new
                {
                    id = c.Guid(nullable: false),
                    address = c.String(nullable: false, maxLength: 200),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "organization",
                c => new
                {
                    id = c.Guid(nullable: false),
                    providertypeid = c.Guid(nullable: false),
                    parentorganizationid = c.Guid(),
                    emailaddressid = c.Guid(),
                    phonenumberid = c.Guid(),
                    streetaddressid = c.Guid(),
                    numericid = c.Long(nullable: false, identity: true),
                    businessid = c.String(maxLength: 9),
                    municipalityid = c.Int(),
                    oid = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.id)
                .Index(t => t.providertypeid)
                .Index(t => t.parentorganizationid)
                .Index(t => t.emailaddressid)
                .Index(t => t.phonenumberid)
                .Index(t => t.streetaddressid);

            AddForeignKey("organization", "emailaddressid", "emailaddress", "id", false, "FK_emailaddress");
            AddForeignKey("organization", "parentorganizationid", "organization", "id", false, "FK_parentorganization");
            AddForeignKey("organization", "phonenumberid", "phonenumber", "id", false, "FK_phonenumber");
            AddForeignKey("organization", "providertypeid", "providertype", "id", true, "FK_providertype");
            AddForeignKey("organization", "streetaddressid", "address", "id", false, "FK_streetaddress");

            CreateTable(
                "organizationlanguagespecification",
                c => new
                {
                    organizationid = c.Guid(nullable: false),
                    languageid = c.Guid(nullable: false),
                    name = c.String(maxLength: 500),
                    description = c.String(maxLength: 1000),
                })
                .PrimaryKey(t => new { t.organizationid, t.languageid })
                .Index(t => t.organizationid)
                .Index(t => t.languageid);

            AddForeignKey("organizationlanguagespecification", "languageid", "language", "id", true, "FK_language");
            AddForeignKey("organizationlanguagespecification", "organizationid", "organization", "id", true, "FK_organization");

            CreateTable(
                "phonenumber",
                c => new
                {
                    id = c.Guid(nullable: false),
                    number = c.String(nullable: false, maxLength: 50),
                    callchargeinfo = c.String(maxLength: 500),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "providertype",
                c => new
                {
                    id = c.Guid(nullable: false),
                    name = c.String(nullable: false, maxLength: 200),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "webaddress",
                c => new
                {
                    id = c.Guid(nullable: false),
                    description = c.String(nullable: false, maxLength: 500),
                    url = c.String(nullable: false, maxLength: 1000),
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

            AddForeignKey("organization_postaladdress", "organizationid", "organization", "id", true, "FK_organization");
            AddForeignKey("organization_postaladdress", "addressid", "address", "id", true, "FK_address");

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

            AddForeignKey("organization_webaddress", "organizationid", "organization", "id", true, "FK_organization");
            AddForeignKey("organization_webaddress", "webaddressid", "webaddress", "id", true, "FK_webaddress");

            Sql(string.Format("INSERT INTO {0}(id, code, description) VALUES('{1}', '{2}', '{3}')",
                FormatTableNameWithSchemaNameAndQuotes("language"), Guid.NewGuid().ToString("D"), "FI", "Finnish"));

            Sql(string.Format("INSERT INTO {0}(id, name) VALUES('{1}', '{2}')",
                FormatTableNameWithSchemaNameAndQuotes("providertype"), Guid.NewGuid().ToString("D"), "Kuntayhtymä"));

            Sql(string.Format("INSERT INTO {0}(id, name) VALUES('{1}', '{2}')",
                FormatTableNameWithSchemaNameAndQuotes("providertype"), Guid.NewGuid().ToString("D"), "Kuntayhtymän liikelaitos"));

            Sql(string.Format("INSERT INTO {0}(id, name) VALUES('{1}', '{2}')",
                FormatTableNameWithSchemaNameAndQuotes("providertype"), Guid.NewGuid().ToString("D"), "Kunta"));

            Sql(string.Format("INSERT INTO {0}(id, name) VALUES('{1}', '{2}')",
                FormatTableNameWithSchemaNameAndQuotes("providertype"), Guid.NewGuid().ToString("D"), "Kunnan liikelaitos"));

            Sql(string.Format("INSERT INTO {0}(id, name) VALUES('{1}', '{2}')",
                FormatTableNameWithSchemaNameAndQuotes("providertype"), Guid.NewGuid().ToString("D"), "Valtio"));

            Sql(string.Format("INSERT INTO {0}(id, name) VALUES('{1}', '{2}')",
                FormatTableNameWithSchemaNameAndQuotes("providertype"), Guid.NewGuid().ToString("D"), "Valtion aluehallinto"));

            Sql(string.Format("INSERT INTO {0}(id, name) VALUES('{1}', '{2}')",
                FormatTableNameWithSchemaNameAndQuotes("providertype"), Guid.NewGuid().ToString("D"), "Valtion keskushallinto"));

            Sql(string.Format("INSERT INTO {0}(id, name) VALUES('{1}', '{2}')",
                FormatTableNameWithSchemaNameAndQuotes("providertype"), Guid.NewGuid().ToString("D"), "Valtion liikelaitos"));

            Sql(string.Format("INSERT INTO {0}(id, name) VALUES('{1}', '{2}')",
                FormatTableNameWithSchemaNameAndQuotes("providertype"), Guid.NewGuid().ToString("D"), "Valtion paikallishallinto"));

            Sql(string.Format("INSERT INTO {0}(id, name) VALUES('{1}', '{2}')",
                FormatTableNameWithSchemaNameAndQuotes("providertype"), Guid.NewGuid().ToString("D"), "Yritys"));

            Sql(string.Format("INSERT INTO {0}(id, name) VALUES('{1}', '{2}')",
                FormatTableNameWithSchemaNameAndQuotes("providertype"), Guid.NewGuid().ToString("D"), "Kolmas sektori"));
        }

        public override void Down()
        {
            DropForeignKey("organization_webaddress", "webaddressid", "webaddress");
            DropForeignKey("organization_webaddress", "organizationid", "organization");
            DropForeignKey("organization", "streetaddressid", "address");
            DropForeignKey("organization", "providertypeid", "providertype");
            DropForeignKey("organization", "phonenumberid", "phonenumber");
            DropForeignKey("organization", "parentorganizationid", "organization");
            DropForeignKey("organizationlanguagespecification", "organizationid", "organization");
            DropForeignKey("organizationlanguagespecification", "languageid", "language");
            DropForeignKey("organization", "emailaddressid", "emailaddress");
            DropForeignKey("organization_postaladdress", "addressid", "address");
            DropForeignKey("organization_postaladdress", "organizationid", "organization");
            DropForeignKey("addresslanguagespecification", "addressid", "address");
            DropForeignKey("addresslanguagespecification", "languageid", "language");
            DropIndex("organization_webaddress", new[] { "webaddressid" });
            DropIndex("organization_webaddress", new[] { "organizationid" });
            DropIndex("organization_postaladdress", new[] { "addressid" });
            DropIndex("organization_postaladdress", new[] { "organizationid" });
            DropIndex("organizationlanguagespecification", new[] { "languageid" });
            DropIndex("organizationlanguagespecification", new[] { "organizationid" });
            DropIndex("organization", new[] { "streetaddressid" });
            DropIndex("organization", new[] { "phonenumberid" });
            DropIndex("organization", new[] { "emailaddressid" });
            DropIndex("organization", new[] { "parentorganizationid" });
            DropIndex("organization", new[] { "providertypeid" });
            DropIndex("addresslanguagespecification", new[] { "languageid" });
            DropIndex("addresslanguagespecification", new[] { "addressid" });
            DropTable("organization_webaddress");
            DropTable("organization_postaladdress");
            DropTable("webaddress");
            DropTable("providertype");
            DropTable("phonenumber");
            DropTable("organizationlanguagespecification");
            DropTable("organization");
            DropTable("emailaddress");
            DropTable("language");
            DropTable("addresslanguagespecification");
            DropTable("address");
        }
    }
}