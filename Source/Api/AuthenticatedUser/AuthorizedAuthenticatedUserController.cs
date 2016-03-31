using System;
using System.Security.Claims;
using System.Web.Http;
using Affecto.WebApi.Toolkit.CustomRoutes;

namespace OrganizationRegister.Api.AuthenticatedUser
{
    [RoutePrefix("v1/serviceregister")]
    public class AuthorizedAuthenticatedUserController : ApiController
    {
        [HttpGet]
        [GetRoute("authenticateduser")]
        public IHttpActionResult GetAuthenticatedUser()
        {
            if (RequestContext.Principal == null)
            {
                throw new AuthenticatedUserNotResolvedException("Error while resolving authenticated user. Principal is null.");
            }

            ClaimsIdentity identity = RequestContext.Principal.Identity as ClaimsIdentity;

            if (identity == null)
            {
                throw new AuthenticatedUserNotResolvedException("Error while resolving authenticated user. Identity is null.");
            }
            if (!identity.IsAuthenticated)
            {
                throw new AuthenticatedUserNotResolvedException("Error while resolving authenticated user. Identity is not authenticated.");
            }

            try
            {
                var userFactory = new AuthenticatedUserFactory(identity.Claims);
                AuthenticatedUser user = userFactory.Create();

                return Ok(user);
            }
            catch (Exception e)
            {
                throw new AuthenticatedUserNotResolvedException("Error while resolving authenticated user.", e);
            }
        }
    }
}