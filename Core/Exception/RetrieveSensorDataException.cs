namespace Core.UseCase;

public class RetrieveSensorDataException : System.Exception
{
    public RetrieveSensorDataException(string message, System.Exception innerException): base(message,innerException)
    {
    }
}