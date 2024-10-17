namespace Core.UseCase;

public class RetrieveSensorDataException : Exception
{
    public RetrieveSensorDataException(string message, Exception innerException): base(message,innerException)
    {
    }
}