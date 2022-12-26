using Business.Entities.ProductPhotoPath;
using Business.Interface.ProductImages;
using ERP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ERP.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductImages _productImages;

        public HomeController(ILogger<HomeController> logger, IProductImages productImages)
        {
            _logger = logger;
            _productImages = productImages;
        }

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public IActionResult Image(string id)
        {
            if (id == "BuildingWire")
            {

                return View("BuildingWire");
            }
            else if (id == "SolarCable")
            {
                List<string> pathList = new List<string>();
                List<ProductPhotoPath> imagePath = _productImages.GetImagePath().Result;
                foreach (var path in imagePath)
                {
                    pathList.Add(path.ImagePath);
                }
                return View("SolarCable", imagePath);
            }
            else
            {
                return View("TelephoneCable");
            }
            

            //if (pathList != null)
            //    ViewData["Image"] = pathList;

            //return View("SolarCable", imagePath);
        }

        public dynamic GetPdf()
        {
            string filePath = "~/file/file.pdf";
            Response.Headers.Add("Content-Disposition", "inline; filename=test.pdf");
            return File(filePath, "application/pdf");
        }
    }
}
