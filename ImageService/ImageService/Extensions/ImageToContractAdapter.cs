using ContractImage = ImageService.Contracts.Image;
using ModelImage = ImageService.Models.Image;

namespace ImageService.Extensions;

public static class ImageToContractAdapter
{
    public static ContractImage ToContractImage(this ModelImage image)
    {
        return new ContractImage
        {
            Base64 = image.Base64,
            Category = image.Category
        };
    }
}