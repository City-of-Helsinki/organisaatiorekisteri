using System;
using Affecto.Identifiers;
using Affecto.Identifiers.Finnish;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Common;
using System.Linq;
using System.Collections.Generic;

namespace OrganizationRegister.Application.Validation
{
    internal class ValidationService : IValidationService
    {
        private readonly IOrganizationRepository organizationRepository;

        private void W(object s)
        {
            System.Diagnostics.Trace.WriteLine("[" + this.GetType() + "][" + this.GetHashCode() +"] " + s);
        }

        public ValidationService(IOrganizationRepository organizationRepository)
        {
            if (organizationRepository == null)
            {
                throw new ArgumentNullException("organizationRepository");
            }
            this.organizationRepository = organizationRepository;
        }

        //businessId = ptvId
        //tunnisteen ei tarvitse olla uniikki sovelluksessa -> validointi tieto on informatiivinen
        public IBusinessIdentifierValidationResult ValidateUniquePtvId(string businessId, Guid? organizationId)
        {
            W("ValidateUniquePtvId ptvId:" + businessId + ", organizationId: " + (organizationId != null ? organizationId + "": "null"));
         
            if (!string.IsNullOrWhiteSpace(businessId))
            {
                Guid oid = (Guid)organizationId;

                List<string> messages = new List<string>();
                var organizations = organizationRepository.GetOrganizationsByPtvId(businessId);
                foreach (var organization in organizations)
                {
                    W("organizationId: " + organization.Id + ", ptvId: " + organization.PTVId);
                    if (string.IsNullOrWhiteSpace(organization.PTVId)) { continue; }
                    if (organization.Id == oid) { continue; }

                    if (organization.PTVId == businessId)
                    {
                        string str = "?";

                        try
                        {
                            IOrganization temp = organizationRepository.GetOrganization(organization.Id) as IOrganization;
                            if (temp.Names != null)
                            {
                                LocalizedText localizedText = temp.Names.Where(a => a.LanguageCode == "fi").FirstOrDefault();
                                str = (localizedText != null ? localizedText.LocalizedValue : "");
                            }
                        }
                        catch (Exception ex)
                        {
                            str = ex.Message;
                        }

                        messages.Add(str + " (" + organization.Id + ")");

                        //return new BusinessIdentifierValidationResult(false, "Tunniste on varattu organisatiolle " + str + " (" + organization.Id + ")");
                    }
                }

                if (messages.Count > 0)
                {
                    return new BusinessIdentifierValidationResult(false, string.Join(", ", messages.ToArray()));
                }
            }

            return new BusinessIdentifierValidationResult(true, "");
        }

        public IBusinessIdentifierValidationResult ValidateUniqueBusinessIdentifier(string businessId, Guid? organizationId)
        {
            IBusinessIdentifierValidationResult result = ValidateBusinessIdentifier(businessId);
            if (result.IsValid && organizationRepository.HasActiveOrganization(businessId, organizationId))
            {
                return new BusinessIdentifierValidationResult(false, InvalidBusinessIdentifierReason.AlreadyExists);
            }
            return result;
        }

        public IBusinessIdentifierValidationResult ValidateBusinessIdentifier(string businessId)
        {
            var specification = new BusinessIdentifierSpecification();
            bool isValid = specification.IsSatisfiedBy(businessId);
            string reasonForInvalidity = specification.GetReasonsForDissatisfactionSeparatedWithNewLine();
            return new BusinessIdentifierValidationResult(isValid, reasonForInvalidity);
        }

        public bool ValidatePhoneNumber(string phoneNumber)
        {
            var specification = new PhoneNumberSpecification();
            return specification.IsSatisfiedBy(phoneNumber);
        }

        public bool ValidateEmailAddress(string emailAddress)
        {
            var specification = new EmailAddressSpecification();
            return specification.IsSatisfiedBy(emailAddress);
        }

        public bool ValidateWebAddress(string webAddress)
        {
            var specification = new WebAddressSpecification();
            return specification.IsSatisfiedBy(webAddress);
        }

        public bool ValidatePostalCode(string postalCode)
        {
            var specification = new PostalCodeSpecification();
            return specification.IsSatisfiedBy(postalCode);
        }

        public bool ValidatePostOfficeBoxPostalCode(string postalCode)
        {
            //var specification = new PostOfficeBoxPostalCodeSpecification();

            // use postal code validation specification with post office box postal code validation
            var specification = new PostalCodeSpecification();
            return specification.IsSatisfiedBy(postalCode);
        }
    }
}
