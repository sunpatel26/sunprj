using Business.Entities.Customer;
using Business.Entities.Employee;
using Business.Interface;
using Business.Interface.ICustomer;
using Business.SQL;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class CustomerController : SettingsController
    {
        private readonly IMasterService _masterService;
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string linkCustomerLogoImage;
        public CustomerController(IMasterService masterService, IConfiguration configuration,IHostEnvironment hostEnvironment, IWebHostEnvironment webHostEnvironment, ICustomerService customerService)
        {
            _masterService = masterService;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
            _webHostEnvironment = webHostEnvironment;
            _customerService = customerService;
            linkCustomerLogoImage = _configuration.GetSection("CustomerLogoImagePath")["CustomerLogoImage"];
        }
        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;
            Action<IGridColumnCollection<CustomerMaster>> columns = c =>
            {
                c.Add(o => o.SrNo, "SrNo").Titled("Sr.No.").SetWidth(20);
                c.Add(o => o.CustomerCode).Titled("Customer Code").Sortable(true);
                c.Add(o => o.CustomerName).Titled("Customer Name").Sortable(true);
                c.Add(o => o.GroupName).Titled("Group Name").Sortable(true);
                c.Add(o => o.OwnerName).Titled("Owner Name").Sortable(true);
                c.Add(o => o.IsActive).Titled("IsActive").Sortable(true);
                //Below code hide on phones
                c.Add().Titled("Details").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs")
                .RenderValueAs(o => $"<a class='btn' href='/Admin/Customer/AddUpdateCustomer/{o.CustomerID}' ><i class='bx bx-edit'></i></a>");
            };
            PagedDataTable<CustomerMaster> pds = _customerService.GetAllCustomerAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<CustomerMaster>(pds, query, false, "ordersGrid",
                columns, PAGESIZE, pds.TotalItemCount)
                .Sortable().ClearFiltersButton(true).Selectable(true).WithGridItemsCount().ChangeSkip(false).EmptyText("No record found").ClearFiltersButton(false);
            return View(server.Grid);
        }

        #region Basic details
        [HttpGet]
        public ActionResult AddUpdateCustomer(int id)
        {
            try
            {
                CustomerMaster customerMaster = new CustomerMaster();
                if (id > 0)
                {
                    customerMaster = _customerService.GetCustomerAsync(id).Result;
                    ViewData["LogoImage"] = customerMaster.LogoImagePath;
                }
                return View("AddUpdateCustomer", customerMaster);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateCustomer(CustomerMaster customerMaster)
        {

            var path1 = "";
            var id = await _customerService.AddUpdateCustomer(customerMaster);

            if (id > 0)
            {
                if (customerMaster.LogoImage != null)
                {
                    string fileExtension = Path.GetExtension(customerMaster.LogoImage.FileName);
                     //Add logic for save file in image folder. 29-09-2022.
                    CustomerLogoImage customerLogoImage = new CustomerLogoImage();
                    path1 = _webHostEnvironment.WebRootPath + linkCustomerLogoImage;  //full path Excluding file name ----  0
                    string filepath = path1;  //full path including file name  -----  1
                    string filename = id + "_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace("-", "").Replace(":", "") + "_" + customerMaster.LogoImage.FileName;
                    string dbfilepath = linkCustomerLogoImage + filename;
                    filepath = filepath + filename;
                    customerLogoImage.CustomerID = id;
                    customerLogoImage.LogoImageName = filename;
                    customerLogoImage.LogoImagePath = dbfilepath;
                    customerLogoImage.CreatedOrModifiedBy = USERID;
                    customerLogoImage.IsActive = true;
                    if (Directory.Exists(path1))
                    {
                        using (var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
                        {
                            customerMaster.LogoImage.CopyTo(fileStream);
                        }
                        var profilePhotoId = _customerService.UpdateCustomerLogoImage(customerLogoImage).Result;
                        //return Json(new { status = false, message = MessageHelper.Error });
                        return RedirectToAction("AddUpdateCustomer", new { id = id });
                    }
                }
                //return Json(new { status = true, message = MessageHelper.Added });
                return RedirectToAction("AddUpdateCustomer", new { id = id });

            }
            else
            {
                //return Json(new { status = false, message = MessageHelper.Error });
                return RedirectToAction("AddUpdateCustomer");
            }
        }
        #endregion Basic details

        #region Contact Person Detail
        [HttpGet]
        public async Task<PartialViewResult> AddUpdateCustomerContactPerson(int customerContactID, int customerId)
        {
            try
            {
                CustomerContactTxn customerContactTxn = new CustomerContactTxn();
                customerContactTxn.CustomerID = customerId;
                if (customerContactID > 0 || customerId > 0)
                {
                    var getCustomerContactTxn = await _customerService.GetCustomerContactPerson(customerContactID, customerId);
                    if (getCustomerContactTxn == null)
                        customerContactTxn.CustomerID = customerId;
                    else
                        customerContactTxn = getCustomerContactTxn;

                    return PartialView("_addUpdateCustomerContactPerson", customerContactTxn);
                }
                else
                    return PartialView("_addUpdateCustomerContactPerson", customerContactTxn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateCustomerContactPerson(CustomerContactTxn customerContactTxn)
        {
            try
            {
                int customerContactId = await _customerService.AddUpdateCustomerContactPerson(customerContactTxn);
                if (customerContactId > 0)
                {
                    //return RedirectToAction("AddUpdateCustomer", new { id = customerContactTxn.CustomerID });
                    return Json(new { status = true, message = MessageHelper.Added });
                }
                else
                    return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion Contact Person Detail
    }
}
