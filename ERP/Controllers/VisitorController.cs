using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Business.Interface;
//using Microsoft.AspNetCore.Mvc.Rendering;
using Business.Entities;
using System.IO;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Business.Interface.IEmployee;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;

namespace ERP.Controllers
{
    [AllowAnonymous]
    [Route("[controller]/[action]")]
    public class VisitorController : SettingsController
    {
        private IConfiguration _config { get; set; }
        private IVisitorService service { get; set; }
        IEmailService _emailService = null;
        private readonly INotyfService _notyf;
        private readonly IHostingEnvironment _env;
        private readonly IMasterService _masterService;
        private readonly IEmployeeService _employeeService;
        public VisitorController(IConfiguration configuration, IEmailService emailService, INotyfService notyf, IHostingEnvironment hostingEnvironment, IMasterService masterService, IEmployeeService employeeService)
        {
            _config = configuration;
            service = new Business.Service.VisitorService(_config);
            _emailService = emailService;
            _notyf = notyf;
            _env = hostingEnvironment;
            _masterService = masterService;
            _employeeService = employeeService;
        }
        // GET: VisitorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VisitorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VisitorController/Create
        public IActionResult Create()
        {
            var model = new Business.Entities.VisitorMetaData();
            model.CreatedBy = USERID;
            model.MeetingRequestDateTime = System.DateTime.Now;
            var idProofList = new Business.Service.MasterService(_config).GetIdentityProofTypeAsync();
            ViewData["IdentityProofType"] = new SelectList(idProofList, "IdentityProofTypeID", "IdentityProofTypeText");
            var vehicleTypeList = new Business.Service.MasterService(_config).GetVehicleTypeAsync();
            ViewData["VehicleTypeID"] = new SelectList(vehicleTypeList, "VehicleTypeID", "VehicleTypeText");
            var listEmployees = _masterService.GetAllEmployees();
            ViewData["MeetToWhomPersonName"] = new SelectList(listEmployees, "EmployeeID", "EmployeeName");
            return View(model);
        }

        // POST: VisitorController/Create
        [HttpPost]
        public ActionResult Create(Business.Entities.VisitorMetaData model, IFormFile files)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var basePath = Path.Combine(_env.WebRootPath + "\\Files\\" + model.Email + "\\");
                    bool basePathExists = System.IO.Directory.Exists(basePath);
                    if (!basePathExists) Directory.CreateDirectory(basePath);
                    var filName = Path.GetFileNameWithoutExtension(files.FileName);
                    var filePath = Path.Combine(basePath, files.FileName);
                    var extension = Path.GetExtension(files.FileName);
                    if (!System.IO.File.Exists(filePath))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                        {
                            files.CopyToAsync(stream);
                        }
                    }
                    int result = new Business.Service.VisitorService(_config).AddVisitorRequestAsync(model, USERID, files, filePath);
                    if (result > 0)
                    {
                        //send email to whom visitor wants to meet
                        MailRequest request = new MailRequest();
                        request.Subject = "Meeting Request -" + model.MeetingRequestTitle;
                        request.ToEmail = model.MeetToWhomPersonEmail;
                        string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\VisitorRequest.html";
                        StreamReader str = new StreamReader(FilePath);
                        string MailText = str.ReadToEnd();
                        str.Close();
                        MailText = MailText.Replace("#username#", model.MeetToWhomPersonName).Replace("#VisitorName#", model.FirstName + " " + model.LastName);
                        MailText = MailText.Replace("#MeetingRequestTitle#", model.MeetingRequestTitle);
                        MailText = MailText.Replace("#approvallink#", "https://www.w3schools.com/");
                        MailText = MailText.Replace("#CompanyName#", "Industrial Boilers LTD");
                        var filePathCompLogo = Path.Combine(Directory.GetCurrentDirectory(), "/companylogo/logo.png");
                        MailText = MailText.Replace("#CompanyLogo#", "<img src='" + filePathCompLogo + "' alt='Industrial Boilers LTD' />");
                        request.Body = MailText;
                        _emailService.SendEmail(request);

                        //send email to visior for acknowledgement
                        request = new MailRequest();
                        request.Subject = "Acknowledgement of Visitor request form";
                        request.ToEmail = model.Email;
                        FilePath = Directory.GetCurrentDirectory() + "\\Templates\\AcknowledgementToVisitor.html";
                        str = new StreamReader(FilePath);
                        MailText = str.ReadToEnd();
                        str.Close();
                        MailText = MailText.Replace("#Visitor#", model.FirstName + " " + model.LastName);
                        filePathCompLogo = Path.Combine(Directory.GetCurrentDirectory(), "/companylogo/logo.png");
                        MailText = MailText.Replace("#CompanyName#", "Industrial Boilers LTD");
                        MailText = MailText.Replace("#CompanyLogo#", "<img src='" + filePathCompLogo + "' alt='Industrial Boilers LTD' />");
                        request.Body = MailText;
                        _emailService.SendEmail(request);
                    }
                    _notyf.Success("Your request has been successfully created.");
                    return RedirectToAction(nameof(Create));
                }
            }
            catch
            {

            }
            if (model.MeetingRequestDateTime == null && model.MeetingRequestDateTime != System.DateTime.MinValue)
            {
                model.MeetingRequestDateTime = System.DateTime.Now;
            }
            var idProofList = new Business.Service.MasterService(_config).GetIdentityProofTypeAsync();
            ViewData["IdentityProofType"] = new SelectList(idProofList, "IdentityProofTypeID", "IdentityProofTypeText");
            var vehicleTypeList = new Business.Service.MasterService(_config).GetVehicleTypeAsync();
            ViewData["VehicleTypeID"] = new SelectList(vehicleTypeList, "VehicleTypeID", "VehicleTypeText");

            return View(model);
        }

        // GET: VisitorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VisitorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VisitorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VisitorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            try
            {
                var zipcodeData = new Business.Service.MasterService(_config).GetZipCodeAsync(prefix);
                var zipcodes = (from zip in zipcodeData
                                where zip.ZipCode.StartsWith(prefix)
                                select new
                                {
                                    label = zip.ZipCode,
                                    val = zip.ZipCodeID
                                }).ToList();

                return Json(zipcodes);
            }
            catch
            {
                return Json(false);
            }
        }

        public JsonResult EmployeeDetails(int id)
        {
            var emploeeDetails = _employeeService.GetEmployeeAsync(id).Result;
            return Json(emploeeDetails);
        }
    }
}


//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Linq;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.Extensions.Configuration;
//using Business.Interface;
//using Business.Entities;
//using System.IO;
//using AspNetCoreHero.ToastNotification.Abstractions;
//using Microsoft.AspNetCore.Hosting;
//using Business.Interface.IEmployee;
//using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
//using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;
//using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;

//namespace ERP.Controllers
//{
//    [AllowAnonymous]
//    [Route("[controller]/[action]")]
//    public class VisitorController : SettingsController
//    {
//        private IConfiguration _config { get; set; }
//        private IVisitorService service { get; set; }
//        IEmailService _emailService = null;
//        private readonly INotyfService _notyf;
//        private readonly IHostingEnvironment _env;
//        private readonly IMasterService _masterService;
//        private readonly IEmployeeService _employeeService;
//        public  VisitorController(IConfiguration configuration, IEmailService emailService, INotyfService notyf, IHostingEnvironment hostingEnvironment, IMasterService masterService, IEmployeeService employeeService)
//        {
//            _config = configuration;
//            service = new Business.Service.VisitorService(_config);
//            _emailService = emailService;
//            _notyf = notyf;
//            _env = hostingEnvironment;
//            _masterService = masterService;
//            _employeeService = employeeService;
//        }
//        // GET: VisitorController
//        public ActionResult Index()
//        {
//            return View();
//        }

//        // GET: VisitorController/Details/5
//        public ActionResult Details(int id)
//        {
//            return View();
//        }

//        // GET: VisitorController/Create
//        public ActionResult Create()
//        {
//            var model = new VisitorMetaData();
//            model.CreatedBy = USERID;
//            model.MeetingRequestDateTime = System.DateTime.Now;
//            var idProofList = new Business.Service.MasterService(_config).GetIdentityProofTypeAsync();
//            ViewData["IdentityProofType"] = new SelectList(idProofList, "IdentityProofTypeID", "IdentityProofTypeText");
//            var vehicleTypeList = new Business.Service.MasterService(_config).GetVehicleTypeAsync();
//            ViewData["VehicleTypeID"] = new SelectList(vehicleTypeList, "VehicleTypeID", "VehicleTypeText");
//            var listEmployees = _masterService.GetAllEmployees();
//            ViewData["MeetToWhomPersonName"] = new SelectList(listEmployees, "EmployeeID", "EmployeeName");
//            return View(model);
//        }

//        // POST: VisitorController/Create
//        [HttpPost]
//        public ActionResult Create(Business.Entities.VisitorMetaData model, IFormFile files)
//        {
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    var basePath = Path.Combine(_env.WebRootPath + "\\Files\\" + model.Email + "\\");
//                    bool basePathExists = System.IO.Directory.Exists(basePath);
//                    if (!basePathExists) Directory.CreateDirectory(basePath);
//                    var filName = Path.GetFileNameWithoutExtension(files.FileName);
//                    var filePath = Path.Combine(basePath, files.FileName);
//                    var extension = Path.GetExtension(files.FileName);
//                    if (!System.IO.File.Exists(filePath))
//                    {
//                        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
//                        {
//                            files.CopyToAsync(stream);
//                        }
//                    }
//                    int result = new Business.Service.VisitorService(_config).AddVisitorRequestAsync(model, USERID, files, filePath);
//                    if (result > 0)
//                    {
//                        //send email to whom visitor wants to meet
//                        MailRequest request = new MailRequest();
//                        request.Subject = "Meeting Request -" + model.MeetingRequestTitle;
//                        request.ToEmail = model.MeetToWhomPersonEmail;
//                        string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\VisitorRequest.html";
//                        StreamReader str = new StreamReader(FilePath);
//                        string MailText = str.ReadToEnd();
//                        str.Close();
//                        MailText = MailText.Replace("#username#", model.MeetToWhomPersonName).Replace("#VisitorName#", model.FirstName + " " + model.LastName);
//                        MailText = MailText.Replace("#MeetingRequestTitle#", model.MeetingRequestTitle);
//                        MailText = MailText.Replace("#approvallink#", "https://www.w3schools.com/");
//                        MailText = MailText.Replace("#CompanyName#", "Industrial Boilers LTD");
//                        var filePathCompLogo = Path.Combine(Directory.GetCurrentDirectory(), "/companylogo/logo.png");
//                        MailText = MailText.Replace("#CompanyLogo#", "<img src='" + filePathCompLogo + "' alt='Industrial Boilers LTD' />");
//                        request.Body = MailText;
//                        _emailService.SendEmail(request);

//                        //send email to visior for acknowledgement
//                        request = new MailRequest();
//                        request.Subject = "Acknowledgement of Visitor request form";
//                        request.ToEmail = model.Email;
//                        FilePath = Directory.GetCurrentDirectory() + "\\Templates\\AcknowledgementToVisitor.html";
//                        str = new StreamReader(FilePath);
//                        MailText = str.ReadToEnd();
//                        str.Close();
//                        MailText = MailText.Replace("#Visitor#", model.FirstName + " " + model.LastName);
//                        filePathCompLogo = Path.Combine(Directory.GetCurrentDirectory(), "/companylogo/logo.png");
//                        MailText = MailText.Replace("#CompanyName#", "Industrial Boilers LTD");
//                        MailText = MailText.Replace("#CompanyLogo#", "<img src='" + filePathCompLogo + "' alt='Industrial Boilers LTD' />");
//                        request.Body = MailText;
//                        _emailService.SendEmail(request);
//                    }
//                    _notyf.Success("Your request has been successfully created.");
//                    return RedirectToAction(nameof(Create));
//                }
//            }
//            catch
//            {

//            }
//            if (model.MeetingRequestDateTime == null && model.MeetingRequestDateTime != System.DateTime.MinValue)
//            {
//                model.MeetingRequestDateTime = System.DateTime.Now;
//            }
//            var idProofList = new Business.Service.MasterService(_config).GetIdentityProofTypeAsync();
//            ViewData["IdentityProofType"] = new SelectList(idProofList, "IdentityProofTypeID", "IdentityProofTypeText");
//            var vehicleTypeList = new Business.Service.MasterService(_config).GetVehicleTypeAsync();
//            ViewData["VehicleTypeID"] = new SelectList(vehicleTypeList, "VehicleTypeID", "VehicleTypeText");

//            return View(model);
//        }

//        // GET: VisitorController/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }

//        // POST: VisitorController/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: VisitorController/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        // POST: VisitorController/Delete/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Delete(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        [HttpPost]
//        public JsonResult AutoComplete(string prefix)
//        {
//            try
//            {
//                var zipcodeData = new Business.Service.MasterService(_config).GetZipCodeAsync(prefix);
//                var zipcodes = (from zip in zipcodeData
//                                where zip.ZipCode.StartsWith(prefix)
//                                select new
//                                {
//                                    label = zip.ZipCode,
//                                    val = zip.ZipCodeID
//                                }).ToList();

//                return Json(zipcodes);
//            }
//            catch
//            {
//                return Json(false);
//            }
//        }

//        public JsonResult EmployeeDetails(int id)
//        {
//            var emploeeDetails = _employeeService.GetEmployeeAsync(id).Result;
//            return Json(emploeeDetails);
//        }
//    }
//}
