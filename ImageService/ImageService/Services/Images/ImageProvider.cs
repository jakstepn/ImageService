using ImageService.Data;
using ImageService.Exceptions;
using ImageService.Models;
using ImageService.Services.Categories;

namespace ImageService.Services.Images;

public class ImageProvider : IImageProvider
{    
    private readonly IMongoDbClient _client;
    private readonly ICategoryProvider _categoryProvider;
    
    public ImageProvider(IMongoDbClient client, ICategoryProvider categoryProvider)
    {
        _client = client;
        _categoryProvider = categoryProvider;
    }

    public IEnumerable<Image> GetImages(string category, string ownerId, int amount = -1)
    {
        if (amount == 0 || (amount < 0 && amount != -1))
        {
            throw new InvalidAmountException("Amount should be positive or -1!", amount);
        }

        var images = _client.GetImages(category, ownerId);
        
        if (amount > 0)
        {
            images = images.Take(amount);
        }

        return images;
    }
    
    public void AddImage(Image image)
    {
        var categories = _categoryProvider.GetCategories();

        if (!categories.Contains(image.Category))
        {
            _client.CreateCollection(image.Category);
        }
        
        _client.AddImageToCollection(image);
    }

    public void RemoveImage(string category, string id, string ownerId)
    {
        var categories = _categoryProvider.GetCategories();

        if (categories.Contains(category))
        {
            throw new CategoryNotFoundException();
        }

        _client.RemoveImageFromCollection(category, id, ownerId);
    }
}