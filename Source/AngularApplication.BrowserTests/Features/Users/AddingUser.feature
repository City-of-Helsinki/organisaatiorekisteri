@BrowserTest
Feature: AddingUser

Background: 
	Given an organization exists with the name 'Daycare Inc.'

Scenario: Mandatory information must be filled before user can be saved
	Given the administrator user is logged in
	And the user is starting to add a new user
	Then the user cannot be saved
	When last name 'Johnson' is typed
	Then the user cannot be saved
	When first name 'Leo' is typed
	Then the user cannot be saved
	When email address 'leo@hotmail.com' is typed
	Then the user cannot be saved
	When password 'LeoTheKing!' is typed
	Then the user cannot be saved
	When password 'LeoTheKing!' is typed again
	Then the user cannot be saved
	When organization 'Daycare Inc.' is selected
	Then the user cannot be saved
	When role 'Peruskäyttäjä' is selected
	Then the user can be saved

Scenario: Email address is validated
	Given the administrator user is logged in
	And the user is starting to add a new user
	And last name 'Johnson' is typed
	And first name 'Leo' is typed
	And password 'LeoTheKing!' is typed
	And password 'LeoTheKing!' is typed again
	And organization 'Daycare Inc.' is selected
	And role 'Peruskäyttäjä' is selected

	When email address 'leo2hotmail.com' is typed
	And focus is moved out of the email address field
	Then the user cannot be saved
	And email address error message is displayed

	When email address 'leo@hotmail.com' is typed
	And focus is moved out of the email address field
	Then the user can be saved
	And email address error message is not displayed

Scenario: Phone number is validated
	Given the administrator user is logged in
	And the user is starting to add a new user
	And last name 'Johnson' is typed
	And first name 'Leo' is typed
	And password 'LeoTheKing!' is typed
	And password 'LeoTheKing!' is typed again
	And organization 'Daycare Inc.' is selected
	And role 'Peruskäyttäjä' is selected
	And email address 'leo@hotmail.com' is typed

	When phone number 'I have no phone' is typed
	And focus is moved out of the phone number field
	Then the user cannot be saved
	And phone number error message is displayed

	When phone number '0501234567' is typed
	And focus is moved out of the phone number field
	Then the user can be saved
	And phone number error message is not displayed

Scenario: Password must be typed twice
	Given the administrator user is logged in
	And the user is starting to add a new user
	And last name 'Johnson' is typed
	And first name 'Leo' is typed
	And organization 'Daycare Inc.' is selected
	And role 'Peruskäyttäjä' is selected
	And email address 'leo@hotmail.com' is typed

	When password 'LeoTheKing!' is typed
	And password 'leoTheKing!!' is typed again
	Then the user cannot be saved
	And password mismatch error message is displayed

	When password 'LeoTheKing!' is typed again
	Then the user can be saved
	And password mismatch error message is not displayed

Scenario: Password strength is validated
	Given the administrator user is logged in
	And the user is starting to add a new user
	And last name 'Johnson' is typed
	And first name 'Leo' is typed
	And organization 'Daycare Inc.' is selected
	And role 'Peruskäyttäjä' is selected
	And email address 'leo@hotmail.com' is typed

	When password 'leo' is typed
	And password 'leo' is typed again
	Then the user cannot be saved
	And password strength error message is displayed

	When password 'LeoTheKing!' is typed
	And password 'LeoTheKing!' is typed again
	Then the user can be saved
	And password strength error message is not displayed

Scenario: User without user maintanance permission cannot add users
	Given the basic user is logged in
	When the user is starting to add a new user
	Then insufficient permissions error is displayed
	
Scenario: Administrator User can add users
	Given the administrator user is logged in
	And the user is starting to add a new user
	When last name 'yllapitaja' is typed
	And first name 'ylermi' is typed
	And email address 'ylermi@yllapitaja.com' is typed
	And password 'Password123' is typed
	And password 'Password123' is typed again
	And organization 'Daycare Inc.' is selected
	And role 'Peruskäyttäjä' is selected
	And the user is saved
	And current user logs out 
	And the user 'ylermi@yllapitaja.com' / 'Password123' is logged in
	Then the user 'ylermi' / 'yllapitaja' is logged in
	
Scenario: User without manage administrator users permission cannot add administrator user
	Given the organization administrator user is logged in
	When the user is starting to add a new user
	Then administrator role is not available