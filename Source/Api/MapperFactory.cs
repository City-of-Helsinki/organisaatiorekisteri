using Affecto.Mapping;
using OrganizationRegister.Api.Classification;
using OrganizationRegister.Api.Organization;
using OrganizationRegister.Api.Service;
using OrganizationRegister.Api.Settings;
using OrganizationRegister.Api.User;
using OrganizationRegister.Api.Validation;
using OrganizationRegister.Application.Classification;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Application.Service;
using OrganizationRegister.Application.Settings;
using OrganizationRegister.Application.User;
using OrganizationRegister.Application.Validation;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api
{
    public class MapperFactory
    {
        public virtual IMapper<IHierarchicalOrganization, HierarchicalOrganization> CreateHierarchicalOrganizationMapper()
        {
            return new HierarchicalOrganizationMapper();
        }

        public virtual IMapper<IOrganizationName, OrganizationName> CreateOrganizationNameMapper()
        {
            return new OrganizationNameMapper();
        }

        public virtual IMapper<IBusinessIdentifierValidationResult, BusinessIdentifierValidationResult> CreateBusinessIdentifierValidationResultMapper()
        {
            return new BusinessIdentifierValidationResultMapper();
        }

        public virtual IMapper<IOrganization, Organization.Organization> CreateOrganizationMapper()
        {
            return new OrganizationMapper();
        }

        public virtual IMapper<IServiceListItem, ServiceListItem> CreateServiceNameMapper()
        {
            return new ServiceListItemMapper();
        }

        public virtual IMapper<IService, Service.Service> CreateServiceMapper()
        {
            return new ServiceMapper();
        }

        public virtual IMapper<IRole, Role> CreateRoleMapper()
        {
            return new RoleMapper();
        }

        public virtual IMapper<ILanguage, Language> CreateLanguageMapper()
        {
            return new LanguageMapper();
        }

        public virtual IMapper<IHierarchicalClass, HierarchicalClass> CreateHierarchicalClassMapper()
        {
            return new HierarchicalClassMapper();
        }

        public virtual IMapper<IUserListItem, UserListItem> CreateUserListItemMapper()
        {
            return new UserListItemMapper(CreateRoleMapper());
        }

        public virtual IMapper<IClass, Class> CreateClassMapper()
        {
            return new ClassMapper();
        }
    }
}