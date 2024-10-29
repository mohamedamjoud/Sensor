Feature: Retrieve Latest Sensor States Requests

Scenario: Retrieve The Latest Fifteen Sensor States Requests.
	Given a request for the latest '15' sensor state records
	When the system retrieves the latest '15' sensor states
 	Then the system should retrieve exactly '15' sensor states
	And the result should contain the expected number of sensors