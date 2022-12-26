
using Business.Entities.ProductPhotoPath;
using Business.Interface;
using Business.Interface.ProductImages;
using ERP.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Web.Mvc;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;



namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    //[DisplayName("UploadProductImages")]
    public class UploadProductImagesController : SettingsController
    {
        private readonly IMasterService _masterService;
        private readonly IProductImages _iProductImages;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly string link;
        private readonly IProductImages _productImages;

        public UploadProductImagesController(IMasterService masterService, IProductImages iProductImages, IHostEnvironment hostEnvironment, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IProductImages productImages)
        {
            _masterService = masterService;
            _iProductImages = iProductImages;
            _hostEnvironment = hostEnvironment;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            link = _configuration.GetSection("ProductImagePath")["UploadProductImages"];
            _productImages = productImages;
         
        }

        public IActionResult Index()
        {
            //List<string> pathList = new List<string>();
            List<ProductPhotoPath> imagePath = _productImages.GetImagePath().Result;
            //foreach (var path in imagePath)
            //{
            //    pathList.Add(path.ImagePath);
            //}
            return View(imagePath);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult AddOrEditProductImages(int id)
        {
            ProductPhotoPath productPhotoPath = new ProductPhotoPath();
            var listUOMID = _masterService.GetAllUOMID();
            ViewData["UOMID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(listUOMID, "UOMID", "UOMText");

            var listItemCategory = _masterService.GetAllItemCategory();
            ViewData["ItemCategory"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(listItemCategory, "ItemCategoryID", "ItemCategoryText");

            if (id > 0)
            {
               productPhotoPath = _iProductImages.GetProductImageDetailAsync(id).Result;
            }
            else 
            {
                return View("AddOrEditProductImages", productPhotoPath);
            }

            return View("AddOrEditProductImages", productPhotoPath);
        }


        [HttpPost]
        public ActionResult AddOrEditProductImages(ProductPhotoPath productPhotoPath) 
        {
            var path1 = "";
            productPhotoPath.CreatedOrModifiedBy = USERID;
            if (productPhotoPath.ProductPhoto != null)
            {
                string text1 = DateTime.Now.ToString();
                string fileExtension = Path.GetExtension(productPhotoPath.ProductPhoto.FileName);
                // Add logic for save file in image folder. 29-09-2022.
                path1 = _webHostEnvironment.WebRootPath + link;  //full path Excluding file name ----  0
                string filepath = path1;  //full path including file name  -----  1
                string filename = USERID + "_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace("-", "").Replace(":", "") + "_" + productPhotoPath.ProductPhoto.FileName;
                string dbfilepath = link + filename;
                filepath = filepath + filename;
                //productPhotoPath.ProductImageText = productPhotoPath.ProductPhoto.FileName;
               // productPhotoPath.ProductImageID = id;
                productPhotoPath.ImagePath = dbfilepath;
                productPhotoPath.CreatedOrModifiedBy = USERID;
                productPhotoPath.IsActive = true;

                var id = _iProductImages.AddOrEditProductImages(productPhotoPath).Result;

                if (id > 0 && Directory.Exists(path1))
                {
                    using (var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        productPhotoPath.ProductPhoto.CopyTo(fileStream);
                    }
                    if (id <= 0)
                        return View(ViewData["Message"] = "Profile photo not uploaded.");
                    else
                        return RedirectToAction("Index", "UploadProductImages");
                }
                return View(ViewData["Message"] = "Root directory not found.");
            }
            else
                return RedirectToAction("Index", "UploadProductImages");
        }

    }
}
