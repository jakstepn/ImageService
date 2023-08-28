using ContractImage = ImageService.Contracts.Image;
using ModelImage = ImageService.Models.Image;

namespace ImageService.Extensions;

public static class ContractToImageAdapter
{
    public static ModelImage ToImage(this ContractImage image, string ownerId)
    {
        return new ModelImage
        {
            Base64 = image.Base64,
            Category = image.Category,
            Id = image.Id,
            OwnerId = ownerId,
        };
    }
}