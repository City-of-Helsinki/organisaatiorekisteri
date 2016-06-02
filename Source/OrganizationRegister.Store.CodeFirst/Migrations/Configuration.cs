using Affecto.EntityFramework.PostgreSql;
using Affecto.EntityFramework.PostgreSql.Configuration;

namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    internal sealed class Configuration : PostgreSqlDbMigrationsConfiguration<StoreContext>
    {
        public Configuration()
            : base(PostgreSqlConfiguration.Settings.Schemas[StoreContext.ConfigurationKey])
        {
        }
    }
}