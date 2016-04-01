using System;

namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public partial class AddWebPageType : ServiceRegisterDbMigration
    {
        public override void Up()
        {
            CreateTable(
                "webpagetype",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        type = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.id);
            AddColumn("webpage", "typeid", c => c.Guid(nullable: true));
            Guid guid = Guid.NewGuid();
            AddPageType("Kotisivu", guid);
            AddPageType("Sosiaalisen median palvelu", Guid.NewGuid());
            UpdateColumnValues(guid);
            AlterColumn("webpage", "typeid", c => c.Guid(nullable: false));
            CreateIndex("webpage", "typeid");
            AddForeignKey("webpage", "typeid", "webpagetype", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("webpage", "typeid", "webpagetype");
            DropIndex("webpage", new[] { "typeid" });
            DropColumn("webpage", "typeid");
            DropTable("webpagetype");
        }

        private void AddPageType(string webPageType, Guid guid)
        {
            string webPageId = guid.ToString("D");
            Sql(string.Format("INSERT INTO {0} (id, type) VALUES('{1}', '{2}')", FormatTableNameWithSchemaNameAndQuotes("webpagetype"), webPageId, webPageType));
        }

        private void UpdateColumnValues(Guid guid)
        {
            Sql(string.Format("UPDATE {0} SET typeid = '{1}';", FormatTableNameWithSchemaNameAndQuotes("webpage"), guid));
        }
    }
}