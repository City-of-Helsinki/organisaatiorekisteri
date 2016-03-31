Feature: UpdatingServiceBasicInformation

Background: 
	Given there is an organization
	And there is a service

Scenario: Changing service basic information
	When the following basic information is set to the service
	| Finnish name | Swedish name | Finnish alternate name | Swedish alternate name | Finnish description | Swedish description | Finnish short description | Swedish short description | Languages | Finnish user instructions | Swedish user instructions | Finnish requirements | Swedish requirements |
	| Päivähoito   | Dagvård      | Päiväkoti              | DagHem                 | Hoidetaan lapsia    | Behandlar barn      | Lapsitarha                | Barnet trädgården         | fi, sv    | Tuo lapset aamulla        | Barn ska komma i morgon   | Tarpeeksi hoitajia   | Några sjukskötare    |
	Then the organization has the following service:
	| Finnish name | Swedish name | Finnish alternate name | Swedish alternate name | Finnish description | Swedish description | Finnish short description | Swedish short description | Languages | Finnish user instructions | Swedish user instructions | Finnish requirements | Swedish requirements |
	| Päivähoito   | Dagvård      | Päiväkoti              | DagHem                 | Hoidetaan lapsia    | Behandlar barn      | Lapsitarha                | Barnet trädgården         | sv, fi    | Tuo lapset aamulla        | Barn ska komma i morgon   | Tarpeeksi hoitajia   | Några sjukskötare    |

Scenario: Setting only mandatory service basic information
	When the following basic information is set to the service
	| Finnish name | Swedish name | Finnish description | Swedish description | Finnish short description | Swedish short description | Languages |
	| Päivähoito   | Dagvård      | Hoidetaan lapsia    | Behandlar barn      | Lapsitarha                | Barnet trädgården         | fi, sv    |
	Then the organization has the following service:
	| Finnish name | Swedish name | Finnish description | Swedish description | Finnish short description | Swedish short description | Languages |
	| Päivähoito   | Dagvård      | Hoidetaan lapsia    | Behandlar barn      | Lapsitarha                | Barnet trädgården         | sv, fi    |

Scenario: Claering service optional information
	Given the following basic information is set to the service
	| Finnish name | Swedish name | Finnish alternate name | Swedish alternate name | Finnish description | Swedish description | Finnish short description | Swedish short description | Languages | Finnish user instructions | Swedish user instructions | Finnish requirements | Swedish requirements |
	| Päivähoito   | Dagvård      | Päiväkoti              | DagHem                 | Hoidetaan lapsia    | Behandlar barn      | Lapsitarha                | Barnet trädgården         | fi, sv    | Tuo lapset aamulla        | Barn ska komma i morgon   | Tarpeeksi hoitajia   | Några sjukskötare    |
	When the following basic information is set to the service
	| Finnish name | Swedish name | Finnish description | Swedish description | Finnish short description | Swedish short description | Languages |
	| Päivähoito   | Dagvård      | Hoidetaan lapsia    | Behandlar barn      | Lapsitarha                | Barnet trädgården         | fi, sv    |
	Then the organization has the following service:
	| Finnish name | Swedish name | Finnish description | Swedish description | Finnish short description | Swedish short description | Languages |
	| Päivähoito   | Dagvård      | Hoidetaan lapsia    | Behandlar barn      | Lapsitarha                | Barnet trädgården         | sv, fi    |
