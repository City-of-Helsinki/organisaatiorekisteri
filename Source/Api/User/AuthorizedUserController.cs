using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Affecto.WebApi.Toolkit.CustomRoutes;
using OrganizationRegister.Api.Organization;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Application.User;

namespace OrganizationRegister.Api.User
{
    [RoutePrefix("v1/serviceregister")]
    public class AuthorizedUserController : ApiController
    {
        private readonly IUserService userService;
        private readonly Lazy<IOrganizationService> organizationService;
        private readonly MapperFactory mapperFactory;

        public AuthorizedUserController(IUserService userService, Lazy<IOrganizationService> organizationService, MapperFactory mapperFactory)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }
            if (organizationService == null)
            {
                throw new ArgumentNullException("organizationService");
            }
            if (mapperFactory == null)
            {
                throw new ArgumentNullException("mapperFactory");
            }

            this.userService = userService;
            this.organizationService = organizationService;
            this.mapperFactory = mapperFactory;
        }

        [HttpPost]
        [PostRoute("users/isexisting")]
        public IHttpActionResult IsExistingUser([FromBody] string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new ArgumentException("Email address cannot be empty.", "emailAddress");
            }

            bool isExistingUser = userService.IsExistingUser(emailAddress);
            return Ok(isExistingUser);
        }

        [HttpPost]
        [PostRoute("users/password")]
        public IHttpActionResult ValidatePasswordStrength([FromBody] string password)
        {
            bool isValid = userService.ValidatePasswordStrength(password);
            return Ok(isValid);
        }

        [HttpPost]
        [PostRoute("users")]
        public IHttpActionResult AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            Guid userId = userService.AddUser(user.RoleId, user.OrganizationId, user.EmailAddress, user.Password, user.LastName, user.FirstName, user.PhoneNumber);
            return Ok(userId);
        }

        [HttpGet]
        [GetRoute("organizations/{organizationId}/users")]
        public IHttpActionResult GetUsers(Guid organizationId)
        {
            if (organizationId == Guid.Empty)
            {
                throw new ArgumentException("organizationId");
            }

            IOrganizationName organization = organizationService.Value.GetOrganizationName(organizationId);
            List<IUserListItem> users = userService.GetUsers(organizationId).ToList();

            var organizationMapper = mapperFactory.CreateOrganizationNameMapper();
            var userMapper = mapperFactory.CreateUserListItemMapper();

            OrganizationName mappedOrganization = organizationMapper.Map(organization);
            List<UserListItem> results = new List<UserListItem>(users.Count);

            foreach (IUserListItem user in users)
            {
                UserListItem mappedUser = userMapper.Map(user);
                mappedUser.Organization = mappedOrganization;
                results.Add(mappedUser);
            }

            return Ok(results.AsEnumerable());
        }
    }
}