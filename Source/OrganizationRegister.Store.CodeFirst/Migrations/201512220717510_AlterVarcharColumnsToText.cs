using System.Data.Entity.Migrations;

namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class AlterVarcharColumnsToText : DbMigration
    {
        public override void Up()
        {
            AlterColumn("address", "postalcode", c => c.String());
            AlterColumn("address", "postofficebox", c => c.String());
            AlterColumn("addresslanguagespecification", "streetaddress", c => c.String());
            AlterColumn("addresslanguagespecification", "postaldistrict", c => c.String());
            AlterColumn("addresslanguagespecification", "qualifier", c => c.String());
            AlterColumn("language", "code", c => c.String(nullable: false));
            AlterColumn("language", "name", c => c.String(nullable: false));
            AlterColumn("emailaddress", "email", c => c.String(nullable: false));
            AlterColumn("lifeevent", "name", c => c.String());
            AlterColumn("lifeevent", "sourceid", c => c.String());
            AlterColumn("lifeevent", "sourceparentid", c => c.String());
            AlterColumn("ontologyterm", "name", c => c.String());
            AlterColumn("ontologyterm", "lowercasename", c => c.String());
            AlterColumn("ontologyterm", "sourceid", c => c.String());
            AlterColumn("ontologyterm", "sourceparentid", c => c.String());
            AlterColumn("organization", "businessid", c => c.String());
            AlterColumn("organization", "oid", c => c.String());
            AlterColumn("organizationlanguagespecification", "name", c => c.String());
            AlterColumn("organizationlanguagespecification", "description", c => c.String());
            AlterColumn("phonenumber", "number", c => c.String(nullable: false));
            AlterColumn("phonenumber", "phonecallfee", c => c.String());
            AlterColumn("organizationtype", "name", c => c.String(nullable: false));
            AlterColumn("organizationtype", "sourceid", c => c.String());
            AlterColumn("webpage", "name", c => c.String(nullable: false));
            AlterColumn("webpage", "url", c => c.String(nullable: false));
            AlterColumn("webpagetype", "type", c => c.String(nullable: false));
            AlterColumn("serviceclass", "name", c => c.String());
            AlterColumn("serviceclass", "sourceid", c => c.String());
            AlterColumn("serviceclass", "sourceparentid", c => c.String());
            AlterColumn("servicekeyword", "keyword", c => c.String(nullable: false));
            AlterColumn("servicelanguagespecification", "name", c => c.String());
            AlterColumn("servicelanguagespecification", "alternatename", c => c.String());
            AlterColumn("servicelanguagespecification", "description", c => c.String());
            AlterColumn("servicelanguagespecification", "shortdescription", c => c.String());
            AlterColumn("servicelanguagespecification", "userinstructions", c => c.String());
            AlterColumn("servicelanguagespecification", "requirements", c => c.String());
            AlterColumn("targetgroup", "name", c => c.String());
            AlterColumn("targetgroup", "sourceid", c => c.String());
            AlterColumn("targetgroup", "sourceparentid", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("targetgroup", "sourceparentid", c => c.String(maxLength: 200));
            AlterColumn("targetgroup", "sourceid", c => c.String(maxLength: 200));
            AlterColumn("targetgroup", "name", c => c.String(maxLength: 200));
            AlterColumn("servicelanguagespecification", "requirements", c => c.String(maxLength: 4000));
            AlterColumn("servicelanguagespecification", "userinstructions", c => c.String(maxLength: 4000));
            AlterColumn("servicelanguagespecification", "shortdescription", c => c.String(maxLength: 150));
            AlterColumn("servicelanguagespecification", "description", c => c.String(maxLength: 4000));
            AlterColumn("servicelanguagespecification", "alternatename", c => c.String(maxLength: 100));
            AlterColumn("servicelanguagespecification", "name", c => c.String(maxLength: 100));
            AlterColumn("servicekeyword", "keyword", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("serviceclass", "sourceparentid", c => c.String(maxLength: 200));
            AlterColumn("serviceclass", "sourceid", c => c.String(maxLength: 200));
            AlterColumn("serviceclass", "name", c => c.String(maxLength: 200));
            AlterColumn("webpagetype", "type", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("webpage", "url", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("webpage", "name", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("organizationtype", "sourceid", c => c.String(maxLength: 200));
            AlterColumn("organizationtype", "name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("phonenumber", "phonecallfee", c => c.String(maxLength: 150));
            AlterColumn("phonenumber", "number", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("organizationlanguagespecification", "description", c => c.String(maxLength: 4000));
            AlterColumn("organizationlanguagespecification", "name", c => c.String(maxLength: 500));
            AlterColumn("organization", "oid", c => c.String(maxLength: 100));
            AlterColumn("organization", "businessid", c => c.String(maxLength: 9));
            AlterColumn("ontologyterm", "sourceparentid", c => c.String(maxLength: 200));
            AlterColumn("ontologyterm", "sourceid", c => c.String(maxLength: 200));
            AlterColumn("ontologyterm", "lowercasename", c => c.String(maxLength: 200));
            AlterColumn("ontologyterm", "name", c => c.String(maxLength: 200));
            AlterColumn("lifeevent", "sourceparentid", c => c.String(maxLength: 200));
            AlterColumn("lifeevent", "sourceid", c => c.String(maxLength: 200));
            AlterColumn("lifeevent", "name", c => c.String(maxLength: 200));
            AlterColumn("emailaddress", "email", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("language", "name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("language", "code", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("addresslanguagespecification", "qualifier", c => c.String(maxLength: 500));
            AlterColumn("addresslanguagespecification", "postaldistrict", c => c.String(maxLength: 100));
            AlterColumn("addresslanguagespecification", "streetaddress", c => c.String(maxLength: 200));
            AlterColumn("address", "postofficebox", c => c.String(maxLength: 100));
            AlterColumn("address", "postalcode", c => c.String(maxLength: 100));
        }
    }
}
