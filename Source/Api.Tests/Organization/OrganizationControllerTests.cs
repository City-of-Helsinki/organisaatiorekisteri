using Affecto.Mapping;
using NSubstitute;
using OrganizationRegister.Api.Organization;
using OrganizationRegister.Application.Organization;

namespace OrganizationRegister.Api.Tests.Organization
{
    public abstract class OrganizationControllerTests
    {
        protected MapperFactory mapperFactory;
        protected IMapper<IHierarchicalOrganization, HierarchicalOrganization> hierarchicalOrganizationMapper;
        protected IMapper<IOrganization, Api.Organization.Organization> organizationMapper;
        protected IMapper<IOrganizationName, OrganizationName> organizationNameMapper;

        protected void SetupMappers()
        {
            mapperFactory = Substitute.For<MapperFactory>();
            hierarchicalOrganizationMapper = Substitute.For<IMapper<IHierarchicalOrganization, HierarchicalOrganization>>();
            organizationMapper = Substitute.For<IMapper<IOrganization, Api.Organization.Organization>>();
            organizationNameMapper = Substitute.For<IMapper<IOrganizationName, OrganizationName>>();
            mapperFactory.CreateHierarchicalOrganizationMapper().Returns(hierarchicalOrganizationMapper);
            mapperFactory.CreateOrganizationMapper().Returns(organizationMapper);
            mapperFactory.CreateOrganizationNameMapper().Returns(organizationNameMapper);
        }
    }
}