namespace Shared.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string name) : base($"Already exists: {name}")
    {
        
    }
}