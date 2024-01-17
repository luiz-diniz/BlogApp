using BlogApp.Core.Enums;
using BlogApp.Core.Intefaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BlogApp.Core;

public class ImageService : IImageService
{
    private readonly ILogger<ImageService> _logger;
    private readonly IConfiguration _configuration;

    public ImageService(ILogger<ImageService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public string CreateImage(string imageBase64, string appSettingsPathSection)
    {
        try
        {
            if (string.IsNullOrEmpty(imageBase64))
                return null!;

            var path = _configuration.GetSection(appSettingsPathSection)?.Value?.ToString();

            if (!string.IsNullOrWhiteSpace(path))
            {
                if (!Directory.Exists(path))
                    throw new DirectoryNotFoundException($"Directory: {path} not found.");

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

            throw new Exception($"Invalid AppSettings section [{appSettingsPathSection}] provided");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public string GetImage(string imageName, string appSettingsPathSection)
    {
        try
        {
            if (string.IsNullOrEmpty(imageName))
                return null!;

            var path = _configuration.GetSection(appSettingsPathSection)?.Value?.ToString();

            if (!string.IsNullOrWhiteSpace(path))
            {
                if (!Directory.Exists(path))
                    throw new DirectoryNotFoundException($"Directory: {path} not found.");

                var imagePath = Path.Combine(path, imageName);

                using var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read);

                var imageBuffer = new byte[fs.Length];

                fs.Read(imageBuffer, 0, imageBuffer.Length);

                var imageBase64 = Convert.ToBase64String(imageBuffer);

                return imageBase64;
            }

            throw new Exception($"Invalid AppSettings section [{appSettingsPathSection}] provided");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}