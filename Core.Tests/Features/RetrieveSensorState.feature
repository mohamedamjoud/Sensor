Feature: Retrieve sensor state
    
    Scenario Outline: Sensor state is determined based on temperature
        Given temperature value is equal to '<temperature>'   
        When we try to retrieve the sensor state
        Then the state value should be '<state>'
        And the Sensor Data Should Be Saved
        Examples: 
          | temperature | state |
          | 40          | Hot   |
          | 41          | Hot   |
          | 21          | Cold  |
          | 22          | Warm  |
          | 35.1        | Hot   |
          
    Scenario: Sensor data saving fails when database is unavailable
        Given temperature value is equal to '40'
        And the sensor repository throws an exception
        When we try to retrieve the sensor state
        Then a "SaveSensorDataException" should be thrown

    