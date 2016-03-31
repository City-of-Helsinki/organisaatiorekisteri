Feature: Settings

Scenario: Organization types
	Given the following organization types exist:
	| Organization type |
	| Municipality      |
	| State             |
	| Company           |
	When organization types are retrieved
	Then the following organization types are returned
	| Organization type |
	| Municipality      |
	| State             |
	| Company           |

Scenario: Web page types
	Given the following web page types exist:
	| Web page type              |
	| Kotisivu                   |
	| Sosiaalisen median palvelu |
	When web page types are retrieved
	Then the following web page types are returned
	| Web page type              |
	| Kotisivu                   |
	| Sosiaalisen median palvelu |

Scenario: Service languages
	Given the languages exist:
	| Language code | Language name |
	| fi            | Finnish       | 
	| sv            | Swedish       | 
	| en            | English       |
	| ar            | Arabic        |
	| et            | Estonian      |
	And the following languages can be used with services
	| Language code | Order number |
	| fi            | 2            |
	| en            |              |
	| sv            | 1            |
	| ar            |              |
	When service languages are retrieved
	Then the following service languages are returned in the following order:
	| Language code | Language name |
	| sv            | Swedish       |
	| fi            | Finnish       |
	| ar            | Arabic        |
	| en            | English       |
