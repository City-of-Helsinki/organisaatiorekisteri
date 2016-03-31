using System;
using Affecto.Patterns.Cqrs;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Application.Classification;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Application.Service;
using OrganizationRegister.Application.Settings;
using OrganizationRegister.Application.Validation;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Infrastructure
{
    internal class StepDefinition
    {
        private const string ServiceRegisterException = "IdentityManagementServiceException";

        private IOrganizationService organizationService;
        private ISettingsService settingsService;
        private IValidationService validationService;
        private IServiceService serviceService;
        private IClassificationRepository classificationRepository;
        private ICommandBus commandBus;

        protected static MockRepository Repository
        {
            get { return Get<MockRepository>(); }
        }

        protected IOrganizationService OrganizationService
        {
            get
            {
                if (organizationService == null)
                {
                    IContainer container = Get<IContainer>();
                    organizationService = container.Resolve<IOrganizationService>();
                }
                return organizationService;
            }
        }

        protected ISettingsService SettingsService
        {
            get
            {
                if (settingsService == null)
                {
                    IContainer container = Get<IContainer>();
                    settingsService = container.Resolve<ISettingsService>();
                }
                return settingsService;
            }
        }

        protected IValidationService ValidationService
        {
            get
            {
                if (validationService == null)
                {
                    IContainer container = Get<IContainer>();
                    validationService = container.Resolve<IValidationService>();
                }
                return validationService;
            }
        }

        protected IServiceService ServiceService
        {
            get
            {
                if (serviceService == null)
                {
                    IContainer container = Get<IContainer>();
                    serviceService = container.Resolve<IServiceService>();
                }
                return serviceService;
            }
        }

        protected IClassificationRepository ClassificationRepository
        {
            get
            {
                if (classificationRepository == null)
                {
                    IContainer container = Get<IContainer>();
                    classificationRepository = container.Resolve<IClassificationRepository>();
                }
                return classificationRepository;
            }
        }

        protected void SendCommand(ICommand command)
        {
            CommandBus.Send(Envelope.Create(command));
        }

        protected static void Try(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                ScenarioContext.Current.Add(ServiceRegisterException, e);
            }
        }

        protected static void AssertCaughtException<TException>() where TException : Exception
        {
            Assert.IsTrue(ScenarioContext.Current.Get<Exception>(ServiceRegisterException) is TException);
            ScenarioContext.Current.Remove(ServiceRegisterException);
        }

        private ICommandBus CommandBus
        {
            get
            {
                if (commandBus == null)
                {
                    IContainer container = Get<IContainer>();
                    commandBus = container.Resolve<ICommandBus>();
                }
                return commandBus;
            }
        }
        
        private static T Get<T>()
        {
            return ScenarioContext.Current.Get<T>();
        }
    }
}
