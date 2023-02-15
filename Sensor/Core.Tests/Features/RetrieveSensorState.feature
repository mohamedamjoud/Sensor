Feature: Retrieve sensor state
    
    Scenario: Sensor state will be Hot and temperature will be saved when it's value is 40C°
        Given temperature value is equal to '40'   
        When we try to retrieve the sensor state
        Then the state should be 'Hot'
        And the temperature will be saved
        
    Scenario: Sensor state will be Hot and temperature will be saved when it's value is 41C°
        Given temperature value is equal to '41'  
        When we try to retrieve the sensor state
        Then the state should be 'Hot'
        And the temperature will be saved

    Scenario: Sensor state will be Cold and temperature will be saved when it's value is less than 22C°
        Given temperature value is equal to '21'  
        When we try to retrieve the sensor state
        Then the state should be 'Cold'
        And the temperature will be saved

    Scenario: Sensor state will be Warm and temperature will be saved when it's value is 22C°
        Given temperature value is equal to '22'   
        When we try to retrieve the sensor state
        Then the state should be 'Warm'
        And the temperature will be saved
     
    Scenario: Sensor state will be Warm and temperature will be saved when it's value is 39C°
        Given temperature value is equal to '39'  
        When we try to retrieve the sensor state
        Then the state should be 'Warm'
        And the temperature will be saved

    