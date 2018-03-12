using System;
using System.Collections.Generic;

namespace OrganizationRegister.Application.User
{
    public interface IGroupService
    {
        Guid AddGroup(string name);
        Guid? GetGroupId(string name);

    }
}