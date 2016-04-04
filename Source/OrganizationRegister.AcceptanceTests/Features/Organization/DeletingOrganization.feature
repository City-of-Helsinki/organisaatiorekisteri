Feature: DeletingOrganization

Scenario: Deleting an organization with all sections filled
	Given there is a fully filled organization 'Turku' with business id '0204819-8'
	And there is a fully filled organization 'Kaarina' with business id '0133226-9'
	When organization 'Turku' is deleted
	Then there are following organizations:
	| Name    |
	| Kaarina |

Scenario: Deleting an organization with only mandatory information filled
	Given there is an organization 'Turku' with business id '0204819-8'
	And there is an organization 'Kaarina' with business id '0133226-9'
	When organization 'Turku' is deleted
	Then there are following organizations:
	| Name    |
	| Kaarina |

Scenario: Deleting an organization with sub organizations
	Given there is an organization 'Turku' with business id '0204819-8'
	And there is an organization 'Turku Touring' which is a sub organization of 'Turku'
	And there is an organization 'Visit Turku' which is a sub organization of 'Turku'
	And there is an organization 'Markkinointi' which is a sub organization of 'Visit Turku'
	When organization 'Turku' is deleted
	Then there are no organizations