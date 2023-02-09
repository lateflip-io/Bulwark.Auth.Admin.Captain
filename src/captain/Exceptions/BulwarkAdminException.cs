namespace Bulwark.Admin.Client.Exceptions;

public class BulwarkAdminException : Exception
{
    public BulwarkAdminException(string message) :
        base(message){ }

    public BulwarkAdminException(string message, Exception inner) :
        base(message, inner)
    {

    }
}