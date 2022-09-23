using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Helpers;
using System.Web.Http;
using Affecto.Logging;
using Affecto.Logging.Log4Net;
using Autofac;
using Autofac.Integration.WebApi;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Serialization;
using OrganizationRegister.Api;
using OrganizationRegister.Api.Authentication;
using OrganizationRegister.Autofac;
using OrganizationRegister.Store.CodeFirst;
using OrganizationRegister.Store.CodeFirst.Mocking;
using OrganizationRegister.UserManagement;
using Owin;
using System;

[assembly: OwinStartup(typeof(Startup))]
namespace OrganizationRegister.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {




            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
               (se, cert, chain, sslerror) =>
               {
                   return true;
               };


            var builder = new ContainerBuilder();

            builder.RegisterModule<WebApiModule>();
            builder.RegisterModule<OrganizationRegisterModule>();

            bool useMockDatabase = (ConfigurationManager.AppSettings["useMockDatabase"] == "true");
            if (useMockDatabase)
            {
                builder.RegisterModule<MockDatabaseModule>();
            }
            else
            {
                builder.RegisterModule<EntityFrameworkModule>();
            }

            builder.RegisterModule(new UserManagementModule(useMockDatabase));
            builder.RegisterType<Log4NetLoggerFactory>().As<ILoggerFactory>();

            IContainer container = builder.Build();
            IAuthenticationServerConfiguration authenticationServerConfiguration = container.Resolve<IAuthenticationServerConfiguration>();

            HttpConfiguration config = new HttpConfiguration
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(container)
            };

            ConfigureWebApi(config);
            app.UseAutofacWebApi(config);
//#if(DEBUG)
            app.UseCors(CorsOptions.AllowAll);
//#endif

            AntiForgeryConfig.UniqueClaimTypeIdentifier = "sub";
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            IdentityServerBearerTokenAuthenticationOptions options = IdentityServerOptionsFactory.Create(authenticationServerConfiguration);
            app.UseIdentityServerBearerTokenAuthentication(options);

            app.UseWebApi(config);

            config.EnsureInitialized();
        }

        private static void ConfigureWebApi(HttpConfiguration config)
        {
            W("ConfigureWebApi");
            config.MapHttpAttributeRoutes();

            JsonMediaTypeFormatter jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public static void W(object s)
        {
            System.Diagnostics.Trace.WriteLine("Trace[OrganizationRegister.Api.Startup] " + s);
            System.Diagnostics.Debug.WriteLine("Debug[OrganizationRegister.Api.Startup]  " + s);
            Console.WriteLine("Console[OrganizationRegister.Api.Startup] " + s);
        }
    }
}