using Business.Entities;
using Business.Interface;
using Business.SQL;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using GridShared.Sorting;
using Kinfo.JsonStore.Builder;
using Kinfo.JsonStore.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    [DisplayName("CompanySetting")]
    public class CompanySettingController : SettingsController
    {
        #region "Variable
        private readonly UserManager<UserMasterMetadata> _userManager;
        private readonly SignInManager<UserMasterMetadata> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISiteCompanyRepository _companyManager;
        public CompanySettingController(ISiteCompanyRepository companyManager, UserManager<UserMasterMetadata> userManager, SignInManager<UserMasterMetadata> signInManager, IWebHostEnvironment hostEnvironment)
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
                        .RenderValueAs(o => $"<a class='btn' href='CompanySetting/Edit/{o.CompanyID}' ><i class='bx bx-edit'></i></a>");


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
        public IActionResult Edit()
        {
            try
            {
                CompanyMasterMetadata model = _companyManager.GetCompnayAsync(COMPANYID.ToString()).Result;
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

                int compnayID = await _companyManager.CreateOrUpdateCompanyAsync(model);
                if (compnayID > 0)
                {
                    int i = await _companyManager.CreateOrUpdateCompanyRegistrationAsync(model);
                }
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
                    return RedirectToAction("Edit");
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
        public async Task<PartialViewResult> AddContact(int id)
        {
            try
            {
                CompanyContactTxnMetadata model = await _companyManager.GetCompnayContactAsync(COMPANYID, id);
                if (model == null)
                    model = new CompanyContactTxnMetadata() { CompanyID = COMPANYID };
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
                    if (i > 0)
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
        public async Task<PartialViewResult> AddAddress(int id)
        {
            try
            {
                CompanyAddressTxnMetadata model = await _companyManager.GetCompnayAddressAsync(COMPANYID, id);
                if (model == null)
                    model = new CompanyAddressTxnMetadata() { CompanyID = COMPANYID };
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
                    model.CreatedOrModifiedBy= USERID;
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

        #region "Bank"

        [HttpPost]
        public async Task<PartialViewResult> AddBank(int id)
        {
            try
            {
                CompanyBankingMetadata model = await _companyManager.GetCompanyBankingAsync(COMPANYID, id);
                if (model == null)
                    model = new CompanyBankingMetadata() { CompanyID = COMPANYID };
                return PartialView("_addBank", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveBank(CompanyBankingMetadata model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedOrModifiedBy = USERID;
                    int i = await _companyManager.CreateOrUpdateCompanyBankingAsync(model);
                    if (i > 0)
                    {
                        if (model.CompanyBankingID == 0)
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

        #region "Documents"

        [HttpPost]
        public async Task<PartialViewResult> AddDocument(int id)
        {
            try
            {
                CompanyDocumentMetadata model = await _companyManager.GetDocumentAsync(COMPANYID, id);
                if (model == null)
                    model = new CompanyDocumentMetadata() { CompanyID = COMPANYID };
                return PartialView("_addDocuments", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveDocument(int CompanyDocumentsID,int DocumentTypeID,string DocumentName,string DocumentDesc,bool IsActive,IList<IFormFile> files)
        {
            try
            {
                CompanyDocumentMetadata model = new CompanyDocumentMetadata();
                model.CompanyID = COMPANYID;
                model.CompanyDocumentsID = CompanyDocumentsID;
                model.DocumentName = DocumentName;
                model.DocumentDesc = DocumentDesc;
                model.IsActive = IsActive;
                model.DocumentPath = UploadedDocFile(files);

                if (ModelState.IsValid)
                {
                    int i = await _companyManager.CreateOrUpdateCompanyDocumentAsync(model);
                    if (i > 0)
                    {
                        if (model.CompanyDocumentsID == 0)
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
        private string UploadedDocFile(IList<IFormFile> files)
        {
            string filePath = string.Empty;
            string fileName = null;
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "DocumentFile");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            if (files.Count > 0)
            {
                foreach (var item in files)
                {
                    fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(item.FileName);

                    filePath = Path.Combine(uploadsFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        item.CopyTo(fileStream);
                    }

                    return fileName;
                }
            }
            return filePath;
        }

        public async Task<FileContentResult> DocumentDownload(int id)
        {
            try
            {
                CompanyDocumentMetadata model = await _companyManager.GetDocumentAsync(COMPANYID, id);
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "DocumentFile");
                string filePath = Path.Combine(uploadsFolder, model.DocumentPath);

                var mimeType = this.GetMimeType(model.DocumentPath);

                byte[] fileBytes=null;

                if (System.IO.File.Exists(filePath))
                {
                    fileBytes = System.IO.File.ReadAllBytes(filePath);
                }
                else
                {
                    // Code to handle if file is not present
                }

                return new FileContentResult(fileBytes, mimeType)
                {
                    FileDownloadName = model.DocumentPath
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        private string GetMimeType(string fileName)
        {
            // Make Sure Microsoft.AspNetCore.StaticFiles Nuget Package is installed
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
        #endregion
    }
}
