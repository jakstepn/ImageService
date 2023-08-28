using ImageService.Data;

namespace ImageService.Services.Categories;

public class CategoryProvider : ICategoryProvider
{
    private readonly IMongoDbClient _client;

    public CategoryProvider(IMongoDbClient client)
    {
        _client = client;
    }

    public IEnumerable<string> GetCategories()
    {
        return _client.GetCollections().Select(col => col["name"].AsString);
    }
}