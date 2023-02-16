Feature: Save sensor state request 

Scenario: sensor state will be saved
	Given sensor state value '40'
	When we try to save the sensor state
	Then the sensor state will be saved