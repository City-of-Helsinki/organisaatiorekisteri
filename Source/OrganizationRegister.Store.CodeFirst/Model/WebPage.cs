using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class WebPage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Guid TypeId { get; set; }
        public virtual WebPageType Type { get; set; }
    }
}