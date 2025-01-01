namespace DB.Exceptions;

public class DataBaseException : Exception
{
    public DataBaseException() { }
    public DataBaseException(string message) : base(message) { }
    public DataBaseException(string message, Exception inner) : base(message, inner) { }
}
