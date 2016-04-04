Feature: UpdatingOrganizationBasicInformation

Background: 
	Given there is an organization

Scenario: Changing organization basic information
	When the following basic information is set to the organization:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   |
	| 1069622-4   | 654321 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget |
	Then the organization has the following basic information:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   |
	| 1069622-4   | 654321 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget |

Scenario: Changing organization type to municipality
	When the following basic information is set to the organization:
	| Business id | Type  | Finnish name | Municipality code |
	| 1069622-4   | Kunta | Kaarina      | 202               |
	Then the organization has the following basic information:
	| Business id | Type  | Finnish name | Municipality code |
	| 1069622-4   | Kunta | Kaarina      | 202               |

Scenario: Changing organization type from municipality
	Given the following basic information is set to the organization:
	| Business id | Type  | Finnish name | Municipality code |
	| 1069622-4   | Kunta | Kaarina      | 202               |
	When the following basic information is set to the organization:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   |
	| 1069622-4   | 654321 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget |
	Then the organization has the following basic information:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   |
	| 1069622-4   | 654321 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget |

Scenario: Setting only mandatory organization basic information
	When the following basic information is set to the organization:
	| Business id | Type   | Finnish name |
	| 1234567-1   | Yritys | Firma        |
	Then the organization has the following basic information:
	| Business id | Type   | Finnish name |
	| 1234567-1   | Yritys | Firma        |

Scenario: Claering organization optional information
	Given the following basic information is set to the organization:
	| Business id | Oid    | Type   | Finnish name | Swedish name | Finnish description | Swedish description   |
	| 1069622-4   | 654321 | Yritys | Affecto      | Affecto      | Ohjelmistoyritys    | Programvara företaget |
	When the following basic information is set to the organization:
	| Business id | Type   | Finnish name |
	| 1234567-1   | Yritys | Firma        |
	Then the organization has the following basic information:
	| Business id | Type   | Finnish name |
	| 1234567-1   | Yritys | Firma        |

Scenario: Setting invalid business identifier
	When the following basic information is set to the organization:
	| Business id | Type   | Finnish name |
	| 1069622-44  | Yritys | Affecto      |
	Then setting the basic information fails

Scenario: Setting a business identifier that is already used for a main organization
	Given the following company is added:
	| Business id | Finnish name |
	| 1069622-4   | Affecto      |
	When the following basic information is set to the previously added organization:
	| Business id | Type   | Finnish name |
	| 1069622-4   | Yritys | Affecto      |
	Then setting the basic information fails

Scenario: Setting a business identifier that is already used for a sub organization
	Given the following company is added:
	| Business id | Finnish name |
	| 1069622-4   | Affecto      |
	And the following company is added as a sub organization of 'Affecto'
	| Business id | Finnish name       |
	| 1234567-1   | Affecto Finland Oy |
	When the following basic information is set to organization 'Affecto Finland Oy':
	| Business id | Type   | Finnish name       |
	| 1069622-4   | Yritys | Affecto Finland Oy |
	Then the organization 'Affecto Finland Oy' has the following basic information:
	| Business id | Type   | Finnish name       |
	| 1069622-4   | Yritys | Affecto Finland Oy |

Scenario: Clearing a sub organization business identifier
	Given the following company is added:
	| Business id | Finnish name |
	| 1069622-4   | Affecto      |
	And the following company is added as a sub organization of 'Affecto'
	| Business id | Finnish name       |
	| 1234567-1   | Affecto Finland Oy |
	When the following basic information is set to organization 'Affecto Finland Oy':
	| Business id | Type   | Finnish name       |
	|             | Yritys | Affecto Finland Oy |
	Then the organization 'Affecto Finland Oy' has the following basic information:
	| Business id | Type   | Finnish name       |
	|             | Yritys | Affecto Finland Oy |

Scenario: Setting invalid municipality code
	When the following basic information is set to the organization:
	| Business id | Type  | Finnish name | Municipality code |
	| 1069622-4   | Kunta | Kaarina      | 2002              |
	Then setting the basic information fails
