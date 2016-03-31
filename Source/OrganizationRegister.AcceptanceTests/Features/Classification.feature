Feature: Classification

Scenario: Getting ordered life event hierarchy
	Given there are following life events:
	| Name                     | Parent          | Order |
	| Getting a child          |                 | 1     |
	| Adoption                 | Getting a child | 1     |
	| Military service         |                 | 2     |
	| Retirement               |                 |       |
	| Divorce                  |                 |       |
	| Moving                   |                 |       |
	| Moving away from Finland | Moving          | 2     |
	| Moving back to Finland   | Moving          | 3     |
	| Moving away from home    | Moving          | 1     |
	When the life events are retrievd
	Then the following main life events are received:
	| Name             |
	| Getting a child  |
	| Military service |
	| Divorce          |
	| Moving           |
	| Retirement       |
	And life event 'Getting a child' has the following child life events:
	| Name     |
	| Adoption |
	And life event 'Moving' has the following child life events:
	| Name                     |
	| Moving away from home    |	
	| Moving away from Finland |
	| Moving back to Finland   |

Scenario: Getting ordered service class hierarchy
	Given there are following service classes:
	| Name             | Parent          | Order |
	| Family services  |                 | 1     |
	| Getting a child  | Family services | 1     |
	| Children daycare | Family services | 2     |
	| Democracy        |                 | 2     |
	| Parties          | Democracy       | 1     |
	| Elections        | Democracy       | 2     |
	When the service classes are retrievd
	Then the following main service classes are received:
	| Name            |
	| Family services |
	| Democracy       |
	And service class 'Family services' has the following child service classes:
	| Name             |
	| Getting a child  |
	| Children daycare |
	And service class 'Democracy' has the following child service classes:
	| Name      |
	| Parties   |
	| Elections |

Scenario: Getting ordered ontology term hierarchy
	Given there are following ontology terms:
	| Name      | Parent    | Order |
	| Teaching  |           | 2     |
	| Daycare   |           | 1     |
	| Languages | Daycare   | 1     |
	| Arabic    | Languages | 1     |
	| Spanish   | Languages | 3     |
	| English   | Languages | 2     |
	When the ontology terms are retrievd
	Then the following main ontology terms are received:
	| Name     |
	| Daycare  |
	| Teaching |
	And ontology term 'Daycare' has the following child ontology terms:
	| Name      |
	| Languages |
	And ontology term 'Languages' has the following child ontology terms:
	| Name      |
	| Arabic    |
	| English   |
	| Spanish   |
	And ontology term 'Teaching' has no child ontology terms

Scenario: Getting ordered target group hierarchy
	Given there are following target groups:
	| Name                    | Parent    | Order |
	| Companies               |           | 2     |
	| Citizens                |           | 1     |
	| Elderly people          | Citizens  | 1     |
	| Children                | Citizens  | 2     |
	| Inventors               | Companies | 2     |
	| International companies | Companies | 1     |
	When the target groups are retrievd
	Then the following main target groups are received:
	| Name      |
	| Citizens  |
	| Companies |
	And target group 'Companies' has the following child target groups:
	| Name                    |
	| International companies |
	| Inventors               |
	And target group 'Citizens' has the following child target groups:
	| Name           |
	| Elderly people |
	| Children       |

Scenario: Getting ordered flat ontology term list
	Given there are following ontology terms:
	| Name             | Parent   | Order |
	| Teaching         |          |       |
	| Day              |          |       |
	| A Day in a Verse |          |       |
	| Daytime saving   |          |       |
	| night and day    |          |       |
	| daycare          | Teaching |       |
	When ontology terms are searched with text 'Day'
	Then the following ontology terms are received:
	| Name             |
	| Day              |
	| daycare          |
	| Daytime saving   |
	| A Day in a Verse |
	| night and day    |

Scenario: Getting ontology term list with only best matches
	Given there are following ontology terms:
	| Name             | Parent   | Order |
	| Teaching         |          |       |
	| Day              |          |       |
	| A Day in a Verse |          |       |
	| Daytime saving   |          |       |
	| night and day    |          |       |
	| daycare          | Teaching |       |
	When ontology terms are searched with text 'Day' and a maximum result amount of '4'
	Then the following ontology terms are received:
	| Name             |
	| Day              |
	| daycare          |
	| Daytime saving   |
	| A Day in a Verse |

Scenario: Ontology terms cannot be searched with less than two character text
	Given there are following ontology terms:
	| Name             | Parent   | Order |
	| Teaching         |          |       |
	| Day              |          |       |
	| A Day in a Verse |          |       |
	| Daytime saving   |          |       |
	| Daycare          | Teaching |       |
	When ontology terms are searched with text 'a'
	Then no ontology terms are received
