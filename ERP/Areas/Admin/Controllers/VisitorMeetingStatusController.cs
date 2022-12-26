using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Entities;
using Business.Interface;
using ERP.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    [DisplayName("VisitorMeetingStatus")]
    public class VisitorMeetingStatusController : SettingsController
    {
        #region "Variable and Properties"
        private readonly IVisitorService _visitorService;
        private readonly IEmailService _emailService;
        private readonly IViewRenderService _viewRenderService;
        private readonly INotyfService _notyf;
        public VisitorMeetingStatusController(IVisitorService visitorService, IEmailService emailService, IViewRenderService viewRenderService, INotyfService notyf)
        {
            this._visitorService = visitorService;
            this._emailService = emailService;
            this._viewRenderService = viewRenderService;
            this._notyf = notyf;



        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetVisitorDetail([FromQuery(Name = "VisitorMeetingRequestID")] int VisitorMeetingRequestID = 0)
        {
            VisitorMeetingStatus visitorMeetingStatus = new VisitorMeetingStatus();

            try
            {
                var list = await _visitorService.GetVisitorMeetingDetail(VisitorMeetingRequestID);
                visitorMeetingStatus = list;
                return View(visitorMeetingStatus);
            }
            catch
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult SaveVisitorStatus(VisitorMeetingStatus visitorMeetingStatus)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = _visitorService.setVisitorStatus(visitorMeetingStatus, USERID);
                    if (result == 2)
                    {
                        var list = _visitorService.GetAll(1, PAGESIZE, "VisitorMeetingRequestID", "DESC", "", visitorMeetingStatus.VisitorMeetingRequestID);
                        var model = list[0];
                        if (model != null)
                        {
                            //send email to whom visitor wants to meet
                            MailRequestMetadata request = new MailRequestMetadata();
                            request.Subject = "Thanks for Visit to our Company";
                            request.ToEmail = model.Email;
                            string FilePath = Directory.GetCurrentDirectory() + "\\Areas\\Admin\\MailTemplate\\ThankYou.html";
                            StreamReader str = new StreamReader(FilePath);
                            string MailText = str.ReadToEnd();
                            str.Close();
                            MailText = MailText.Replace("#Visitor#", model.VisitorName);
                            var siteURL = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                            MailText = MailText.Replace("#feedbackURL#", siteURL + "/Acknowledge/Feedback/" + model.VisitorMeetingRequestID);
                            MailText = MailText.Replace("#CompanyName#", "Industrial Boilers LTD");
                            var filePathCompLogo = Path.Combine(Directory.GetCurrentDirectory(), "/companylogo/logo.png");
                            MailText = MailText.Replace("#CompanyLogo#", "<img src='" + filePathCompLogo + "' alt='Industrial Boilers LTD' />");
                            request.Body = MailText;
                            _emailService.SendEmail(request);
                        }

                    }
                    _notyf.Success("Your request has been successfully created.");
                    return RedirectToAction(nameof(GetVisitorDetail), new { VisitorMeetingRequestID = visitorMeetingStatus.VisitorMeetingRequestID });
                }
            }
            catch (Exception ex)
            {
                throw;
            }


            return View(visitorMeetingStatus);

        }
    }
}
