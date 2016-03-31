using System;
using OrganizationRegister.Application.Classification;

namespace OrganizationRegister.Application.Service
{
    public interface IService : IBasicInformation, IClassification
    {
        Guid Id { get; }
        long NumericId { get; }
    }
}
