using System;
using Affecto.Authentication.Claims;

namespace OrganizationRegister.Common.User
{
    public static class AuthenticationExtensionMethods
    {
        public static Guid GetUserOrganizationId(this IAuthenticatedUserContext context)
        {
            string propertyName = CustomPropertyName.OrganizationId.ToString();
            if (context.HasCustomProperty(propertyName))
            {
                string value = context.GetCustomPropertyValue(propertyName);

                Guid organizationId;
                if (OrganizationId.TryConvert(value, out organizationId))
                {
                    return organizationId;
                }
            }

            throw new RequiredCustomPropertyMissingException("Organization id is missing from authenticated user context.");
        }
    }
}