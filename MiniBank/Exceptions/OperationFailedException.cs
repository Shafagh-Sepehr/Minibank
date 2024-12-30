namespace MiniBank.Exceptions;

public class OperationFailedException : Exception
{
    OperationFailedException()
    {
        
    }
    
    public OperationFailedException(string message) : base(message)
    {
        
    }
    
    public OperationFailedException(string message, Exception inner) : base(message, inner)
    {
        
    }
}
