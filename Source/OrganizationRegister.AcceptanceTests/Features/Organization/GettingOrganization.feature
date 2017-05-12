Feature: GettingOrganization

Scenario: Getting organization name
	Given the following company is added:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   | Can be added to FSC | Can Be Responsible Dept For Service |
	| 1234567-1   | 123456 | Yritys | Affecto Oy   | Affecto Ab   | Ohjelmistoyritys    | Programvara företaget | false               | false                               |
	When the name of organization 'Affecto Oy' is requested
	Then the following information is retrieved:
	| Finnish name | Swedish name |
	| Affecto Oy   | Affecto Ab   |
