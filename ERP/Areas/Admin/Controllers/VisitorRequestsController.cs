using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Entities;
using Business.Interface;
using Business.SQL;
using ERP.Controllers;
using GridCore.Server;
using GridShared;
//using iTextSharp.text;
//using iTextSharp.text.html.simpleparser;
//using iTextSharp.text.pdf;
//using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectPdf;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    [DisplayName("VisitorRequests")]
    public class VisitorRequestsController : SettingsController
    {
        #region "Variable and Properties"
        private readonly IVisitorService _visitorService;
        private readonly IEmailService _emailService;
        private readonly IViewRenderService _viewRenderService;
        private readonly INotyfService _notyf;
        private readonly ISuperAdminService _masterService;
        private readonly IHostingEnvironment _env;
        public VisitorRequestsController(IVisitorService visitorService, IEmailService emailService, IViewRenderService viewRenderService, INotyfService notyf, ISuperAdminService masterService, IHostingEnvironment hostingEnvironment)
        {
            this._visitorService = visitorService;
            this._emailService = emailService;
            this._viewRenderService = viewRenderService;
            this._notyf = notyf;
            this._masterService = masterService;
            this._env = hostingEnvironment;
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        #endregion

        // GET: VisitorRequestsController
        [DisplayName("View All")]
        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "MeetingRequestDateTime", [FromQuery(Name = "grid-dir")] string sortby = "1", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            int pSize = PAGESIZE;
            IQueryCollection query = Request.Query;
            int id = 0;
             

            Action<IGridColumnCollection<VisitorMetaData>> columns = c =>
            {
                //c.Add(o => o.SrNo)
                //        .Titled("Srno")
                //        .SetWidth(10)
                //        .Sortable(true)
                //        .SortInitialDirection(GridSortDirection.Ascending);


                c.Add(o => o.VisitorName)
                    .Titled("Visitor Name")
                    //.SortInitialDirection(GridSortDirection.Ascending)
                    .SetWidth(110)
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(60)
                    .Sortable(true)
                    .SetCellCssClassesContraint(o => o.IsApproved == 0 ? "rRow" : o.IsApproved == 1 ? "gRow" : o.IsApproved == 2 ? "yRow" : "vRow");
                
                
                c.Add(o => o.Email)
                        .Titled("Email")
                        .SetWidth(110).Sortable(true).SetCellCssClassesContraint(o => o.IsApproved == 0 ? "rRow" : o.IsApproved == 1 ? "gRow" : o.IsApproved == 2 ? "yRow" : "vRow");

                c.Add(o => o.MobileNo)
                  .Titled("MobileNo")
                  .SetWidth(110).Sortable(true).SetCellCssClassesContraint(o => o.IsApproved == 0 ? "rRow" : o.IsApproved == 1 ? "gRow" : o.IsApproved == 2 ? "yRow" : "vRow");

                c.Add(o => o.MeetingRequestDateTime)
                  .Titled("Metting Req. On")
                  .SetWidth(110).Sortable(true).SetCellCssClassesContraint(o => o.IsApproved == 0 ? "rRow" : o.IsApproved == 1 ? "gRow" : o.IsApproved == 2 ? "yRow" : "vRow");

                c.Add(o => o.MeetToWhomPersonName)
                  .Titled("Whom to Meet")
                  .SetWidth(110).Sortable(true).SetCellCssClassesContraint(o => o.IsApproved == 0 ? "rRow" : o.IsApproved == 1 ? "gRow" : o.IsApproved == 2 ? "yRow" : "vRow");

                c.Add(o => o.MeetingRequestTitle)
                 .Titled("Meeting Purpose")
                 .SetWidth(250).Sortable(true).SetCellCssClassesContraint(o => o.IsApproved == 0 ? "rRow" : o.IsApproved == 1 ? "gRow" : o.IsApproved == 2 ? "yRow" : "vRow");

                c.Add(o => o.IsApproved)
                 .RenderValueAs(o => $"{o.IsApproved.ToApproveOrReject()}")
                   .Titled("Approved")
                   .SetWidth(50)
                   .SetCellCssClassesContraint(o => o.IsApproved == 0 ? "rRow" : o.IsApproved == 1 ? "gRow" : o.IsApproved == 2 ? "yRow" : "vRow");

                c.Add()
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(20).Sortable(false)
                    //.Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn' href='VisitorRequests/Details/{o.VisitorMeetingRequestID}' ><i class='bx bx-check-square bx-flashing-hover'></i></a>")
                    .SetCellCssClassesContraint(o => o.IsApproved == 0 ? "rRow disable" : o.IsApproved == 1 ? "gRow disable" : o.IsApproved == 2 ? "yRow" : "vRow disable");

                c.Add()
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(20).Sortable(false)
                    //.Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn' href='VisitorRequests/GetVisitorDetail/{o.VisitorMeetingRequestID}' ><i class='bx bxs-user-pin bx-flashing-hover'></i></a>")
                    .SetCellCssClassesContraint(o => o.IsApproved == 0 ? "rRow hidden" : o.IsApproved == 1 ? "gRow" : o.IsApproved == 2 ? "yRow hidden" : "vRow hidden");

            };
            PagedDataTable<VisitorMetaData> pds = _visitorService.GetAll(gridpage.ToInt(), pSize, orderby, sortby, search, id, userid);
            var server = new GridCoreServer<VisitorMetaData>(pds, query, false, "VisitorRquests",
                columns, pSize, pds.TotalItemCount)
                .Sortable()
                //.Filterable()
                //.WithMultipleFilters()
                .Searchable(true, true)
                .ClearFiltersButton(false)
                .SetStriped(true)
                //.ChangePageSize(true)
                .WithGridItemsCount()
                //.WithPaging(PAGESIZE, pds.TotalItemCount)
                .ChangeSkip(false)
                .EmptyText("No record found")
                .Selectable(true);

            
            return View(server.Grid);
        }

        // GET: VisitorRequestsController/Details/5
        public ActionResult Details(int id)
        {
            VisitorMetaData data = new VisitorMetaData();
            try
            {
                var list = _visitorService.GetAll(1, PAGESIZE, "VisitorMeetingRequestID", "DESC", "", id, 0);
                data = list[0];
            }
            catch
            {
                throw;
            }
            return View(data);
        }
        public ActionResult GetVisitorDetail(int id)
        {
            return RedirectToAction("GetVisitorDetail", "VisitorMeetingStatus", new { VisitorMeetingRequestID = id });
        }
        public async Task<IActionResult> ScantoView(string QRCode)
        {
            VisitorMetaData data = new VisitorMetaData();
            try
            {
                var list = await _visitorService.GetVisitorAsync(QRCode);
                return View(list);

            }
            catch
            {
                throw;
            }
        }

        // GET: VisitorRequestsController/Create
        public ActionResult Create()
        {
            var model = new Business.Entities.VisitorMetaData();
            model.CreatedBy = USERID;
            model.MeetingRequestDateTime = System.DateTime.Now;
            var idProofList = _masterService.GetIdentityProofTypeAsync();
            ViewData["IdentityProofType"] = new SelectList(idProofList, "IdentityProofTypeID", "IdentityProofTypeText");
            var vehicleTypeList = _masterService.GetVehicleTypeAsync();
            ViewData["VehicleTypeID"] = new SelectList(vehicleTypeList, "VehicleTypeID", "VehicleTypeText");

            return View(model);
        }

        // POST: VisitorRequestsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VisitorMetaData model, IFormFile files)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var basePath = Path.Combine(_env.WebRootPath + "\\Files\\" + model.Email + "\\");
                    bool basePathExists = System.IO.Directory.Exists(basePath);
                    if (!basePathExists) Directory.CreateDirectory(basePath);
                    string filName = "", filePath = "", extension = "";
                    if (files != null)
                    {
                        filName = Path.GetFileNameWithoutExtension(files.FileName);
                        filePath = Path.Combine(basePath, files.FileName);
                        extension = Path.GetExtension(files.FileName);
                        if (!System.IO.File.Exists(filePath))
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                            {
                                files.CopyToAsync(stream);
                            }
                        }
                    }
                    int result = _visitorService.AddVisitorRequestAsync(model, USERID, files, filePath);
                    if (result > 0)
                    {
                        var list = _visitorService.GetAll(1, PAGESIZE, "VisitorMeetingRequestID", "DESC", "", result);
                        var data = list[0];
                        //Initialize HTML to PDF converter 

                        //HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

                        //HTML string and base URL 

                        string FilePathD = Directory.GetCurrentDirectory() + "\\Areas\\Admin\\MailTemplate\\ApprovedWithQR.html";
                        StreamReader strD = new StreamReader(FilePathD);
                        string MailTextD = strD.ReadToEnd();
                        MailTextD = MailTextD.Replace("#VisitorName#", data.VisitorName.ToString());
                        MailTextD = MailTextD.Replace("#VisitorMeetingRequestCode#", data.VisitorMeetingRequestCode.ToString());
                        MailTextD = MailTextD.Replace("#MeetingRequestDateTime#", data.MeetingRequestDateTime.ToString());
                        MailTextD = MailTextD.Replace("#CompanyName#", "Industrial Boilers LTD");
                        var filePathCompLogo = Path.Combine(Directory.GetCurrentDirectory(), "/companylogo/logo.png");
                        MailTextD = MailTextD.Replace("#CompanyLogo#", "<img src='" + filePathCompLogo + "' alt='Industrial Boilers LTD' />");


                        string fileName = data.VisitorName.RemoveSpace() + "-" + data.VisitorMeetingRequestCode.ToString();
                        string saveUrl = Directory.GetCurrentDirectory() + @"\wwwroot\VMS\" + fileName + ".pdf";

                        string baseUrl = Directory.GetCurrentDirectory() + @"\" + data.VisitorName + ".pdf";

                        //Convert HTML to PDF document 
                        HtmlToPdf htmlToPdf = new HtmlToPdf();
                        htmlToPdf.Options.PdfPageOrientation = SelectPdf.PdfPageOrientation.Portrait;
                        // put css in pdf
                        htmlToPdf.Options.MarginLeft = 15;
                        htmlToPdf.Options.MarginRight = 15;

                        SelectPdf.PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(MailTextD);
                        byte[] pdf = pdfDocument.Save();
                        //convert to memory stream
                        Stream stream = new MemoryStream(pdf);
                        pdfDocument.Close();
                        //if want to transfer stream to file 
                        return File(stream, "application/pdf", Guid.NewGuid().ToString() + ".pdf");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
            }
            if (model.MeetingRequestDateTime != System.DateTime.MinValue)
            {
                model.MeetingRequestDateTime = System.DateTime.Now;
            }
            var idProofList = _masterService.GetIdentityProofTypeAsync();
            ViewData["IdentityProofType"] = new SelectList(idProofList, "IdentityProofTypeID", "IdentityProofTypeText");
            var vehicleTypeList = _masterService.GetVehicleTypeAsync();
            ViewData["VehicleTypeID"] = new SelectList(vehicleTypeList, "VehicleTypeID", "VehicleTypeText");

            return View(model);
        }


        // GET: VisitorRequestsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VisitorRequestsController/Edit/5
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

        // GET: VisitorRequestsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VisitorRequestsController/Delete/5
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

        public ActionResult Approve(int id)
        {
            VisitorMetaData data = new VisitorMetaData();
            try
            {
                var list = _visitorService.GetAll(1, PAGESIZE, "VisitorMeetingRequestID", "DESC", "", id);
                data = list[0];

                VisitorRequestApproveMetaData model = new VisitorRequestApproveMetaData();
                model.VisitorMeetingRequestID = id;
                model.IsApproved = true;
                model.MeetingDateAndTime = data.MeetingRequestDateTime;
                model.Note = data.PurposeofMeeting;
                model.VisitorMeetingRequestCode = data.VisitorMeetingRequestCode;
                int resSuccess = _visitorService.ApproveVisitorRequest(model, USERID);

                if (resSuccess > 0)
                {
                    //send email to whom visitor wants to meet
                    MailRequestMetadata request = new MailRequestMetadata();
                    request.Subject = "Confirm the Meeting Request";
                    request.ToEmail = data.Email;
                    string FilePath = Directory.GetCurrentDirectory() + "\\Areas\\Admin\\MailTemplate\\MeetingApproved.html";
                    StreamReader str = new StreamReader(FilePath);
                    string MailText = str.ReadToEnd();
                    str.Close();
                    MailText = MailText.Replace("#Visitor#", data.VisitorName);
                    MailText = MailText.Replace("#CompanyName#", "Industrial Boilers LTD");
                    var filePathCompLogo = Path.Combine(Directory.GetCurrentDirectory(), "/companylogo/logo.png");
                    MailText = MailText.Replace("#CompanyLogo#", "<img src='" + filePathCompLogo + "' alt='Industrial Boilers LTD' />");
                    request.Body = MailText;
                    //var resultView = _viewRenderService.RenderToStringAsync("~/VisitorRequests/ApprovalWithQR.cshtml", data);
                    //var resultView = RazorTemplateEngine.RenderAsync("~/Areas/Admin/Views/VisitorRequests/ApproveWithQR.cshtml", data);
                    string FilePathD = Directory.GetCurrentDirectory() + "\\Areas\\Admin\\MailTemplate\\ApprovedWithQR.html";
                    StreamReader strD = new StreamReader(FilePathD);
                    string MailTextD = strD.ReadToEnd();
                    MailTextD = MailTextD.Replace("#VisitorName#", data.VisitorName.ToString());
                    MailTextD = MailTextD.Replace("#VisitorMeetingRequestCode#", data.VisitorMeetingRequestCode.ToString());
                    MailTextD = MailTextD.Replace("#MeetingRequestDateTime#", data.MeetingRequestDateTime.ToString());
                    string fileName = data.VisitorName.RemoveSpace() + "-" + data.VisitorMeetingRequestCode.ToString();
                    string saveUrl = Directory.GetCurrentDirectory() + @"\wwwroot\VMS\" + fileName + ".pdf";

                    //byte[] bytes = GeneratePDF(MailTextD, FilePathD, saveUrl);
                    //Convert HTML to PDF document 
                    HtmlToPdf htmlToPdf = new HtmlToPdf();
                    htmlToPdf.Options.PdfPageOrientation = SelectPdf.PdfPageOrientation.Portrait;
                    // put css in pdf
                    htmlToPdf.Options.MarginLeft = 15;
                    htmlToPdf.Options.MarginRight = 15;

                    SelectPdf.PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(MailTextD);
                    byte[] pdf = pdfDocument.Save();
                    //convert to memory stream
                    MemoryStream stream = new MemoryStream(pdf);
                    pdfDocument.Close();
                    //if want to transfer stream to file 
                    //return File(stream, "application/pdf", Guid.NewGuid().ToString() + ".pdf");
                    //FileStream fileStream = System.IO.File.Open(saveUrl, FileMode.Open, FileAccess.ReadWrite);
                    //bytes = new byte[fileStream.Length];
                    ////byte[] bytes = GeneratePdf(MailTextD);
                    request.Content = stream.ToArray();
                    _emailService.SendEmail(request);
                }
                _notyf.Success("This request has been successfully approved.");
                return RedirectToAction(nameof(Details), new { id = id });
            }
            catch (Exception ex)
            {
                _notyf.Error("Sorry! Something went wrong. Please try after some time." + ex.Message);

                return RedirectToAction(nameof(Details), new { id = id });
            }
        }


        //public byte[] GeneratePDF(string htmlPdf, string fPath, string saveUrl)
        //{
        //    //byte[] arry = null;
        //    string baseUrl = Directory.GetCurrentDirectory() + @"\wwwroot\companylogo\";

        //    //Initialize HTML to PDF converter.
        //    //HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();
        //    //WebKitConverterSettings webkitConverterSettings = new WebKitConverterSettings();
        //    ////Set the Qt Binaries folder path
        //    //webkitConverterSettings.WebKitPath = Directory.GetCurrentDirectory() + @"\QtBinariesWindows\";
        //    ////Assign Webkit converter settings to HTML converter
        //    //htmlConverter.ConverterSettings = webkitConverterSettings;
        //    ////Convert URL to PDF
        //    ////SelectPdf.PdfDocument document = htmlConverter.Convert(htmlPdf);
        //    ////FileStream fileStream = new FileStream(saveUrl, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        //    //////Save and close the PDF document 

        //    //MemoryStream stream = new MemoryStream();
        //    ////// Saves the document as stream
        //    ////document.Save(stream);
        //    ////document.Close(true);
        //    //// Converts the PdfDocument object to byte form.
        //    //byte[] docBytes = stream.ToArray();



        //    //PdfLoadedDocument ldoc = new PdfLoadedDocument(docBytes);
        //    //Saves the document and dispose it.
        //    //ldoc.Save(fileStream);
        //    //ldoc.Close(true);
        //    //arry = document.ToByteArray();

        //    //document.Save(fileStream);
        //    //document.Close(true);
        //    //document.DisposeOnClose(fileStream);
        //    //document.Dispose();
        //    ;


        //    //return docBytes;
        //}

        public ActionResult Declain(int id)
        {
            VisitorMetaData data = new VisitorMetaData();
            try
            {
                var list = _visitorService.GetAll(1, PAGESIZE, "VisitorMeetingRequestID", "DESC", "", id);
                data = list[0];

                VisitorRequestApproveMetaData model = new VisitorRequestApproveMetaData();
                model.VisitorMeetingRequestID = id;
                model.IsApproved = false;
                model.MeetingDateAndTime = data.MeetingRequestDateTime;
                model.Note = data.PurposeofMeeting;
                int resSuccess = _visitorService.ApproveVisitorRequest(model, USERID);

                if (resSuccess > 0)
                {
                    //send email to whom visitor wants to meet
                    MailRequestMetadata request = new MailRequestMetadata();
                    request.Subject = "Cancelled the Meeting Request";
                    request.ToEmail = data.Email;
                    string FilePath = Directory.GetCurrentDirectory() + "\\Areas\\Admin\\MailTemplate\\CancellMeeting.html";
                    StreamReader str = new StreamReader(FilePath);
                    string MailText = str.ReadToEnd();
                    str.Close();
                    MailText = MailText.Replace("#Visitor#", data.VisitorName);
                    var filePathCompLogo = Path.Combine(Directory.GetCurrentDirectory(), "/companylogo/logo.png");
                    MailText = MailText.Replace("#CompanyLogo#", "<img src='" + filePathCompLogo + "' alt='Industrial Boilers LTD' />");
                    request.Body = MailText;
                    _emailService.SendEmail(request);
                }
                _notyf.Success("This request has been successfully declained.");
                return RedirectToAction(nameof(Details), new { id = id });
            }
            catch
            {
                _notyf.Error("Sorry! Something went wrong. Please try after some time.");
                return View();
            }
        }

        //public ActionResult FeedbackListing()
        //{

        //}

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            try
            {
                var zipcodeData = _masterService.GetZipCodeAsync(prefix);
                var zipcodes = (from zip in zipcodeData
                                where zip.ZIPCode.StartsWith(prefix)
                                select new
                                {
                                    label = zip.ZIPCode,
                                    val = zip.ZIPCodeID
                                }).ToList();

                return Json(zipcodes);
            }
            catch
            {
                return Json(false);
            }
        }

        public FileResult DownloadFile(int id)
        {
            try
            {
                var fileData = _visitorService.GetVisitorMeetingRequestFile(id);
                string nm = ""; byte[] bytes = null;
                if (fileData != null)
                {
                    bytes = System.IO.File.ReadAllBytes(fileData.FilePath);
                    nm = fileData.Name;
                    return File(bytes, "application/octet-stream", nm);
                }                
            }
            catch
            {
                
            }
            return null;
        }
    }
}
