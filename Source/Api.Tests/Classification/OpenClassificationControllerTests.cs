using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Affecto.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Classification;
using OrganizationRegister.Application.Classification;

namespace OrganizationRegister.Api.Tests.Classification
{
    [TestClass]
    public class OpenClassificationControllerTests
    {
        private OpenClassificationController sut;
        private MapperFactory mapperFactory;
        private IMapper<IHierarchicalClass, HierarchicalClass> hierarchicalClassMapper;
        private IMapper<IClass, Class> classMapper;
        private IClassificationRepository classificationRepository;

        [TestInitialize]
        public void Setup()
        {
            SetupMappers();
            classificationRepository = Substitute.For<IClassificationRepository>();
            sut = new OpenClassificationController(classificationRepository, mapperFactory);
        }

        private void SetupMappers()
        {
            hierarchicalClassMapper = Substitute.For<IMapper<IHierarchicalClass, HierarchicalClass>>();
            classMapper = Substitute.For<IMapper<IClass, Class>>();
            mapperFactory = Substitute.For<MapperFactory>();
            mapperFactory.CreateHierarchicalClassMapper().Returns(hierarchicalClassMapper);
            mapperFactory.CreateClassMapper().Returns(classMapper);
        }

        [TestMethod]
        public void GetLifeEventHierarchy()
        {
            IHierarchicalClass appLifeEvent = Substitute.For<IHierarchicalClass>();
            HierarchicalClass resultLifeEvent = new HierarchicalClass();
            classificationRepository.GetLifeEventHierarchy().Returns(new List<IHierarchicalClass> { appLifeEvent });
            hierarchicalClassMapper.Map(appLifeEvent).Returns(resultLifeEvent);

            var result = sut.GetLifeEventHierarchy() as OkNegotiatedContentResult<IEnumerable<HierarchicalClass>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(resultLifeEvent, result.Content.Single());
        }

        [TestMethod]
        public void GetServiceClassHierarchy()
        {
            IHierarchicalClass appServiceClass = Substitute.For<IHierarchicalClass>();
            HierarchicalClass resultClass = new HierarchicalClass();
            classificationRepository.GetServiceClassHierarchy().Returns(new List<IHierarchicalClass> { appServiceClass });
            hierarchicalClassMapper.Map(appServiceClass).Returns(resultClass);

            var result = sut.GetServiceClassHierarchy() as OkNegotiatedContentResult<IEnumerable<HierarchicalClass>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(resultClass, result.Content.Single());
        }

        [TestMethod]
        public void GetOntologyTermHierarchy()
        {
            IHierarchicalClass appTerm = Substitute.For<IHierarchicalClass>();
            HierarchicalClass resultTerm = new HierarchicalClass();
            classificationRepository.GetOntologyTermHierarchy().Returns(new List<IHierarchicalClass> { appTerm });
            hierarchicalClassMapper.Map(appTerm).Returns(resultTerm);

            var result = sut.GetOntologyTermHierarchy() as OkNegotiatedContentResult<IEnumerable<HierarchicalClass>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(resultTerm, result.Content.Single());
        }

        [TestMethod]
        public void GetFlatOntologyTermsWithoutMaxResults()
        {
            const string searchText = "yö";
            IClass appTerm = Substitute.For<IClass>();
            Class resultTerm = new Class();
            classificationRepository.GetFlatOntologyTerms(searchText).Returns(new List<IClass> { appTerm });
            classMapper.Map(appTerm).Returns(resultTerm);

            var result = sut.GetFlatOntologyTerms(searchText, null) as OkNegotiatedContentResult<ClassesSearchResult>;

            Assert.IsNotNull(result);
            Assert.AreEqual(resultTerm, result.Content.Classes.Single());
            Assert.AreEqual(searchText, result.Content.SearchText);
        }

        [TestMethod]
        public void GetFlatOntologyTermsWithMaxResults()
        {
            const string searchText = "yö";
            const int maxResults = 2;
            IClass appTerm = Substitute.For<IClass>();
            Class resultTerm = new Class();
            classificationRepository.GetFlatOntologyTerms(searchText, maxResults).Returns(new List<IClass> { appTerm });
            classMapper.Map(appTerm).Returns(resultTerm);

            var result = sut.GetFlatOntologyTerms(searchText, maxResults) as OkNegotiatedContentResult<ClassesSearchResult>;

            Assert.IsNotNull(result);
            Assert.AreEqual(resultTerm, result.Content.Classes.Single());
            Assert.AreEqual(searchText, result.Content.SearchText);
        }

        [TestMethod]
        public void GetTargetGroupHierarchy()
        {
            IHierarchicalClass appGroup = Substitute.For<IHierarchicalClass>();
            HierarchicalClass resultGroup = new HierarchicalClass();
            classificationRepository.GetTargetGroupHierarchy().Returns(new List<IHierarchicalClass> { appGroup });
            hierarchicalClassMapper.Map(appGroup).Returns(resultGroup);

            var result = sut.GetTargetGroupHierarchy() as OkNegotiatedContentResult<IEnumerable<HierarchicalClass>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(resultGroup, result.Content.Single());
        }
    }
}
