namespace OrganizationRegister.Common
{
    public class WebPage
    {
        public WebPage()
        {
        }

        public WebPage(string name, string address, string type)
        {
            Name = name;
            Address = address;
            Type = type;
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is WebPage)
            {
                return Equals(obj as WebPage);
            }
            return false;
        }
        protected bool Equals(WebPage other)
        {
            return GetHashCode() == other.GetHashCode();
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Address != null ? Address.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Type != null ? Type.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}