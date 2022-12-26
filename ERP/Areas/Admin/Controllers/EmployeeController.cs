using AspNetCore;
using Business.Entities.Employee;
using Business.Interface;
using Business.Interface.IEmployee;
using Business.SQL;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using ERP.Controllers;
using ERP.Extensions;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class EmployeeController : SettingsController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMasterService _masterService;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string linkEmployeeImage;
        private readonly string linkEmployeeDocument;
        public EmployeeController(IEmployeeService employeeService, IMasterService masterService, IConfiguration configuration, IHostEnvironment hostEnvironment, IWebHostEnvironment webHostEnvironment)
        {
            _employeeService = employeeService;
            _masterService = masterService;
            _configuration = configuration;
            linkEmployeeImage = _configuration.GetSection("EmployeeImagePath")["EmployeeImages"];
            linkEmployeeDocument = _configuration.GetSection("EmployeeImagePath")["EmployeeDocuments"];
            _hostEnvironment = hostEnvironment;
            _webHostEnvironment = webHostEnvironment;
        }
        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;
            string value = string.Empty;
            Action<IGridColumnCollection<EmployeeMaster>> columns = c =>
            {
                c.Add(o => o.SrNo, "SrNo").Titled("Sr.No.").SetWidth(20);
                c.Add(o => o.EmployeeCode).Titled("Employee Code").Sortable(true);
                c.Add(o => o.EmployeeName).Titled("Name").Sortable(true);
                c.Add(o => o.GenderText).Titled("Gender").Sortable(false);
                c.Add(o => o.IsActive).Titled("IsActive").Sortable(true);
                //Below code hide on phones
                c.Add().Titled("Details").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs")
                .RenderValueAs(o => $"<a class='btn' href='/Admin/Employee/AddUpdateEmployee/{o.EmployeeID}' ><i class='bx bx-edit'></i></a>");
            };
            PagedDataTable<EmployeeMaster> pds = _employeeService.GetAllEmployeesAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<EmployeeMaster>(pds, query, false, "ordersGrid",
                columns, PAGESIZE, pds.TotalItemCount)
                .Sortable().ClearFiltersButton(true).Selectable(true).WithGridItemsCount().ChangeSkip(false).EmptyText("No record found").ClearFiltersButton(false);
            return View(server.Grid);
        }

        #region Basic details
        [HttpGet]
        public ActionResult AddUpdateEmployee(int id)
        {
            try
            {
                AddUpdateEmployee addUpdateEmployee = new AddUpdateEmployee();
                if (id > 0)
                {
                    addUpdateEmployee = _employeeService.GetEmployeeAsync(id).Result;
                    ViewData["Image"] = addUpdateEmployee.ImagePath;
                }

                var listDepartment = _masterService.GetAllDepartments();
                ViewData["DepartmentIdName"] = new SelectList(listDepartment, "DepartmentID", "DepartmentName");

                var listDesignation = _masterService.GetAllDesignations();
                ViewData["DesignationIdText"] = new SelectList(listDesignation, "DesignationID", "DesignationText");


                var listEmployees = _masterService.GetAllEmployees();
                ViewData["EmployeeIdName"] = new SelectList(listEmployees, "EmployeeID", "EmployeeName");

                var genders = _masterService.GetAllGenders();
                ViewData["GenderIdText"] = new SelectList(genders, "GenderID", "GenderText");

                var emailGroupMaster = _masterService.GetAllEmailGroupMaster();
                ViewData["EmailGroupName"] = new SelectList(emailGroupMaster, "EmailGroupID", "EmailGroupName");

                return View("AddUpdateEmployee", addUpdateEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployee(AddUpdateEmployee addUpdateEmployee)
        {
            var path1 = "";
            addUpdateEmployee.CompanyID = COMPANYID;
            addUpdateEmployee.CreatedOrModifiedBy = USERID;
            var id = await _employeeService.AddUpdateEmployee(addUpdateEmployee);

            if (id > 0)
            {
                if (addUpdateEmployee.ProfilePhoto != null)
                {
                    string fileExtension = Path.GetExtension(addUpdateEmployee.ProfilePhoto.FileName);
                    // Add logic for save file in image folder. 29-09-2022.
                    EmployeeProfileImage employeeProfileImage = new EmployeeProfileImage();

                    path1 = _webHostEnvironment.WebRootPath + linkEmployeeImage;  //full path Excluding file name ----  0
                    string filepath = path1;  //full path including file name  -----  1
                    string filename = id + "_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace("-", "").Replace(":", "") + "_" + addUpdateEmployee.ProfilePhoto.FileName;
                    string dbfilepath = linkEmployeeImage + filename;
                    filepath = filepath + filename;

                    employeeProfileImage.EmployeeID = id;
                    employeeProfileImage.ImageName = filename;
                    employeeProfileImage.ImagePath = dbfilepath;
                    employeeProfileImage.CreatedOrModifiedBy = USERID;
                    employeeProfileImage.IsActive = true;

                    if (Directory.Exists(path1))
                    {
                        using (var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
                        {
                            addUpdateEmployee.ProfilePhoto.CopyTo(fileStream);
                        }

                        var profilePhotoId = _employeeService.UpdateEmployeeProfilePhoto(employeeProfileImage).Result;

                        //return Json(new { status = false, message = MessageHelper.Error });
                        return RedirectToAction("AddUpdateEmployee", new { id = id });
                    }
                }
                //return Json(new { status = true, message = MessageHelper.Added });
                return RedirectToAction("AddUpdateEmployee", new { id = id });
            }
            else
            {
                //return Json(new { status = false, message = MessageHelper.Error });
                return RedirectToAction("AddUpdateEmployee");
            }
        }
        #endregion Basic details

        #region Address details
        [HttpGet]
        public async Task<PartialViewResult> AddUpdateEmployeeAddress(int employeeAddressTxnId, int employeeId)
        {
            try
            {
                EmployeeAddressTxn addressMaster = new EmployeeAddressTxn();
                addressMaster.EmployeeID = employeeId;
                if (employeeAddressTxnId > 0 || employeeId > 0)
                {
                    var employeeAddressTxn = await _employeeService.GetEmployeeAddressTxn(employeeAddressTxnId, employeeId);
                    if (employeeAddressTxn == null)
                        addressMaster.EmployeeID = employeeId;
                    else
                        addressMaster = employeeAddressTxn;

                    return PartialView("_addUpdateEmployeeAddress", addressMaster);
                }
                else
                    return PartialView("_addUpdateEmployeeAddress", addressMaster);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeAddress(EmployeeAddressTxn addressMaster)
        {
            try
            {
                int addressId = await _employeeService.CreateOrUpdateEmployeeAddressAsync(addressMaster);
                if (addressId > 0)
                {
                    return RedirectToAction("AddUpdateEmployee", new { id = addressMaster.EmployeeID });
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
        #endregion Address details

        #region Family background

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeFamilyBackground(EmployeeFamilyDetail employeeFamilyDetail)
        {
            try
            {
                employeeFamilyDetail.CreatedModifiedBy = USERID;
                int familyId = await _employeeService.CreateOrUpdateEmployeeFamilyBackgroundAsync(employeeFamilyDetail);
                if (familyId > 0)
                {
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

        #endregion Family background

        #region Employee banking detail

        [HttpGet]
        public async Task<PartialViewResult> AddUpdateEmployeeBankAccount(int employeeBankDetailId, int employeeId)
        {
            try
            {
                EmployeeBankDetails employeeBankDetail = await _employeeService.GetEmployeeBankAccount(employeeBankDetailId, employeeId);

                if (employeeBankDetail == null)
                    employeeBankDetail = new EmployeeBankDetails { EmployeeID = employeeId };

                return PartialView("_addUpdateEmployeeBankAccount", employeeBankDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeBankAccount(EmployeeBankDetails employeeBankDetails)
        {
            try
            {
                employeeBankDetails.CreatedOrModifiedBy = USERID;
                int familyId = await _employeeService.CreateOrUpdateEmployeeBankDetail(employeeBankDetails);
                if (familyId > 0)
                {
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
        #endregion Employee banking detail

        #region Employee document
        [HttpGet]
        public async Task<PartialViewResult> AddUpdateEmployeeDocument(int employeeDocumentId, int employeeId)
        {
            try
            {
                EmployeeDocument employeeDocument = await _employeeService.GetEmployeeDocument(employeeDocumentId, employeeId);

                if (employeeDocument == null)
                    employeeDocument = new EmployeeDocument { EmployeeID = employeeId };

                ViewData["DocumentPath"] = employeeDocument.DocumentPath;

                return PartialView("_addUpdateEmployeeDocument", employeeDocument);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateEmployeeDocument(EmployeeDocument employeeDocument)
        {
            try
            {
                int id = employeeDocument.EmployeeID;
                if (employeeDocument.DocumentFile != null)
                {
                    var path1 = "";
                    string fileExtension = Path.GetExtension(employeeDocument.DocumentFile.FileName);

                    path1 = _webHostEnvironment.WebRootPath + linkEmployeeDocument;  //full path Excluding file name ----  0
                    string filepath = path1;  //full path including file name  -----  1
                    string filename = employeeDocument.DocumentName + "_" + employeeDocument.EmployeeID + "_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace("-", "").Replace(":", "") + fileExtension;
                    string dbfilepath = linkEmployeeImage + filename;
                    filepath = filepath + filename;

                    if (Directory.Exists(path1))
                    {
                        using (var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
                        {
                            employeeDocument.DocumentFile.CopyTo(fileStream);
                        }
                        employeeDocument.DocumentName = filename;
                        employeeDocument.DocumentExtension = fileExtension;
                        employeeDocument.DocumentPath = dbfilepath;
                        int documentId = await _employeeService.CreateOrUpdateEmployeeDocument(employeeDocument);
                        if (documentId > 0)
                        {
                            //TempData["message"] = MessageHelper.Uploaded;
                            //ViewBag.Message = MessageHelper.Uploaded;
                            //return Json(new { status = true, message = MessageHelper.Uploaded });
                            return RedirectToAction("AddUpdateEmployee", new { id = id });
                        }
                        else
                        {
                            //ViewBag.Message = MessageHelper.Uploaded;
                            //return Json(new { status = false, message = MessageHelper.NoDocument });
                            return RedirectToAction("AddUpdateEmployee", new { id = id });
                        }
                    }
                }
                //ViewBag.Message = MessageHelper.NoDocument;
                //return Json(new { status = false, message = MessageHelper.Error });
                return RedirectToAction("Index", "Employee");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> ActiveInActiveEmployeeDocument(int employeeDocumentId, int employeeId, bool isActive)
        {
            EmployeeDocument employeeDocument = new EmployeeDocument()
            {
                EmployeeDocumentID = employeeDocumentId,
                EmployeeID = employeeId,
                IsActive = isActive,
                CreatedOrModifiedBy = USERID
            };
            int modifiedBy = USERID;
            int employeeDocumentIsActive = await _employeeService.ActiveInActiveEmployeeDocument(employeeDocument);
            if (employeeDocumentIsActive > 0)
            {
                if (isActive)
                    return Json(new { status = true, message = MessageHelper.ActivatedDocument });
                else
                    return Json(new { status = true, message = MessageHelper.InactivatedDocument });
            }
            else
                return Json(new { status = true, message = MessageHelper.Error });
        }
        #endregion Employee document
    }
}
