using BlogApp.Core.Intefaces;
using static BCrypt.Net.BCrypt;

namespace BlogApp.Core;

public class PasswordService : IPasswordService
{
    public string GeneratePasswordHash(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentNullException(nameof(text), "Text null or empty.");

        return HashPassword(text);
    }

    public bool VerifyPasswordMatch(string inputText, string hashedText)
    {
        if(string.IsNullOrWhiteSpace(inputText))
            throw new ArgumentNullException(nameof(inputText), "Input text is null or empty.");

        if (string.IsNullOrWhiteSpace(hashedText))
            throw new ArgumentNullException(nameof(hashedText), "Hashed text is null or empty.");

        return Verify(inputText, hashedText);
    }
}