using Affecto.EntityFramework.PostgreSql;
using Affecto.EntityFramework.PostgreSql.Configuration;

namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    public abstract class OrganizationRegisterDbMigration : PostgreSqlDbMigration
    {
        protected override string ResolveSchemaName()
        {
            return PostgreSqlConfiguration.Settings.Schemas[StoreContext.ConfigurationKey];
        }
    }
}