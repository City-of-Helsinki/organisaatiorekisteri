﻿Feature: AddingOrganization

Scenario: Adding a company
	When the following company is added:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   | Valid from | Valid to   | Finnish name abbreviation | Swedish name abbreviation |
	| 1234567-1   | 123456 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget | 01.01.2015 | 01.01.2016 | AFE                       | AFEsv                     |
	Then there are following organizations:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   | Valid from | Valid to   | Finnish name abbreviation | Swedish name abbreviation |
	| 1234567-1   | 123456 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget | 01.01.2015 | 01.01.2016 | AFE                       | AFEsv                     |


Scenario: Adding a municipality
	When the following municipality is added:
	| Business id | Oid    | Type  | Finnish name | Swedish name | Municipality code | Valid from |
	| 1234567-1   | 123456 | Kunta | Turku        | Åbo          | 853               | 01.02.2014 |
	And the following municipality is added:
    | Business id | Oid | Type  | Finnish name | Swedish name | Municipality code | Valid from |
    | 1069622-4   |     | Kunta | Kaarina      | St. Karins   | 202               | 01.03.2014 |
	Then there are following organizations:
	| Business id | Oid    | Type  | Finnish name | Swedish name | Municipality code | Valid from |
	| 1234567-1   | 123456 | Kunta | Turku        | Åbo          | 853               | 01.02.2014 |
	| 1069622-4   |        | Kunta | Kaarina      | St. Karins   | 202               | 01.03.2014 |

Scenario: Adding an organization with invalid business id
	When the following municipality is added:
	| Business id | Oid    | Type  | Finnish name | Swedish name | Municipality code |
	| 1234567-12  | 123456 | Kunta | Turku        | Åbo          | 853               |
	Then Adding the organization fails

Scenario: Adding an organization with validity starting after it has ended
	When the following company is added:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   | Valid from | Valid to   |
	| 1234567-1   | 123456 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget | 01.01.2017 | 01.01.2016 |
	Then Adding the organization fails

Scenario: Adding two organizations with the same business id
	Given the following company is added:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   |
	| 1234567-1   | 123456 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget |
	When the following municipality is added:
	| Business id | Oid    | Type  | Finnish name | Swedish name | Municipality code |
	| 1234567-1   | 123456 | Kunta | Turku        | Åbo          | 853               |
	Then Adding the organization fails

Scenario: Adding a municipality with invalid id
	When the following municipality is added:
	| Business id | Oid    | Type  | Finnish name | Swedish name | Municipality code |
	| 1234567-1   | 123456 | Kunta | Turku        | Åbo          | 8533              |
	Then Adding the organization fails

Scenario: Adding sub organizations
	Given the following company is added:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   | 
	| 1234567-1   | 123456 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget |
	When the following company is added as a sub organization of 'Affecto'
	| Business id | Oid    | Type   | Finnish name | Swedish name  | Finnish description | Swedish description   | Valid to   |
	| 2352175-7   | 654321 | Yritys | Karttakeskus | karta centrum | Ohjelmistoyritys    | Programvara företaget | 01.01.2074 |
	And the following company is added as a sub organization of 'Affecto'
	| Business id | Oid     | Type   | Finnish name    | Swedish name    | Finnish description | Swedish description   |
	| 0140351-4   | 1234567 | Yritys | Affecto Finland | Affecto Finland | Ohjelmistoyritys    | Programvara företaget |
	And the following company is added as a sub organization of 'Karttakeskus'
	| Business id | Oid | Type   | Finnish name          | Swedish name              | Finnish description | Swedish description |
	| 2352175-7   | 12  | Yritys | Karttakeskus Helsinki | karta centrum Helsingfors |                     |                     |
	Then there are following organizations:
	| Business id | Oid     | Type   | Finnish name          | Swedish name              | Finnish description | Swedish description   | Valid to   |
	| 1234567-1   | 123456  | Yritys | Affecto               | Affecto                   | Ohjelmistoyritys    | Programvara företaget |            |
	| 2352175-7   | 654321  | Yritys | Karttakeskus          | karta centrum             | Ohjelmistoyritys    | Programvara företaget | 01.01.2074 |
	| 2352175-7   | 12      | Yritys | Karttakeskus Helsinki | karta centrum Helsingfors |                     |                       |            |
	| 0140351-4   | 1234567 | Yritys | Affecto Finland       | Affecto Finland           | Ohjelmistoyritys    | Programvara företaget |            |
	And there are following main organizations:
	| Finnish name          | Swedish name              |
	| Affecto               | Affecto                   |
	And 'Karttakeskus' is a sub organization of 'Affecto'
	And 'Affecto Finland' is a sub organization of 'Affecto'
	And 'Karttakeskus Helsinki' is a sub organization of 'Karttakeskus'

Scenario: Sub organization doesn't need a business id
	Given the following company is added:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   |
	| 1234567-1   | 123456 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget |
	When the following company is added as a sub organization of 'Affecto'
	| Business id | Oid |  | Type   | Finnish name | Swedish name  | Finnish description | Swedish description   |
	|             |     |  | Yritys | Karttakeskus | karta centrum | Ohjelmistoyritys    | Programvara företaget |
	Then there are following organizations:
	| Business id | Oid    | Type   | Finnish name | Swedish name  | Finnish description | Swedish description   |
	| 1234567-1   | 123456 | Yritys | Affecto      | Affecto       | Ohjelmistoyritys    | Programvara företaget |
	|             |        | Yritys | Karttakeskus | karta centrum | Ohjelmistoyritys    | Programvara företaget |
	And 'Karttakeskus' is a sub organization of 'Affecto'
