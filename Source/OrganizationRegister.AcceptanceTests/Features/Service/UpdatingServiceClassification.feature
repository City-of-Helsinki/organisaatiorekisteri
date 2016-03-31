Feature: UpdatingServiceClassification

Background: 
	Given there is an organization
	And there is a service
	And there are following service classes:
	| Name             | Parent          |
	| Family services  |                 |
	| Getting a child  | Family services |
	| Children daycare | Family services |
	| Democracy        |                 |
	| Parties          | Democracy       |
	| Elections        | Democracy       |
	And there are following ontology terms:
	| Name      | Parent    |
	| Teaching  |           |
	| Daycare   |           |
	| Languages | Daycare   |
	| Arabic    | Languages |
	| Spanish   | Languages |
	| English   | Languages |
	And there are following target groups:
	| Name                    | Parent    |
	| Companies               |           |
	| Citizens                |           |
	| Elderly people          | Citizens  |
	| Children                | Citizens  |
	| Inventors               | Companies |
	| International companies | Companies |
	And there are following life events:
	| Name                     | Parent          |
	| Getting a child          |                 |
	| Adoption                 | Getting a child |
	| Military service         |                 |
	| Retirement               |                 |
	| Divorce                  |                 |
	| Moving                   |                 |
	| Moving away from Finland | Moving          |
	| Moving back to Finland   | Moving          |
	| Moving away from home    | Moving          |

Scenario: Setting service classification
	When the following classification is set to the service:
	| Service classes                  | Ontology terms               | Target groups | Life events         | Finnish keywords | Swedish keywords |
	| Getting a child, Family services | Teaching, Daycare, Languages | Citizens      | Retirement, Divorce | perhe, lapset    | familjen, barnen |
	Then the service has the following classification:
	| Service classes                  | Ontology terms               | Target groups | Life events         | Finnish keywords | Swedish keywords |
	| Family services, Getting a child | Daycare, Languages, Teaching | Citizens      | Retirement, Divorce | lapset, perhe    | barnen, familjen |

Scenario: Setting service classification again
	Given the following classification is set to the service:
	| Service classes | Ontology terms     | Target groups | Life events              | Finnish keywords | Swedish keywords |
	| Family services | Daycare, Languages | Companies     | Military service, Moving | perhe            | familjen         |
	When the following classification is set to the service:
	| Service classes                  | Ontology terms               | Target groups | Life events         | Finnish keywords | Swedish keywords |
	| Family services, Getting a child | Teaching, Daycare, Languages | Citizens      | Retirement, Divorce | perhe, lapset    | familjen, barnen |
	Then the service has the following classification:
	| Service classes                  | Ontology terms               | Target groups | Life events         | Finnish keywords | Swedish keywords |
	| Family services, Getting a child | Daycare, Languages, Teaching | Citizens      | Retirement, Divorce | lapset, perhe    | barnen, familjen |

Scenario: Setting only mandatory classification
	When the following classification is set to the service:
	| Service classes                  | Ontology terms               | Target groups | Life events | Finnish keywords | Swedish keywords |
	| Family services, Getting a child | Teaching, Daycare, Languages | Citizens      |             |                  |                  |
	Then the service has the following classification:
	| Service classes                  | Ontology terms               | Target groups | Life events | Finnish keywords | Swedish keywords |
	| Family services, Getting a child | Daycare, Languages, Teaching | Citizens      |             |                  |                  |
