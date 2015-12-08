﻿using System;

namespace ServiceRegister.Store.CodeFirst.Model
{
    public class OrganizationLanguageSpecification
    {
        public Guid LanguageId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual AvailableDataLanguage Language { get; set; }
    }
}