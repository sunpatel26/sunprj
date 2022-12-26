using Business.Entities.OurProduct;
using Business.Entities.ProductPhotoPath;
using Business.Interface;
using Business.Interface.ProductImages;
using ERP.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
//using PartialViewResult = System.Web.Mvc.PartialViewResult;
//using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    [DisplayName("OurProduct")]
    public class OurProductController : SettingsController
    {
        private readonly IMasterService _masterService;
        private readonly IProductImages _productImages;

        public OurProductController(IMasterService masterService, IProductImages productImages)
        {
            _masterService = masterService;
            _productImages = productImages;
        }
            public IActionResult Index()
        {
            List<ProductPhotoPath> imagePath = _productImages.GetImagePath().Result;
            return View(imagePath);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            try
            {
                OurProduct model = new OurProduct();
                var listUOMID = _masterService.GetAllUOMID();
                ViewData["UOMID"] = new SelectList(listUOMID, "UOMID", "UOMText");

                if (id > 0)
                {
                    //model = _iMarketingCompanyFinancialYear.GetFinancialYearAsync(id).Result;

                    return PartialView("AddOurProductImages", model);
                }
                else
                {
                    return PartialView("AddOurProductImages", model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        
    }
}
