Feature: Retrieve sensor state
 
Scenario: Retrieve sensor state
	Given I am a client
	When Get request to controller was made
	Then  the request status code will be 200

Scenario: Sensor state will be Hot and temperature will be saved when it's value is 40C°
	Given temperature value is equal to '40'   
	When Get request to controller was made
	Then the state value should be 'Hot'
	And the request status code will be 200
        
Scenario: Sensor state will be Hot and temperature will be saved when it's value is 41C°
	Given temperature value is equal to '41'  
	When Get request to controller was made
	Then the state value should be 'Hot'
	And the request status code will be 200

Scenario: Sensor state will be Cold and temperature will be saved when it's value is less than 22C°
	Given temperature value is equal to '21'  
	When Get request to controller was made
	Then the state value should be 'Cold'
	And the request status code will be 200

Scenario: Sensor state will be Warm and temperature will be saved when it's value is 22C°
	Given temperature value is equal to '22'   
	When Get request to controller was made
	Then the state value should be 'Warm'
	And the request status code will be 200
     
Scenario: Sensor state will be Warm and temperature will be saved when it's value is 39C°
	Given temperature value is equal to '39'  
	When Get request to controller was made
	Then the state value should be 'Warm'
	And the request status code will be 200
	