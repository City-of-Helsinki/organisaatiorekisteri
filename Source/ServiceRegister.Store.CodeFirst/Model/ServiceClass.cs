﻿using System;

namespace ServiceRegister.Store.CodeFirst.Model
{
    public class ServiceClass : IClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SourceId { get; set; }
        public string SourceParentId { get; set; }
        public int? OrderNumber { get; set; }
    }
}
