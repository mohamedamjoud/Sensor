Feature: RetrieveLatestSensorStatesRequests
	
Scenario: RetrieveTheLatestFifteenSensorStatesRequests.
	Given The sensor returns these temperatures
	  | temperature |
	  |       1  	|
	  |       2  	|
	  |       3  	|
	  |       4   	|
	  |       5     |
	  |       6    	|
	  |       7    	|
	  |       8     |
	  |       9     |
	  |       45    |
	  |       45    |
	  |       45	|
	  |       45	|
	  |       45	|
	  |       45	|
	When We try to retrieve the latest Fifteen Sensor States Requests
	Then We got the same sensor states