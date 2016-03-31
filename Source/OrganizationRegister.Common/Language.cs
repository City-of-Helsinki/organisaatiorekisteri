using System;

namespace OrganizationRegister.Common
{
    public class Language
    {
        public Language(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public string Code { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Language)
            {
                return GetHashCode().Equals(obj.GetHashCode());
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Code != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Code) : 0) * 397) ^ (Name != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Name) : 0);
            }
        }
    }
}