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
