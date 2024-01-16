using BlogApp.Core.Intefaces;
using static BCrypt.Net.BCrypt;

namespace BlogApp.Core;

public class PasswordService : IPasswordService
{
    public string GeneratePasswordHash(string text)
    {
        return HashPassword(text);
    }

    public bool VerifyPasswordMatch(string inputText, string hashedText)
    {
        return Verify(inputText, hashedText);
    }
}