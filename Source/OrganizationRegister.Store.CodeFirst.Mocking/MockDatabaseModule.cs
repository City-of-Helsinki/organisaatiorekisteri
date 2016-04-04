using Autofac;

namespace OrganizationRegister.Store.CodeFirst.Mocking
{
    public class MockDatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MockDbContext>().SingleInstance().As<IStoreContext>();
        }
    }
}