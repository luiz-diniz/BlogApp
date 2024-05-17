namespace BlogApp.Core.Exceptions;

public class InvalidPasswordConfirmationException : Exception
{
    public InvalidPasswordConfirmationException(string? message) : base(message)
    {
    }
}