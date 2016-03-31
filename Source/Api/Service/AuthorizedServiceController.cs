using System;
using System.Web.Http;
using Affecto.Patterns.Cqrs;
using Affecto.WebApi.Toolkit.CustomRoutes;
using OrganizationRegister.Application.Service;
using OrganizationRegister.Commanding.Service.Commands;

namespace OrganizationRegister.Api.Service
{
    [RoutePrefix("v1/serviceregister")]
    public class AuthorizedServiceController : ApiController
    {
        private readonly IServiceService serviceService;
        private readonly ICommandBus commandBus;

        public AuthorizedServiceController(IServiceService serviceService, ICommandBus commandBus)
        {
            if (serviceService == null)
            {
                throw new ArgumentNullException("serviceService");
            }
            if (commandBus == null)
            {
                throw new ArgumentNullException("commandBus");
            }

            this.serviceService = serviceService;
            this.commandBus = commandBus;
        }

        [HttpPost]
        [PostRoute("organizations/{organizationId}/services")]
        public IHttpActionResult AddService(Guid organizationId, BasicInformation service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
            Guid serviceId = serviceService.AddService(organizationId, service.Names, service.AlternateNames, service.Descriptions, service.ShortDescriptions,
                service.UserInstructions, service.LanguageCodes, service.Requirements);
            return Ok(serviceId);
        }

        [HttpPut]
        [PutRoute("organizations/{organizationId}/services/{serviceId}/basicinformation")]
        public IHttpActionResult SetServiceBasicInformation(Guid organizationId, Guid serviceId, BasicInformation information)
        {
            if (information == null)
            {
                throw new ArgumentNullException("information");
            }
            serviceService.SetServiceBasicInformation(organizationId, serviceId, information.Names, information.AlternateNames, information.Descriptions,
                information.ShortDescriptions, information.UserInstructions, information.LanguageCodes, information.Requirements);
            return Ok();
        }

        [HttpPut]
        [PutRoute("organizations/{organizationId}/services/{serviceId}/classification")]
        public IHttpActionResult SetServiceClassification(Guid organizationId, Guid serviceId, Classification classification)
        {
            if (classification == null)
            {
                throw new ArgumentNullException("classification");
            }
            var command = new SetServiceClassification(organizationId, serviceId, classification.ServiceClasses, classification.OntologyTerms,
                classification.TargetGroups, classification.LifeEvents, classification.Keywords);
            commandBus.Send(Envelope.Create(command));
            return Ok();
        }
    }
}