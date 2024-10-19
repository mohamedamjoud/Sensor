namespace Adapter.SQLite.Exception;

public class DatabaseSaveException : System.Exception
{
    public DatabaseSaveException(string message, System.Exception innerException) : base(message, innerException)
    {
    }
}