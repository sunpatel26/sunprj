using Business.Entities;
using Business.Interface;
using Business.SQL;
using DocumentFormat.OpenXml.Wordprocessing;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridMvc.Server;
using GridShared;
using GridShared.Sorting;
using GridShared.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace ERP.Areas.SuperAdmin.Controllers
{


    [Area("SuperAdmin"), Authorize]
    [DisplayName("Compnay")]
    public class CompanyController : BaseController
    {
        #region "Variable
        private readonly UserManager<UserMasterMetadata> _userManager;
        private readonly SignInManager<UserMasterMetadata> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISiteCompanyRepository _companyManager;
        public CompanyController(ISiteCompanyRepository companyManager, UserManager<UserMasterMetadata> userManager, SignInManager<UserMasterMetadata> signInManager, IWebHostEnvironment hostEnvironment)
        {

            this._userManager = userManager;
            this._signInManager = signInManager;
            _webHostEnvironment = hostEnvironment;
            _companyManager = companyManager;
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        #endregion

        #region "Compnay"
        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            try
            {
                int userid = USERID;
                IQueryCollection query = Request.Query;

                Action<IGridColumnCollection<CompanyMasterMetadata>> columns = c =>
                {
                    c.Add(o => o.CompanyName)
                        .Titled("Company Name")
                        .SortInitialDirection(GridSortDirection.Ascending)
                        //.ThenSortByDescending(o => o.CompanyID)
                        .SetWidth(110);


                    c.Add(o => o.CompanyCode)
                        .Titled("CompanyCode")
                        .SetWidth(250);

                    c.Add()
                        .Encoded(false)
                        .Sanitized(false)
                        .SetWidth(60)
                        .RenderValueAs(o => $"<a class='btn' href='Company/Edit/{o.CompanyID}' ><i class='bx bx-edit'></i></a>");


                };
                PagedDataTable<CompanyMasterMetadata> pds = _companyManager.GetAllCompanyAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
                var server = new GridCoreServer<CompanyMasterMetadata>(pds, query, false, "ordersGrid",
                    columns, PAGESIZE, pds.TotalItemCount)
                    .Sortable()
                    //.Filterable()
                    //.WithMultipleFilters()
                    .Searchable(true, false)
                    //.Groupable(true)
                    .ClearFiltersButton(true)
                    //.Selectable(true)
                    .SetStriped(true)
                    .ChangePageSize(true)
                    .WithGridItemsCount()
                    .WithPaging(PAGESIZE, pds.TotalItemCount)
                    .ChangeSkip(false)
                    .EmptyText("No record found")
                    ;
                return View(server.Grid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public IActionResult Register()
        {
            return View("Register", new CompanyMasterMetadata());
        }
        public IActionResult Edit(int id)
        {
            try
            {
                CompanyMasterMetadata model = _companyManager.GetCompnayAsync(id.ToString()).Result;
                return View("Edit", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> Register(CompanyMasterMetadata model)
        {
            try
            {
                if (model.ImageFile != null)
                {
                    string uniqueFileName = UploadedFile(model);
                    model.CompanyLogoImagePath = uniqueFileName;
                    model.CompanyLogoName = uniqueFileName;
                }

                int compnayID = _companyManager.CreateOrUpdateCompanyAsync(model).Result;
                if (compnayID > 0 && model.CompanyID == 0)
                {
                    model.CompanyID = compnayID;
                    var userCheck = await _userManager.FindByEmailAsync(model.Email);
                    if (userCheck == null)
                    {
                        var user = new UserMasterMetadata
                        {
                            Forename = model.FirstName,
                            Surname = model.LastName,
                            Username = model.Email,
                            NormalizedUserName = model.Email,
                            Email = model.Email,
                            PhoneNumber = model.Mobile,
                            EmailConfirmed = true,
                            PhoneNumberConfirmed = true,
                            CompanyID = model.CompanyID,
                            IsActive = true
                        };
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, "Admin");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            if (result.Errors.Count() > 0)
                            {
                                foreach (var error in result.Errors)
                                {
                                    ModelState.AddModelError("message", error.Description);
                                }
                            }
                            return View(model);
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        private string UploadedFile(CompanyMasterMetadata model)
        {
            string filePath, fileName = null;
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "companylogo");
            if (!string.IsNullOrEmpty(model.CompanyLogoName))
            {
                filePath = Path.Combine(uploadsFolder, model.CompanyLogoName);
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }
            if (model.ImageFile != null)
            {

                fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(model.ImageFile.FileName);
                filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        public async Task<IActionResult> ShowLogo()
        {
            try
            {
                CompanyLogoMasterMetadata model = null;
                model = await _companyManager.GetCompanyLogoMaster(COMPANYID);
                if (model != null)
                {
                    string filePath;
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "companylogo");
                    if (!string.IsNullOrEmpty(model.CompanyLogoName))
                    {
                        filePath = Path.Combine(uploadsFolder, model.CompanyLogoName);
                        if (System.IO.File.Exists(filePath))
                        {

                            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                            return new FileStreamResult(new System.IO.MemoryStream(bytes), "image/png");
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion

        #region "Contact"

        [HttpPost]
        public async Task<PartialViewResult> AddContact(int id,int compnayid)
        {
            try
            {
                CompanyContactTxnMetadata model = await _companyManager.GetCompnayContactAsync(compnayid, id);
                if (model == null)
                    model = new CompanyContactTxnMetadata() { CompanyID= compnayid };
                return PartialView("_addContact", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveContact(CompanyContactTxnMetadata model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //model.CompanyID = COMPANYID;
                    int i = await _companyManager.CreateOrUpdateCompanyContactAsync(model);
                    if (i>0)
                    {
                        if (model.CompanyContactPersonsID == 0)
                            return Json(new { status = true, message = MessageHelper.Added });
                        else
                            return Json(new { status = true, message = MessageHelper.Updated });
                    }                   
                }
                return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }
        #endregion

        #region "Address"

        [HttpPost]
        public async Task<PartialViewResult> AddAddress(int id, int compnayid)
        {
            try
            {
                CompanyAddressTxnMetadata model = await _companyManager.GetCompnayAddressAsync(compnayid, id);
                if (model == null)
                    model = new CompanyAddressTxnMetadata() { CompanyID = compnayid };
                return PartialView("_addAddress", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveAddress(CompanyAddressTxnMetadata model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedOrModifiedBy = USERID;
                    int i = await _companyManager.CreateOrUpdateCompanyAddressAsync(model);
                    if (i > 0)
                    {
                        if (model.CompanyAddressTxnID == 0)
                            return Json(new { status = true, message = MessageHelper.Added });
                        else
                            return Json(new { status = true, message = MessageHelper.Updated });
                    }
                }
                return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }
        #endregion
    }
}