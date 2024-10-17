namespace Core.Domain.Exception;

public class InvalidTemperatureException: System.Exception
{
    public InvalidTemperatureException(string message , System.Exception innerException) : base(message, innerException)
    {
        
    }
}