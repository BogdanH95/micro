namespace BuildingBlocks.Exceptions;

public class NotFoundException
    : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string name, object id) : base($"""Entity "{name}" ({id}) was not found.""")
    {

    }
}