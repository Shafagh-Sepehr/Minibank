namespace Repository.Exceptions;

public class RepositoryException : Exception
{
    RepositoryException()
    {
    }
    
    public RepositoryException(string message) : base(message)
    {
        
    }
    
    public RepositoryException(string message, Exception inner) : base(message, inner)
    {
        
    }
}
