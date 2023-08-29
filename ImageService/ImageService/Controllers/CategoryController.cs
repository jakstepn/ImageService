using ImageService.Services.Categories;
using Microsoft.AspNetCore.Mvc;

namespace ImageService.Controllers;

[Route("category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryProvider _categoryProvider;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(
        ICategoryProvider categoryProvider,
        ILogger<CategoryController> logger)
    {
        _categoryProvider = categoryProvider;
        _logger = logger;
    }
    
    [HttpGet]
    public IEnumerable<string> GetCategories()
    {
        return _categoryProvider.GetCategories();
    }
}