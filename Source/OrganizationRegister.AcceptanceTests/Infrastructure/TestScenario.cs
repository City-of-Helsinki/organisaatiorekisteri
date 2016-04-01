using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Autofac;
using OrganizationRegister.Store.CodeFirst;
using OrganizationRegister.Store.CodeFirst.Mocking;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Infrastructure
{
    [Binding]
    internal static class TestScenario
    {
        [BeforeScenario]
        public static void Setup()
        {
            var builder = new ContainerBuilder();
            RegisterProductionCodeModules(builder);
            SetupMockRepositories(builder);
            BuildContainer(builder);
        }

        [AfterScenario]
        public static void TearDown()
        {
            List<Exception> exceptions = ScenarioContext.Current.Where(pair => pair.Value is Exception).Select(pair => (Exception)pair.Value).ToList();

            if (exceptions.Any())
            {
                string[] exceptionMessages = exceptions.Select(FormatExceptionMessage).ToArray();
                string exceptionMessage = string.Join(Environment.NewLine, exceptionMessages);
                string exceptionStackTrace = exceptions.First().StackTrace;
                Assert.Fail("Unhandled exception was thrown in scenario:{0}{1}{2}{3}", Environment.NewLine, exceptionMessage, Environment.NewLine, exceptionStackTrace);
            }
        }

        private static string FormatExceptionMessage(Exception e)
        {
            return string.Format("{0}: {1}", e.GetType().FullName, e.Message);
        }

        private static void SetupMockRepositories(ContainerBuilder builder)
        {
            var dbContext = new MockDbContext(false);
            builder.RegisterInstance(dbContext).As<IStoreContext>();
            MockRepository repository = new MockRepository(dbContext);
            ScenarioContext.Current.Set(repository);
        }

        private static void RegisterProductionCodeModules(ContainerBuilder builder)
        {
            builder.RegisterModule<OrganizationRegisterModule>();
        }

        private static void BuildContainer(ContainerBuilder builder)
        {
            IContainer container = builder.Build();
            ScenarioContext.Current.Set(container);
        }
    }
}