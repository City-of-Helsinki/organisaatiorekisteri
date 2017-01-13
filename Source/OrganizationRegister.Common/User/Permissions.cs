namespace OrganizationRegister.Common.User
{
    public static class Permissions
    {
        public static class Users
        {
            public const string MaintenanceOfOwnOrganizationUsers = "MAINTENANCE_OF_OWN_ORGANIZATION_USERS";
            public const string MaintenanceOfAllUsers = "MAINTENANCE_OF_ALL_USERS";
        }


        public static class Organization
        {
            public const string MaintenanceOfOwnOrganizationData = "MAINTENANCE_OF_OWN_ORGANIZATION_DATA";
            public const string MaintenanceOfAllOrganizationData = "MAINTENANCE_OF_ALL_ORGANIZATION_DATA";
        }
    }
}