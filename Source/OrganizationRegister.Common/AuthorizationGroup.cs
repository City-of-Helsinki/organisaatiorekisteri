using System;


namespace OrganizationRegister.Common
{ 

    public class AuthorizationGroup 
    {

        public AuthorizationGroup()
        {
        }

        public AuthorizationGroup(string name, Guid roleId, Guid? groupId)
        {
            Name = name;
            RoleId = roleId;
            GroupId = groupId;
        }

        public string Name { get; set; }
        public Guid RoleId { get; set; }
        public Guid? GroupId { get; set; }

        //PM 20.4.2021
        public Guid? OrganizationId { get; set; }

    }

}