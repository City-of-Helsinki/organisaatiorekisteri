Feature: ValidatingData

Scenario: Valid business identifier
	When unique business identifier '1069622-4' is validated
	Then business identifer is valid

Scenario: Invalid format business identifier
	When unique business identifier '11069622-4' is validated
	Then business identifer is invalid because of invalid format

Scenario: Invalid business identifier check sum digit
	When unique business identifier '1069622-5' is validated
	Then business identifer is invalid because of invalid check sum digit

Scenario: Same unique business identifier cannot be added twice to different active organizations
	Given there is an active organization with business identifier '1234567-1'
	When unique business identifier '1234567-1' is validated for a new organization
	Then business identifer is invalid because it is already used

Scenario: Same unique business identifier can be added twice to different inactive organizations
	Given there is an inactive organization with business identifier '1234567-1'
	When unique business identifier '1234567-1' is validated for a new organization
	Then business identifer is valid

Scenario: Unchanged business identifier of an already added organization is valid
	Given there is an active organization with business identifier '1234567-1'
	When unique business identifier '1234567-1' is validated for the same organization
	Then business identifer is valid

Scenario: Non-unique business identifier can be added twice to different organizations
	Given there is an active organization with business identifier '1234567-1'
	When non-unique business identifier '1234567-1' is validated for a new organization
	Then business identifer is valid

Scenario: Valid phone number
	When phone number '0100100' is validated
	Then phone number is valid

Scenario: Invalid format phone number
	When phone number 'no number' is validated
	Then phone number is invalid

Scenario: Valid email address
	When email 'me@gmail.com' is validated
	Then email is valid

Scenario: Invalid format email address
	When email 'me2gmail,com' is validated
	Then email is invalid

Scenario: Valid web address
	When web address 'www.gmail.com' is validated
	Then web address is valid

Scenario: Invalid format web address
	When web address 'www.gmail,com' is validated
	Then web address is invalid

Scenario: Valid postal code
	When postal code '20540' is validated
	Then postal code is valid

Scenario: Invalid format postal code
	When postal code '2054' is validated
	Then postal code is invalid

Scenario: Valid post office box postal code
	When post office box postal code '20541' is validated
	Then post office box postal code is valid

Scenario: Invalid format post office box postal code
	When post office box postal code '2054' is validated
	Then post office box postal code is invalid