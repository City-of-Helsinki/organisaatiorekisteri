using System;
using Affecto.Mapping;
using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.Api.Settings;
using OrganizationRegister.Application.User;

namespace OrganizationRegister.Api.User
{
    public class UserListItemMapper : OneWayMapper<IUserListItem, UserListItem>
    {
        private readonly IMapper<IRole, Role> roleMapper;

        public UserListItemMapper(IMapper<IRole, Role> roleMapper)
        {
            if (roleMapper == null)
            {
                throw new ArgumentNullException("roleMapper");
            }
            this.roleMapper = roleMapper;
        }

        public override UserListItem Map(IUserListItem source)
        {
            UserListItem destination = base.Map(source);
            if (source != null && destination != null)
            {
                destination.Role = MapRole(source);
            }
            return destination;
        }

        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IUserListItem, UserListItem>()
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Organization, opt => opt.Ignore());
        }

        private Role MapRole(IUserListItem user)
        {
            return roleMapper.Map(user.Role);
        }
    }
}