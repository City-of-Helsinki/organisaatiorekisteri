namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCallChargeType : OrganizationRegisterDbMigration
    {
        public override void Up()
        {
            CreateTable(
                "callchargetype",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        type = c.String(nullable: false),
                        ordernumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);

                AddChargeTypes();

            CreateTable(
                "phonenumberlanguagespecification",
                c => new
                    {
                        phonenumberid = c.Guid(nullable: false),
                        languageid = c.Guid(nullable: false),
                        callchargeinfo = c.String(),
                    })
                .PrimaryKey(t => new { t.phonenumberid, t.languageid })  
                .Index(t => t.phonenumberid)
                .Index(t => t.languageid);

                AddForeignKey("phonenumberlanguagespecification", "languageid", "availabledatalanguage", "languageid");
                AddForeignKey("phonenumberlanguagespecification", "phonenumberid", "phonenumber", "id");

                AddColumn("phonenumber", "chargetypeid", c => c.Guid(nullable: true));
                UpdatePhoneNumberChargeType();
                AlterColumn("phonenumber", "chargetypeid", c => c.Guid(nullable: false));
                CreateIndex("phonenumber", "chargetypeid");
                AddForeignKey("phonenumber", "chargetypeid", "callchargetype", "id");
                DropColumn("phonenumber", "phonecallfee");


                
        }
        
        public override void Down()
        {
            AddColumn("phonenumber", "phonecallfee", c => c.String());
            DropForeignKey("phonenumberlanguagespecification", "phonenumberid", "phonenumber");
            DropForeignKey("phonenumberlanguagespecification", "languageid", "availabledatalanguage");
            DropForeignKey("phonenumber", "chargetypeid", "callchargetype");
            DropIndex("phonenumberlanguagespecification", new[] { "languageid" });
            DropIndex("phonenumberlanguagespecification", new[] { "phonenumberid" });
            DropIndex("phonenumber", new[] { "chargetypeid" });
            DropColumn("phonenumber", "chargetypeid");
            DropTable("phonenumberlanguagespecification");
            DropTable("callchargetype");
        }

        private void AddChargeTypes()
        {
            Sql(string.Format("INSERT INTO {0}(id, type, ordernumber) VALUES('{1}','{2}','{3}' )",
                FormatTableNameWithSchemaNameAndQuotes("callchargetype"), Guid.NewGuid().ToString("D"), "Maksuton", 10));

            Sql(string.Format("INSERT INTO {0}(id, type, ordernumber) VALUES('{1}','{2}','{3}')",
                FormatTableNameWithSchemaNameAndQuotes("callchargetype"), Guid.NewGuid().ToString("D"), "Paikallisverkko-, paikallispuhelu- tai matkapuhelinmaksu", 20));

            Sql(string.Format("INSERT INTO {0}(id, type, ordernumber) VALUES('{1}','{2}','{3}')",
               FormatTableNameWithSchemaNameAndQuotes("callchargetype"), Guid.NewGuid().ToString("D"), "Muu", 30));
        }

        private void UpdatePhoneNumberChargeType()
        {
            Sql(string.Format("update {0} set chargetypeid = (select id from {1} where type='{2}')",
                FormatTableNameWithSchemaNameAndQuotes("phonenumber"), FormatTableNameWithSchemaNameAndQuotes("callchargetype"), "Maksuton"));

        }
    }
}
