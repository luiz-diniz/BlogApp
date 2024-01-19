namespace BlogApp.Core.Intefaces;

public interface IImageService
{
    string CreateImage(string imageBase64, string appSettingsPathSection);
    string GetImage(string imageName, string appSettingsPathSection);
}