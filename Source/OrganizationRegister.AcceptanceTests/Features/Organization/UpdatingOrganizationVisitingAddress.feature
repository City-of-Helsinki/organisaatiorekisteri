Feature: UpdatingOrganizationVisitingAddress

Background: 
	Given there is an organization

Scenario: Setting organization visiting address
	When the following visiting address is set to the organization:
	| Finnish street address | Swedish street address | Finnish postal district | Swedish postal district | Finnish qualifier               | Swedish qualifier | Postal code |
	| Helsingintie 15        | Helsinforsvägen 15     | Turku                   | Åbo                     | Vaaleanpunaisen possun vieressä | Nära svin         | 21500       |
	Then the organization has the following visiting address:
	| Finnish street address | Swedish street address | Finnish postal district | Swedish postal district | Finnish qualifier               | Swedish qualifier | Postal code |
	| Helsingintie 15        | Helsinforsvägen 15     | Turku                   | Åbo                     | Vaaleanpunaisen possun vieressä | Nära svin         | 21500       |

Scenario: Setting organization visiting address again
	Given the following visiting address is set to the organization:
	| Finnish street address | Swedish street address | Finnish postal district | Swedish postal district | Finnish qualifier               | Swedish qualifier | Postal code |
	| Helsingintie 15        | Helsinforsvägen 15     | Turku                   | Åbo                     | Vaaleanpunaisen possun vieressä | Nära svin         | 21500       |
	When the following visiting address is set to the organization:
	| Finnish street address | Swedish street address | Finnish postal district | Swedish postal district | Finnish qualifier | Postal code |
	| Helsinginkatu 15       | Helsinforsgatan 15     | Turku                   | Åbo                     | Piipun vieressä   | 25100       |
	Then the organization has the following visiting address:
	| Finnish street address | Swedish street address | Finnish postal district | Swedish postal district | Finnish qualifier | Swedish qualifier | Postal code |
	| Helsinginkatu 15       | Helsinforsgatan 15     | Turku                   | Åbo                     | Piipun vieressä   |                   | 25100       |

Scenario: Organization can have no visiting address
	When visiting address of the organization is set as empty
	Then the organization has no visiting address

Scenario: Setting invalid postal code
	When the following visiting address is set to the organization:
	| Finnish street address | Swedish street address | Finnish postal district | Swedish postal district | Finnish qualifier | Postal code |
	| Helsinginkatu 15       | Helsinforsgatan 15     | Turku                   | Åbo                     | Piipun vieressä   | 251000      |
	Then setting the visiting address fails