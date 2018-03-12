Feature: UpdatingOrganizationAuthorizationInformation

Background: 
	Given there is an organization

Scenario: Setting organization authorization information
	When the following authorization information is set to the organization:
	| group name    | group id                             | role id                              | second group name | second group id                      | second role id                       |
	| domain\group1 | 87605358-1e1d-49f3-8172-63d00ad0f318 | c0902227-a8dc-49b0-adde-6105bab48dec | domain\group2     | b2d4d59e-55df-45d6-bea3-57cd142ccf5a | 6b8aed7c-e23f-4476-84ab-cccbfb224aba |
	Then the organization has the following authorization information:
	| group name    | group id                             | role id                              | second group name | second group id                      | second role id                       |
	| domain\group1 | 87605358-1e1d-49f3-8172-63d00ad0f318 | c0902227-a8dc-49b0-adde-6105bab48dec | domain\group2     | b2d4d59e-55df-45d6-bea3-57cd142ccf5a | 6b8aed7c-e23f-4476-84ab-cccbfb224aba |
	


Scenario: Organization can have no authorization information
	Given the following authorization information is set to the organization:
	| group name    | group id                             | role id                              | second group name | second group id                      | second role id                       |
	| domain\group1 | 87605358-1e1d-49f3-8172-63d00ad0f318 | c0902227-a8dc-49b0-adde-6105bab48dec | domain\group2     | b2d4d59e-55df-45d6-bea3-57cd142ccf5a | 6b8aed7c-e23f-4476-84ab-cccbfb224aba |
	When authorization information of the organization is set as empty
	Then the organization has no authorization information


