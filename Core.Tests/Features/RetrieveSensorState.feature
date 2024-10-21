Feature: Retrieve sensor state          
    Scenario: Sensor state will be Hot and temperature will be saved when it's value is 40C°
        Given temperature value is equal to '40'   
        When we try to retrieve the sensor state
        Then the state value should be 'Hot'
        And the Sensor Data Should Be Saved
        
    Scenario: Sensor state will be Hot and temperature will be saved when it's value is 41C°
        Given temperature value is equal to '41'  
        When we try to retrieve the sensor state
        Then the state value should be 'Hot'
        And the Sensor Data Should Be Saved

    Scenario: Sensor state will be Cold and temperature will be saved when it's value is less than 22C°
        Given temperature value is equal to '21'  
        When we try to retrieve the sensor state
        Then the state value should be 'Cold'
        And the Sensor Data Should Be Saved

    Scenario: Sensor state will be Warm and temperature will be saved when it's value is 22C°
        Given temperature value is equal to '22'   
        When we try to retrieve the sensor state
        Then the state value should be 'Warm'
        And the Sensor Data Should Be Saved
     
    Scenario: Sensor state will be Hot and temperature will be saved when it's value is 35.1C°
        Given temperature value is equal to '35.1'  
        When we try to retrieve the sensor state
        Then the state value should be 'Hot'
        And the Sensor Data Should Be Saved
        
    Scenario: Sensor data saving fails when database is unavailable
        Given temperature value is equal to '40'
        And the sensor repository throws an exception
        When we try to retrieve the sensor state
        Then a "SaveSensorDataException" should be thrown

    