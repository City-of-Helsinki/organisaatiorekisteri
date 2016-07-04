Feature: UpdatingOrganizationContactInformation

Background: 
	Given there is an organization

Scenario: Setting organization contact information
	When the following contact information is set to the organization:
	| Phone number | Phone call fee  | Email address       | web site name | web address    | web page type | second web site name | second web address | second web page type | homepage in finnish | homepage in swedish |
	| 0205 777 450 | Local call cost | info.fi@affecto.com | Finnish site  | www.affecto.fi | Kotisivu      | Global site          | www.affecto.com    | Kotisivu             | http://wwww.url.fi  | http://wwww.url.sv  |
	Then the organization has the following contact information:
	| Phone number | Phone call fee  | Email address       | web site name | web address    | web page type | second web site name | second web address | second web page type | homepage in finnish | homepage in swedish |
	| 0205 777 450 | Local call cost | info.fi@affecto.com | Finnish site  | www.affecto.fi | Kotisivu      | Global site          | www.affecto.com    | Kotisivu             | http://wwww.url.fi  | http://wwww.url.sv  |

Scenario: Setting organization contact information again
	Given the following contact information is set to the organization:
	| Phone number | Phone call fee | Email address    | web site name | web address    | web page type | second web site name | second web address | second web page type | homepage in finnish | homepage in swedish |
	| 0100100      | Local call     | info@affecto.com | Finland       | www.affecto.fi | Kotisivu      | Everyone             | www.affecto.com    | Kotisivu             | http://wwww.url.fi  | http://wwww.url.sv  |
	When the following contact information is set to the organization:
	| Phone number | Phone call fee  | Email address       | web site name | web address    | web page type | second web site name | second web address | second web page type | homepage in finnish | homepage in swedish |
	| 0205 777 450 | Local call cost | info.fi@affecto.com | Finnish site  | www.affecto.fi | Kotisivu      | Global site          | www.affecto.com    | Kotisivu             | http://wwww.url.fi  | http://wwww.url.sv  |
	Then the organization has the following contact information:
	| Phone number | Phone call fee  | Email address       | web site name | web address    | web page type | second web site name | second web address | second web page type | homepage in finnish | homepage in swedish |
	| 0205 777 450 | Local call cost | info.fi@affecto.com | Finnish site  | www.affecto.fi | Kotisivu      | Global site          | www.affecto.com    | Kotisivu             | http://wwww.url.fi  | http://wwww.url.sv  |
	

Scenario: Organization can have no contact information
	Given the following contact information is set to the organization:
	| Phone number | Phone call fee | Email address    | web site name | web address    | web page type | second web site name | second web address | second web page type | homepage in finnish | homepage in swedish |
	| 0100100      | Local call     | info@affecto.com | Finland       | www.affecto.fi | Kotisivu      | Everyone             | www.affecto.com    | Kotisivu             | http://wwww.url.fi  | http://wwww.url.sv  |
	When contact information of the organization is set as empty
	Then the organization has no contact information

Scenario: Setting invalid email address
	When the following contact information is set to the organization:
	| Phone number | Phone call fee  | Email address       | web site name | web address    | web page type | second web site name | second web address | second web page type | homepage in finnish | homepage in swedish |
	| 0205 777 450 | Local call cost | info.fi2affecto.com | Finnish site  | www.affecto.fi | Kotisivu      | Global site          | www.affecto.com    | Kotisivu             | http://wwww.url.fi  | http://wwww.url.sv  |
	When contact information of the organization is set as empty
	Then setting the contact information fails

Scenario: Setting invalid phone number
	When the following contact information is set to the organization:
	| Phone number    | Phone call fee  | Email address       | web site name | web address    | web page type | second web site name | second web address | second web page type | homepage in finnish | homepage in swedish |
	| Local call cost | Local call cost | info.fi@affecto.com | Finnish site  | www.affecto.fi | Kotisivu      | Global site          | www.affecto.com    | Kotisivu             | http://wwww.url.fi  | http://wwww.url.sv  |
	When contact information of the organization is set as empty
	Then setting the contact information fails

Scenario: Setting invalid web address
	When the following contact information is set to the organization:
	| Phone number | Phone call fee  | Email address       | web site name | web address    | web page type | second web site name | second web address | second web page type | homepage in finnish | homepage in swedish |
	| 0205 777 450 | Local call cost | info.fi@affecto.com | Finnish site  | www.affecto.fi | Kotisivu      | Global site          | www,affecto,com    | Kotisivu             | http://wwww.url.fi  | http://wwww.url.sv  |
	When contact information of the organization is set as empty
	Then setting the contact information fails

	Scenario: Setting invalid homepage url
	When the following contact information is set to the organization:
	| Phone number | Phone call fee  | Email address       | web site name | web address    | web page type | second web site name | second web address | second web page type | homepage in finnish | homepage in swedish |
	| 0205 777 450 | Local call cost | info.fi@affecto.com | Finnish site  | www.affecto.fi | Kotisivu      | Global site          | www.affecto.com    | Kotisivu             | http://wwww.url.fi  | http://wwww.url     |
	Then setting the contact information fails