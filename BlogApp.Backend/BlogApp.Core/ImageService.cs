using BlogApp.Core.Intefaces;
using Microsoft.Extensions.Configuration;

namespace BlogApp.Core;

public class ImageService : IImageService
{
    private readonly IConfiguration _configuration;

    public ImageService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateImage(string imageBase64)
    {
        try
        {
            if (string.IsNullOrEmpty(imageBase64))
                return null!;

            var path = _configuration.GetSection("ImageStoragePath")?.Value?.ToString();

            if (!string.IsNullOrWhiteSpace(path) && Directory.Exists(path))
            {
                var imageExtension = imageBase64.Substring(imageBase64.IndexOf('/') + 1, imageBase64.IndexOf(';') - imageBase64.IndexOf('/') - 1);

                var imageName = $"{Guid.NewGuid()}.{imageExtension}";

                var fullPath = Path.Combine(path, imageName);

                var rawBase64 = imageBase64.Remove(0, imageBase64.IndexOf(",") + 1);

                var imageData = Convert.FromBase64String(rawBase64);

                using var image = new FileStream(fullPath, FileMode.Create, FileAccess.Write);

                image.Write(imageData, 0, imageData.Length);
                image.Flush();

                return imageName;
            }

            throw new Exception("Invalid ImageStoragePath provided");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public string GetImage(string imageName)
    {
        try
        {
            if (string.IsNullOrEmpty(imageName))
                return null!;

            var path = _configuration.GetSection("ImageStoragePath")?.Value?.ToString();

            if (!string.IsNullOrWhiteSpace(path) && Directory.Exists(path))
            {
                var imagePath = Path.Combine(path, imageName);

                using var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read);

                var imageBuffer = new byte[fs.Length];

                fs.Read(imageBuffer, 0, imageBuffer.Length);

                var imageBase64 = Convert.ToBase64String(imageBuffer);

                return imageBase64;
            }

            throw new Exception("Invalid ImageStoragePath provided.");
        }
        catch (Exception)
        {
            throw;
        }
    }
}