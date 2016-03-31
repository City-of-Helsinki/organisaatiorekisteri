"use strict";

module OrganizationRegister
{
    export class ServiceMapper
    {
        public static map(data: any): Service
        {
            return new Service(data.id, data.numericId, data.names, data.alternateNames, data.descriptions, data.shortDescriptions, data.userInstructions, data.requirements,
                data.languages, HierarchicalClassMapper.map(data.serviceClasses), HierarchicalClassMapper.map(data.ontologyTerms), HierarchicalClassMapper.map(data.targetGroups),
                HierarchicalClassMapper.map(data.lifeEvents), data.keywords);
        }
    }
}  