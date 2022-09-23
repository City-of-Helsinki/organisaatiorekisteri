using System;
using System.Collections.Generic;
using System.Web.Http;
using Affecto.Mapping;
using Affecto.WebApi.Toolkit.CustomRoutes;
using OrganizationRegister.Application.Settings;
using OrganizationRegister.Application.User;

namespace OrganizationRegister.Api.Settings
{
    [RoutePrefix("v1/organizationregister")]
    public class OpenSettingsController : ApiController
    {
        private readonly Lazy<ISettingsService> settingsService;
        private readonly Lazy<IUserService> userService;
        private readonly MapperFactory mapperFactory;

        public void W(object s)
        {
            Console.WriteLine("Console[" + this.GetType() + "][" + this.GetHashCode() + "] " + s);
            System.Diagnostics.Trace.WriteLine("Trace[" + this.GetType() + "][" + this.GetHashCode() + "] " + s);
            System.Diagnostics.Debug.WriteLine("Debug[" + this.GetType() + "][" + this.GetHashCode() + "] " + s);
        }

        public OpenSettingsController(Lazy<ISettingsService> settingsService, Lazy<IUserService> userService, MapperFactory mapperFactory)
        {
            W("OpenSettingsController");

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

            W("settingsService: " + settingsService.GetType());
            W("userService: " + userService.GetType());
            W("mapperFactory: " + mapperFactory.GetType());

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
            W("GetRoles");

           IEnumerable<IRole> roles = null;
           IMapper <IRole, Role> mapper = null;

            try
            {
                roles = userService.Value.GetRoles();
                mapper = mapperFactory.CreateRoleMapper();
            } catch (Exception ex)
            {
                W(ex);
                throw ex;
            }
  

            return Ok(mapper.Map(roles));
        }

        [HttpGet]
        [GetRoute("callchargetypes")]
        public IHttpActionResult GetPhoneCallCharggeTypes()
        {
            IEnumerable<string> types = settingsService.Value.GetCallChargeTypes();
            return Ok(types);
        }
    }
}