using Affecto.EntityFramework.PostgreSql;

namespace OrganizationRegister.Store.CodeFirst.Migrations
{
    internal sealed class Configuration : HistoryContextDbMigrationsConfiguration<StoreContext>
    {
        public Configuration()
            : base(PostgreSqlConfiguration.Settings.Schemas[StoreContext.ConfigurationKey])
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}