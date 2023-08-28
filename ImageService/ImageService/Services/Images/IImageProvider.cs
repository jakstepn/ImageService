using ImageService.Models;

namespace ImageService.Services.Images;

public interface IImageProvider
{
    IEnumerable<Image> GetImages(string category, string ownerId, int amount = -1);
    void AddImage(Image image);
    void RemoveImage(string category, string id, string ownerId);
}