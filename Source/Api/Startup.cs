﻿using System.Collections.Generic;
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
using Owin;
using ServiceRegister.Api;
using ServiceRegister.Api.Authentication;
using ServiceRegister.Autofac;
using ServiceRegister.Store.CodeFirst;
using ServiceRegister.Store.CodeFirst.Mocking;
using ServiceRegister.UserManagement;

[assembly: OwinStartup(typeof(Startup))]
namespace ServiceRegister.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<WebApiModule>();
            builder.RegisterModule<ServiceRegisterModule>();

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
#if(DEBUG)
            app.UseCors(CorsOptions.AllowAll);
#endif

            AntiForgeryConfig.UniqueClaimTypeIdentifier = "sub";
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            IdentityServerBearerTokenAuthenticationOptions options = IdentityServerOptionsFactory.Create(authenticationServerConfiguration);
            app.UseIdentityServerBearerTokenAuthentication(options);

            app.UseWebApi(config);

            config.EnsureInitialized();
        }

        private static void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            JsonMediaTypeFormatter jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}