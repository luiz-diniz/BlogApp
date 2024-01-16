namespace BlogApp.Core.Intefaces;

public interface IImageService
{
    string CreateImage(string imageBase64);
    string GetImage(string imageName);
}