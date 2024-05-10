namespace Shared.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name) : base($"Not found: {name}")
    {
        
    }
}