using Business.Entities.ProductPhotoPath;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.ProductImages
{
    public interface IProductImages
    {
        Task<PagedDataTable<ProductPhotoPath>> GetImagePath();
        Task<int> AddOrEditProductImages(ProductPhotoPath productPhotoPath);
        Task<int> UpdateProductPhoto(ProductPhotoPath productPhotoPath);

        Task<ProductPhotoPath> GetProductImageDetailAsync(int ProductImageID);
        //public Task<ProductPhotoPath> GetImagePath();
    }
}
