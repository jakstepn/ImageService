namespace ImageService.Services.Categories;

public interface ICategoryProvider
{
    public IEnumerable<string> GetCategories();
}