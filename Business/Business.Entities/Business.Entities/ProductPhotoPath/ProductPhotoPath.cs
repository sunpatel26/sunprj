using Microsoft.AspNetCore.Http;

namespace Business.Entities.ProductPhotoPath
{
    public class ProductPhotoPath
    {
        public int ProductImageID { get; set; }
        public string ProductImageText { get; set; }
        public string ImagePath { get; set; }
        public IFormFile ProductPhoto { get; set; }
        public int UOMID { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedOrModifiedBy { get; set; }

        public int ItemCategoryID { get; set; }

    }
}