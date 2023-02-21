Feature: Retrieve Latest Sensor States Requests

Scenario: Retrieve The Latest Fifteen Sensor States Requests.
	Given I am adapter, I need latest '15' sensor states requests
	When We try to retrieve the latest Fifteen Sensor States Requests.
	Then The sensor state repository should be invoked with size '15' in parameters
	