using System;
using System.Collections.Generic;
using System.Web.Http;
using Affecto.Mapping;
using Affecto.WebApi.Toolkit.CustomRoutes;
using OrganizationRegister.Application.Settings;
using OrganizationRegister.Application.User;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Settings
{
    [RoutePrefix("v1/serviceregister")]
    public class OpenSettingsController : ApiController
    {
        private readonly Lazy<ISettingsService> settingsService;
        private readonly Lazy<IUserService> userService;
        private readonly MapperFactory mapperFactory;

        public OpenSettingsController(Lazy<ISettingsService> settingsService, Lazy<IUserService> userService, MapperFactory mapperFactory)
        {
            if (settingsService == null)
            {
                throw new ArgumentNullException("settingsService");
            }
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }
            if (mapperFactory == null)
            {
                throw new ArgumentNullException("mapperFactory");
            }

            this.settingsService = settingsService;
            this.userService = userService;
            this.mapperFactory = mapperFactory;
        }

        [HttpGet]
        [GetRoute("organizationtypes")]
        public IHttpActionResult GetOrganizationTypes()
        {
            IEnumerable<string> organizationTypes = settingsService.Value.GetOrganizationTypes();
            return Ok(organizationTypes);
        }

        [HttpGet]
        [GetRoute("webpagetypes")]
        public IHttpActionResult GetWebPageTypes()
        {
            IEnumerable<string> webPageTypes = settingsService.Value.GetWebPageTypes();
            return Ok(webPageTypes);
        }

        [HttpGet]
        [GetRoute("roles")]
        public IHttpActionResult GetRoles()
        {
            IEnumerable<IRole> roles = userService.Value.GetRoles();
            IMapper<IRole, Role> mapper = mapperFactory.CreateRoleMapper();

            return Ok(mapper.Map(roles));
        }

        [HttpGet]
        [GetRoute("servicelanguages")]
        public IHttpActionResult GetServiceLanguages()
        {
            var mapper = mapperFactory.CreateLanguageMapper();
            IEnumerable<Language> serviceLanguages = mapper.Map(settingsService.Value.GetServiceLanguages());
            return Ok(serviceLanguages);
        }
    }
}