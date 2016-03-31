using System;

namespace OrganizationRegister.Application.Classification
{
    public interface IClass
    {
        Guid Id { get; }
        string Name { get; }
    }
}
