Feature: UpdatingOrganizationPostalAddresses

Background: 
	Given there is an organization

Scenario: Setting a street address as organization postal address
	When the following postal street address is set to the organization:
	| Finnish street address | Swedish street address | Finnish postal district | Swedish postal district | Postal code |
	| Helsingintie 15        | Helsinforsvägen 15     | Turku                   | Åbo                     | 21500       |
	Then the organization has the following postal street address:
	| Finnish street address | Swedish street address | Finnish postal district | Swedish postal district | Postal code |
	| Helsingintie 15        | Helsinforsvägen 15     | Turku                   | Åbo                     | 21500       |

Scenario: Setting a street address as organization postal address again
	Given the following postal street address is set to the organization:
	| Finnish street address | Swedish street address | Finnish postal district | Swedish postal district | Postal code |
	| Helsingintie 15        | Helsinforsvägen 15     | Turku                   | Åbo                     | 21500       |
	When the following postal street address is set to the organization:
	| Finnish street address | Swedish street address | Finnish postal district | Swedish postal district | Postal code |
	| Helsinginkatu 15       | Helsinforsgatan 15     | Turku                   | Åbo                     | 25100       |
	Then the organization has the following postal street address:
	| Finnish street address | Swedish street address | Finnish postal district | Swedish postal district | Postal code |
	| Helsinginkatu 15       | Helsinforsgatan 15     | Turku                   | Åbo                     | 25100       |

Scenario: Organization can have no postal address
	When postal address of the organization is set as empty
	Then the organization has no postal address

Scenario: Setting invalid street address postal code
	When the following postal street address is set to the organization:
	| Finnish street address | Swedish street address | Finnish postal district | Swedish postal district | Postal code |
	| Helsinginkatu 15       | Helsinforsgatan 15     | Turku                   | Åbo                     | 251000      |
	Then setting the postal address fails

Scenario: Setting a post office box address as organization postal address
	When the following postal post office box address is set to the organization:
	| Finnish postal district | Swedish postal district | Postal code | post office box |
	| Turku                   | Åbo                     | 21501       | 10              |
	Then the organization has the following postal post office box address:
	| Finnish postal district | Swedish postal district | Postal code | post office box |
	| Turku                   | Åbo                     | 21501       | 10              |

Scenario: Setting a post office box address as organization postal address again
	Given the following postal post office box address is set to the organization:
	| Finnish postal district | Swedish postal district | Postal code | post office box |
	| Turku                   | Åbo                     | 21501       | 10              |
	When the following postal post office box address is set to the organization:
	| Finnish postal district | Swedish postal district | Postal code | post office box |
	| Turku                   | Åbo                     | 25101       | 11              |
	Then the organization has the following postal post office box address:
	| Finnish postal district | Swedish postal district | Postal code | post office box |
	| Turku                   | Åbo                     | 25101       | 11              |

Scenario: Setting invalid post office box address postal code
	When the following postal post office box address is set to the organization:
	| Finnish postal district | Swedish postal district | Postal code | post office box |
	| Turku                   | Åbo                     | 251000      | 10              |
	Then setting the postal address fails

Scenario: Setting invalid post office box
	When the following postal post office box address is set to the organization:
	| Finnish postal district | Swedish postal district | Postal code | post office box |
	| Turku                   | Åbo                     | 25101       | o               |
	Then setting the postal address fails

Scenario: Setting both street and post office box organization postal address
	When the following postal address is set to the organization:
	| Finnish street address | Swedish street address | Finnish street address postal district | Swedish street address postal district | Street address postal code | post office box | Finnish post office box address postal district | Swedish post office box address postal district | post office box address postal code |
	| Helsinginkatu 15       | Helsinforsgatan 15     | Turku                                  | Åbo                                    | 25100                      | 11              | Turku                                           | Åbo                                             | 25101                               |
	Then the organization has the following postal street address:
	| Finnish street address | Swedish street address | Finnish postal district | Swedish postal district | Postal code |
	| Helsinginkatu 15       | Helsinforsgatan 15     | Turku                   | Åbo                     | 25100       |
	And the organization has the following postal post office box address:
	| Finnish postal district | Swedish postal district | Postal code | post office box |
	| Turku                   | Åbo                     | 25101       | 11              |

Scenario: Using organization visiting address as the postal address
	Given the following visiting address is set to the organization:
	| Finnish street address | Swedish street address | Finnish postal district | Swedish postal district | Finnish qualifier               | Swedish qualifier | Postal code |
	| Helsingintie 15        | Helsinforsvägen 15     | Turku                   | Åbo                     | Vaaleanpunaisen possun vieressä | Nära svin         | 21500       |
	When the postal address of the organization is set to be the same as the visting address
	Then the postal address of the organization is the same as the visiting address

Scenario: Using organization visiting address as the postal address when there is no visiting address
	When the postal address of the organization is set to be the same as the visting address
	Then setting the postal address fails

Scenario: Cleared visiting address cannot be used as a postal address
	Given the following visiting address is set to the organization:
	| Finnish street address | Swedish street address | Finnish postal district | Swedish postal district | Finnish qualifier               | Swedish qualifier | Postal code |
	| Helsingintie 15        | Helsinforsvägen 15     | Turku                   | Åbo                     | Vaaleanpunaisen possun vieressä | Nära svin         | 21500       |
	And the postal address of the organization is set to be the same as the visting address
	When visiting address of the organization is set as empty
	Then the postal address of the organization is not the same as the visiting address
