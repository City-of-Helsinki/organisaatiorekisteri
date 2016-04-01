using Affecto.EntityFramework.PostgreSql;

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