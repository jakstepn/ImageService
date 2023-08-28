using ImageService.Contracts;
using ImageService.Exceptions;
using ImageService.Extensions;
using ImageService.Services.Images;
using Microsoft.AspNetCore.Mvc;

namespace ImageService.Controllers;

[Route("/image")]
public class ImageController : ControllerBase
{
    private readonly IImageProvider _imageProvider;
    private readonly ILogger<ImageController> _logger;

    public ImageController(
        IImageProvider imageProvider,
        ILogger<ImageController> logger)
    {
        _imageProvider = imageProvider;
        _logger = logger;
    }
    
    [HttpGet("{category}")]
    public ActionResult<IEnumerable<Image>> GetImages(string category)
    {
        try
        {
            var images = _imageProvider.GetImages(category, "admin").Select(i => i.ToContractImage());
            return Ok(images);
        }
        catch (InvalidAmountException e)
        {
            _logger.Log(LogLevel.Critical, "{message}, {amount}", e.Message, e.Amount);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{category}/{amount:int}")]
    public ActionResult<Image> GetImages(string category, int amount)
    {
        try
        {
            var images = _imageProvider.GetImages(category, "admin", amount).Select(i => i.ToContractImage());
            return Ok(images);
        }
        catch (InvalidAmountException e)
        {
            _logger.Log(LogLevel.Critical, "{message}, {amount}", e.Message, e.Amount);
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public ActionResult<Image> SaveImage(Image image)
    {
        try
        {
            _imageProvider.AddImage(image.ToImage("admin"));
            return Ok(image);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Critical, "{message}", e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("{category}/{id}")]
    public ActionResult<Image> DeleteImage(string category, string id)
    {
        try
        {
            _imageProvider.RemoveImage(category, id, "admin");
            return Ok();
        }
        catch (CategoryNotFoundException e)
        {
            _logger.Log(LogLevel.Critical, "{message}", e.Message);
            return BadRequest(e.Message);
        }
    }
}