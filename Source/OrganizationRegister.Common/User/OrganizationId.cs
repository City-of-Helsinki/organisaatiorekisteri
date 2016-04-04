using System;

namespace OrganizationRegister.Common.User
{
    public class OrganizationId
    {
        private const string OrganizationIdFormat = "D";

        public static bool IsValid(string value)
        {
            Guid result;
            return TryConvert(value, out result);
        }

        public static string Convert(Guid value)
        {
            string result;
            if (TryConvert(value, out result))
            {
                return result;
            }

            throw new ArgumentException("Organization id cannot be an empty Guid.");
        }

        public static bool TryConvert(Guid value, out string result)
        {
            if (value == Guid.Empty)
            {
                result = null;
                return false;
            }

            result = value.ToString(OrganizationIdFormat);
            return true;
        }

        public static Guid Convert(string value)
        {
            Guid result;
            if (TryConvert(value, out result))
            {
                return result;
            }

            throw new FormatException("Organization id is of invalid format.");
        }

        public static bool TryConvert(string value, out Guid result)
        {
            return Guid.TryParseExact(value, OrganizationIdFormat, out result);
        }
    }
}