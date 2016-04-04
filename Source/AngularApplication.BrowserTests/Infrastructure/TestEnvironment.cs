using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using Affecto.Authentication.Claims;
using Affecto.Testing.UI.Selenium;
using Autofac;
using OpenQA.Selenium.Support.Extensions;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Autofac;
using OrganizationRegister.Common;
using OrganizationRegister.Common.User;
using OrganizationRegister.Store.CodeFirst;
using OrganizationRegister.Tests.Infrastructure;
using OrganizationRegister.UserManagement;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AngularApplication.BrowserTests.Infrastructure
{
    [Binding]
    internal static class TestEnvironment
    {
        public static readonly string BasicTestUserEmailAddress = "basic@test.net";
        public static readonly string OrganizationAdministratorTestUserEmailAddress = "organizationAdministrator@test.net";
        public static readonly string AdministratorTestUserEmailAddress = "administrator@test.net";
        public static readonly string TestUserPassword = "secretPassword!";
        public static readonly string ApplicationHomePage = ConfigurationManager.AppSettings.Get("applicationUrl");

        public static WebHostDriver Driver { get; private set; }

        private static readonly string Browser = ConfigurationManager.AppSettings.Get("browser");
        private static readonly string FailedTestFolder = ConfigurationManager.AppSettings.Get("failedTestFolder");
        private static readonly string ApplicationWebApiWarmUpUrl = ConfigurationManager.AppSettings.Get("applicationWebApiWarmUpUrl");
        private static readonly string AuthenticationWebApiWarmUpUrl = ConfigurationManager.AppSettings.Get("authenticationWebApiWarmUpUrl");
        private static readonly int DaysToKeepOldFailedTestData = int.Parse(ConfigurationManager.AppSettings.Get("daysToKeepOldFailedTestData"));
        
        private static IOrganizationService organizationService;
        private static UserManagementTestEnvironment userManagementTestEnvironment;
        private static TestUserContext userContext;
        private static IContainer container;

        [BeforeTestRun]
        public static void SetupTestRun()
        {
            ClearOldFailedTestData();
            WarmUpWebApi();
        }

        [BeforeScenario]
        public static void SetupScenario()
        {
            var builder = new ContainerBuilder();
            RegisterProductionCodeModules(builder);
            RegisterAuthenticatedUserContext(builder);
            ResolveProductionCodeModules(builder);
            ClearDatabase();
            CreateTestOrganizationAndUsers();
            SetupCurrentScenarioContext();
            Driver = new WebHostDriver(Browser);
        }

        [AfterScenario]
        public static void TearDownScenario()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                Driver.Value.TakeScreenshot().SaveAsFile(ScreenShotFileName, ImageFormat.Png);
                File.WriteAllText(HtmlFileName, Driver.Value.PageSource);
            }
            Driver.Dispose();
        }

        private static void SetupCurrentScenarioContext()
        {
            ScenarioContext.Current.Set(organizationService);
            ScenarioContext.Current.Set(container);
        }

        private static void ResolveProductionCodeModules(ContainerBuilder builder)
        {
            container = builder.Build();
            organizationService = container.Resolve<IOrganizationService>();
            userManagementTestEnvironment = container.Resolve<UserManagementTestEnvironment>();
            userContext = container.Resolve<TestUserContext>();
        }

        private static void WarmUpWebApi()
        {
            using (WebHostDriver driver = new WebHostDriver(Browser))
            {
                driver.NavigateTo(ApplicationWebApiWarmUpUrl);
            }
            using (WebHostDriver driver = new WebHostDriver(Browser))
            {
                driver.NavigateTo(AuthenticationWebApiWarmUpUrl);
            }
            Thread.Sleep(5000);
        }

        private static void ClearOldFailedTestData()
        {
            foreach (string fileName in Directory.GetFiles(FailedTestFolder))
            {
                DateTime lastModified = new FileInfo(fileName).LastWriteTime;
                if (lastModified <= DateTime.Today.AddDays(-1 * DaysToKeepOldFailedTestData))
                {
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch (Exception)
                    {
                        // deleting a file may fail for various reasons and it shouldn't cause testing to stop
                    }
                }
            }
        }

        private static void ClearDatabase()
        {
            foreach (IOrganizationName organization in organizationService.GetMainOrganizations())
            {
                organizationService.RemoveOrganization(organization.Id);
            }

            userManagementTestEnvironment.RemoveAllUsers();
        }

        private static void CreateTestOrganizationAndUsers()
        {
            Guid organizationId = organizationService.AddOrganization("6464032-2", null, "Valtio", null,
                new List<LocalizedText> { new LocalizedText("fi", "Testkäyttäjän organisaatio") }, null);

            userContext.Permissions = new List<string> { Permissions.Users.UserMaintenance, Permissions.Users.ManageAdministratorUsers };
            userManagementTestEnvironment.AddBasicTestUser(organizationId, BasicTestUserEmailAddress, TestUserPassword, "Basic", "User");
            userManagementTestEnvironment.AddOrganizationAdministratorTestUser(organizationId, OrganizationAdministratorTestUserEmailAddress, TestUserPassword, 
                "OrganizationAdmin", "User");
            userManagementTestEnvironment.AddAdministratorTestUser(organizationId, AdministratorTestUserEmailAddress, TestUserPassword, "Admin", "User");
            userContext.Permissions.Clear();
        }

        private static void RegisterProductionCodeModules(ContainerBuilder builder)
        {
            builder.RegisterModule<OrganizationRegisterModule>();
            builder.RegisterModule<EntityFrameworkModule>();
            builder.RegisterModule(new UserManagementModule(false));
        }

        private static void RegisterAuthenticatedUserContext(ContainerBuilder builder)
        {
            builder.RegisterType<TestUserContext>()
                .AsSelf()
                .As<IAuthenticatedUserContext>()
                .SingleInstance();
        }

        private static string ScreenShotFileName
        {
            get { return string.Format("{0}.png", FileNameBase); }
        }

        private static string HtmlFileName
        {
            get { return string.Format("{0}.html", FileNameBase); }
        }

        private static string FileNameBase
        {
            get { return string.Format("{0}{1}_{2}", FailedTestFolder, ScenarioContext.Current.ScenarioInfo.Title, DateTime.Now.ToString("yyyyMMddTHHmmss")); }
        }
    }
}