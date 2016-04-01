using Affecto.Patterns.Cqrs.Autofac;
using Autofac;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Application.Settings;
using OrganizationRegister.Application.Validation;
using OrganizationRegister.Store.CodeFirst;

namespace OrganizationRegister.Autofac
{
    public class OrganizationRegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<CqrsModule>();
            RegisterServices(builder);
            RegisterRepositories(builder);
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>();
            builder.RegisterType<SettingsRepository>().As<ISettingsRepository>();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<SettingsService>().As<ISettingsService>();
            builder.RegisterType<OrganizationService>().As<IOrganizationService>();
            builder.RegisterType<ValidationService>().As<IValidationService>();
        }
    }
}