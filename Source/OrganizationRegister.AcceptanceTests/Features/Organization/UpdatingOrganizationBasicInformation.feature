Feature: UpdatingOrganizationBasicInformation

Background: 
	Given there is an organization

Scenario: Changing organization basic information
	When the following basic information is set to the organization:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   | Valid from | Valid to   | Finnish name abbreviation | Swedish name abbreviation | Can be added to FSC |
	| 1069622-4   | 654321 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget | 12.11.2015 | 20.10.2016 | AFE                       | AFEsv                     | false               |
	Then the organization has the following basic information:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   | Valid from | Valid to   | Finnish name abbreviation | Swedish name abbreviation | Can be added to FSC |
	| 1069622-4   | 654321 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget | 12.11.2015 | 20.10.2016 | AFE                       | AFEsv                     | false               |

Scenario: Changing organization type to municipality
	When the following basic information is set to the organization:
	| Business id | Type  | Finnish name | Municipality code | Can be added to FSC |
	| 1069622-4   | Kunta | Kaarina      | 202               | false               |
	Then the organization has the following basic information:
	| Business id | Type  | Finnish name | Municipality code | Can be added to FSC |
	| 1069622-4   | Kunta | Kaarina      | 202               | false               |

Scenario: Changing organization type from municipality
	Given the following basic information is set to the organization:
	| Business id | Type  | Finnish name | Municipality code | Can be added to FSC |
	| 1069622-4   | Kunta | Kaarina      | 202               | false               |
	When the following basic information is set to the organization:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   | Can be added to FSC |
	| 1069622-4   | 654321 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget | true                |
	Then the organization has the following basic information:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   | Can be added to FSC |
	| 1069622-4   | 654321 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget | true                |

Scenario: Setting only mandatory organization basic information
	When the following basic information is set to the organization:
	| Business id | Type   | Finnish name | Can be added to FSC |
	| 1234567-1   | Yritys | Firma        | true                |
	Then the organization has the following basic information:
	| Business id | Type   | Finnish name | Can be added to FSC |
	| 1234567-1   | Yritys | Firma        | true                |

Scenario: Claering organization optional information
	Given the following basic information is set to the organization:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   | Valid from | Valid to   | Finnish name abbreviation | Swedish name abbreviation | Can be added to FSC |
	| 1069622-4   | 654321 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget | 12.11.2015 | 20.10.2016 | AFE                       | AFEsv                     | true                |
	When the following basic information is set to the organization:
	| Business id | Type   | Finnish name | Finnish name abbreviation | Can be added to FSC |
	| 1234567-1   | Yritys | Firma        | AFEfi                     | false               |
	Then the organization has the following basic information:
	| Business id | Type   | Finnish name | Finnish name abbreviation | Can be added to FSC |
	| 1234567-1   | Yritys | Firma        | AFEfi                     | false               |

Scenario: Setting invalid business identifier
	When the following basic information is set to the organization:
	| Business id | Type   | Finnish name | Can be added to FSC |
	| 1069622-44  | Yritys | Affecto      | true                |
	Then setting the basic information fails

Scenario: Setting a business identifier that is already used for a main organization
	Given the following company is added:
	| Business id | Finnish name | Can be added to FSC |
	| 1069622-4   | Affecto      | true                |
	When the following basic information is set to the previously added organization:
	| Business id | Type   | Finnish name | Can be added to FSC |
	| 1069622-4   | Yritys | Affecto      | true                |
	Then setting the basic information fails

Scenario: Setting a business identifier that is already used for a sub organization
	Given the following company is added:
	| Business id | Finnish name | Can be added to FSC |
	| 1069622-4   | Affecto      | true                |
	And the following company is added as a sub organization of 'Affecto'
	| Business id | Finnish name       |
	| 1234567-1   | Affecto Finland Oy |
	When the following basic information is set to organization 'Affecto Finland Oy':
	| Business id | Type   | Finnish name       | Can be added to FSC |
	| 1069622-4   | Yritys | Affecto Finland Oy | true                |
	Then the organization 'Affecto Finland Oy' has the following basic information:
	| Business id | Type   | Finnish name       | Can be added to FSC |
	| 1069622-4   | Yritys | Affecto Finland Oy | true                |

Scenario: Clearing a sub organization business identifier
	Given the following company is added:
	| Business id | Finnish name | Can be added to FSC |
	| 1069622-4   | Affecto      | true                |
	And the following company is added as a sub organization of 'Affecto'
	| Business id | Finnish name       |
	| 1234567-1   | Affecto Finland Oy |
	When the following basic information is set to organization 'Affecto Finland Oy':
	| Business id | Type   | Finnish name       | Can be added to FSC |
	|             | Yritys | Affecto Finland Oy | true                |
	Then the organization 'Affecto Finland Oy' has the following basic information:
	| Business id | Type   | Finnish name       | Can be added to FSC |
	|             | Yritys | Affecto Finland Oy | true                |

Scenario: Setting invalid municipality code
	When the following basic information is set to the organization:
	| Business id | Type  | Finnish name | Municipality code | Can be added to FSC |
	| 1069622-4   | Kunta | Kaarina      | 2002              | true                |
	Then setting the basic information fails

Scenario: Setting invalid validity
	When the following basic information is set to the organization:
	| Business id | Type   | Finnish name | Valid from | Valid to   | Can be added to FSC |
	| 1069622-4   | Yritys | Affecto      | 01.01.2012 | 01.01.2010 | true                |
	Then setting the basic information fails
