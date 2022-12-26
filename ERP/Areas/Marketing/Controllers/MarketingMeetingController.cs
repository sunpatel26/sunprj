using Business.Entities;
using Business.Entities.Marketing.Meeting;
using Business.Interface;
using Business.Interface.Marketing.IMeeting;
using ERP.Controllers;
using ERP.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;
using JsonResult = System.Web.Mvc.JsonResult;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Business.Service.Marketing;
using Business.SQL;

namespace ERP.Areas.Marketing.Controllers
{
    [Area("Marketing"), Authorize]
    [DisplayName("Meeting")]
    public class MarketingMeetingController : SettingsController
    {

        private readonly UserManager<UserMasterMetadata> _userManager;
        private readonly SignInManager<UserMasterMetadata> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMasterService _masterService;
        private readonly IMarketingMeeting _iMarketingMeeting;
        
        public MarketingMeetingController(UserManager<UserMasterMetadata> userManager, SignInManager<UserMasterMetadata> signInManager, IMasterService masterService, IWebHostEnvironment hostEnvironment, IMarketingMeeting marketingMeeting)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._masterService = masterService;
            _webHostEnvironment = hostEnvironment;
            _iMarketingMeeting = marketingMeeting;
        }


        public ActionResult Index()                        
        {
            return View();
        }
       
        public Microsoft.AspNetCore.Mvc.JsonResult MarketingMeet()
        {
            /*List<temp> events = new List<temp> { 
            new temp{ EventID=1, Subject="Test 1", Description="testing d 1", Start="17-11-2022", End="18-11-2022"} ,
            new temp{ EventID=2, Subject="Test 2", Description="testing d 2", Start="19-11-2022", End="20-11-2022"} ,
            new temp{ EventID=3, Subject="Test 3", Description="testing d 3", Start="14-11-2022", End="15-11-2022"} ,
            };*/

            List<MarketingMeeting> events = _iMarketingMeeting.GetAllMarketingMeetingAsync().Result;

            //List<MarketingMeeting> events = new List<MarketingMeeting>();
            //var json = new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return Json(events);
        }

        public IActionResult Create()
        {
            var MeetingStatusTypeTextList = _masterService.GetAllMeetingStatusTypeMasterAsync(); ViewData["MeetingStatusTypeText"] = new SelectList(MeetingStatusTypeTextList, "MeetingStatusTypeID", "MeetingStatusTypeText");

            var MeetingDurationTypeTextList = _masterService.GetAllMeetingDurationTypeMasterAsync(); ViewData["MeetingDurationTypeText"] = new SelectList(MeetingDurationTypeTextList, "MeetingDurationTypeID", "MeetingDurationTypeText");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateMeetingDetail(MarketingMeeting model)
        {
            model.CreatedOrModifiedBy = USERID;
            var _marketingMeetingID = await _iMarketingMeeting.MarketingMeetingInsertOrUpdateAsync(model);

            if (_marketingMeetingID > 0)
            {
                model.MarketingMeetingID = _marketingMeetingID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
                return Json(new { status = false, message = MessageHelper.Error });
        }
    }

   /* public class temp { 
        public int EventID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string ThemeColor { get; set; }
        public string IsFullyDay { get; set; }
    }*/
}
