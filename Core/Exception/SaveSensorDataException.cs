namespace Core.Exception;

public class SaveSensorDataException : System.Exception
{
    public SaveSensorDataException(string message, System.Exception innerException) : base(message,innerException)
    {
    }
}