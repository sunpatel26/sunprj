using Business.Interface;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using ERP.Controllers;
using ERP.Extensions;
using ERP.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaseController : SettingsController
    {
        private static HttpContext Current => new HttpContextAccessor().HttpContext;
        private static ISuperAdminService _admin => (ISuperAdminService)Current.RequestServices.GetService(typeof(ISuperAdminService));

        public IActionResult GetState(int countryId)
        {
            try
            {
                var states = SuperCompanyDropdownBinder.State(countryId);

                return Json(new { status = true, data = states });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }
        public IActionResult GetDistrict(int stateId)
        {
            try
            {
                var states = SuperCompanyDropdownBinder.District(stateId);

                return Json(new { status = true, data = states });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }
        public IActionResult GetCity(int stateId)
        {
            try
            {
                var states = SuperCompanyDropdownBinder.City(stateId);

                return Json(new { status = true, data = states });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }
        public IActionResult GetTaluka(int districtID)
        {
            try
            {
                var states = SuperCompanyDropdownBinder.Taluka(districtID);

                return Json(new { status = true, data = states });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }
        public IActionResult GetZipcode(string q)
        {
            try
            {
                var zipcode = _admin.GetAllZipcodeAutoCompletAsync(q).Result;
                //var customers = (from a in zipcode select new 
                //                                        {
                //                label = customer.ContactName,
                //                val = customer.CustomerID
                //           }).ToList();
                return Json(zipcode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }
    }
}
