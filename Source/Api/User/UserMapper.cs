using System;
using Affecto.Mapping;
using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.Api.Settings;
using OrganizationRegister.Application.User;

namespace OrganizationRegister.Api.User
{
    public class UserMapper : OneWayMapper<IUser, User>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IUser, User>().ForMember(x => x.Password, opt => opt.Ignore()); ;
            
        }
    }
}