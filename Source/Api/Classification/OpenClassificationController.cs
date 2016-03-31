using System;
using System.Collections.Generic;
using System.Web.Http;
using Affecto.Mapping;
using Affecto.WebApi.Toolkit.CustomRoutes;
using OrganizationRegister.Application.Classification;

namespace OrganizationRegister.Api.Classification
{
    [RoutePrefix("v1/serviceregister")]
    public class OpenClassificationController : ApiController
    {
        private readonly IClassificationRepository classificationRepository;
        private readonly MapperFactory mapperFactory;

        public OpenClassificationController(IClassificationRepository classificationRepository, MapperFactory mapperFactory)
        {
            if (classificationRepository == null)
            {
                throw new ArgumentNullException("classificationRepository");
            }
            if (mapperFactory == null)
            {
                throw new ArgumentNullException("mapperFactory");
            }
            this.classificationRepository = classificationRepository;
            this.mapperFactory = mapperFactory;
        }

        [HttpGet]
        [GetRoute("lifeevents")]
        public IHttpActionResult GetLifeEventHierarchy()
        {
            IEnumerable<IHierarchicalClass> lifeEvents = classificationRepository.GetLifeEventHierarchy();
            return MapAndWrapClasses(lifeEvents);
        }

        [HttpGet]
        [GetRoute("serviceclasses")]
        public IHttpActionResult GetServiceClassHierarchy()
        {
            IEnumerable<IHierarchicalClass> serviceClasses = classificationRepository.GetServiceClassHierarchy();
            return MapAndWrapClasses(serviceClasses);
        }

        [HttpGet]
        [GetRoute("ontologyterms")]
        public IHttpActionResult GetOntologyTermHierarchy()
        {
            IEnumerable<IHierarchicalClass> ontologyTerms = classificationRepository.GetOntologyTermHierarchy();
            return MapAndWrapClasses(ontologyTerms);
        }

        [HttpGet]
        [GetRoute("ontologyterms/{searchText}")]
        public IHttpActionResult GetFlatOntologyTerms(string searchText, int? maxResults = null)
        {
            IEnumerable<IClass> terms = maxResults.HasValue ? classificationRepository.GetFlatOntologyTerms(searchText, maxResults.Value) : 
                classificationRepository.GetFlatOntologyTerms(searchText);

            var mapper = mapperFactory.CreateClassMapper();
            IEnumerable<Class> mappedTerms = mapper.Map(terms);
            return Ok(new ClassesSearchResult { Classes = mappedTerms, SearchText = searchText });
        }

        [HttpGet]
        [GetRoute("targetgroups")]
        public IHttpActionResult GetTargetGroupHierarchy()
        {
            IEnumerable<IHierarchicalClass> targetGroups = classificationRepository.GetTargetGroupHierarchy();
            return MapAndWrapClasses(targetGroups);
        }

        private IHttpActionResult MapAndWrapClasses(IEnumerable<IHierarchicalClass> classes)
        {
            var classMapper = mapperFactory.CreateHierarchicalClassMapper();
            IEnumerable<HierarchicalClass> mappedClasses = classMapper.Map(classes);
            return Ok(mappedClasses);
        }
    }
}