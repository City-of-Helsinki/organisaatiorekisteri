Feature: DeactivatingOrganization

Scenario: Deactivating an organization
	Given there is an organization 'Turku' with business id '0204819-8'
	And there is an organization 'Kaarina' with business id '0133226-9'
	When organization 'Turku' is deactivated
	Then there are following organizations:
	| Name    |
	| Kaarina |

Scenario: Deactivating an organization with sub organizations
	Given there is an organization 'Turku' with business id '0204819-8'
	And there is an organization 'Turku Touring' which is a sub organization of 'Turku'
	And there is an organization 'Visit Turku' which is a sub organization of 'Turku'
	And there is an organization 'Markkinointi' which is a sub organization of 'Visit Turku'
	When organization 'Turku' is deactivated
	Then there are no organizations