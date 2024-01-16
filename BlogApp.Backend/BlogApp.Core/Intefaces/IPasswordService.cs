namespace BlogApp.Core.Intefaces;

public interface IPasswordService
{
    string GeneratePasswordHash(string text);
    bool VerifyPasswordMatch(string inputText, string hashedText);
}