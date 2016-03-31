using Affecto.Patterns.Cqrs.Autofac;
using Autofac;
using OrganizationRegister.Application.Classification;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Application.Service;
using OrganizationRegister.Application.Settings;
using OrganizationRegister.Application.Validation;
using OrganizationRegister.Commanding.Service.CommandHandlers;
using OrganizationRegister.Store.CodeFirst;

namespace OrganizationRegister.Autofac
{
    public class ServiceRegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<CqrsModule>();
            RegisterServices(builder);
            RegisterRepositories(builder);
            RegisterCommandHandlers(builder);
        }

        private void RegisterCommandHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<SetServiceClassificationHandler>().AsImplementedInterfaces();
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>();
            builder.RegisterType<SettingsRepository>().As<ISettingsRepository>();
            builder.RegisterType<ServiceRepository>().As<IServiceRepository>();
            builder.RegisterType<ClassificationRepository>().As<IClassificationRepository>();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<SettingsService>().As<ISettingsService>();
            builder.RegisterType<OrganizationService>().As<IOrganizationService>();
            builder.RegisterType<ValidationService>().As<IValidationService>();
            builder.RegisterType<ServiceService>().As<IServiceService>();
        }
    }
}