using System.Data.Entity;
using Affecto.EntityFramework.PostgreSql;
using Autofac;

namespace OrganizationRegister.Store.CodeFirst
{
    public class EntityFrameworkModule : Module
    {
        public EntityFrameworkModule()
        {
            Database.SetInitializer<StoreContext>(null);
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(ctx => new StoreContext(PostgreSqlConfiguration.Settings.Schemas[StoreContext.ConfigurationKey])).As<IStoreContext>();
        }
    }
}