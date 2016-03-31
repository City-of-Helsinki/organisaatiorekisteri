using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    internal interface IClass
    {
        Guid Id { get; }
        string Name { get; }
        string SourceId { get; }
        string SourceParentId { get; }
        int? OrderNumber { get; }
    }
}
