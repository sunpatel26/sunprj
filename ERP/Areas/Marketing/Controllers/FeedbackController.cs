using Business.Entities;
using Business.Entities.Marketing.Feedback;
using Business.Interface;
using Business.Interface.Marketing;
using Business.SQL;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;

namespace ERP.Areas.Marketing.Controllers
{
    [Area("Marketing"), Authorize]
    [DisplayName("Feedback")]
    public class FeedbackController : SettingsController
    {

        private readonly UserManager<UserMasterMetadata> _userManager;
        private readonly SignInManager<UserMasterMetadata> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMasterService _masterService;
        private readonly IMarketingFeedbackService _marketingFeedbackService;

        public FeedbackController(UserManager<UserMasterMetadata> userManager, SignInManager<UserMasterMetadata> signInManager,
            IMasterService masterService, IWebHostEnvironment hostEnvironment, IMarketingFeedbackService marketingFeedbackService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._masterService = masterService;
            _webHostEnvironment = hostEnvironment;
            _marketingFeedbackService = marketingFeedbackService;
        }




        /*Feedback List Start*/
        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<MarketingFeedback>> columns = c =>
            {

                c.Add(o => o.MarketingFeedbackID, "SrNo")
                    .Titled("SrNo")
                    .Sortable(true)
                    .SetWidth(10);

                c.Add(o => o.PartyName)
                    .Titled("Party Name")
                    .Sortable(true)
                    .SetWidth(20);

                c.Add(o => o.Note)
                    .Titled("Feedbach Note")
                    .Sortable(true)
                    .SetWidth(50);

                c.Add(o => o.MobileNo)
                    .Titled("Mobile No")
                    .Sortable(true)
                    .SetWidth(50);

                c.Add(o => o.FeedbackDate)
                    .Titled("Feedback Date")
                    .Sortable(true)
                    .SetWidth(20);

                /*c.Add(o => o.PartyTypeText)
                    .Titled("Party Type")
                    .Sortable(true)
                    .SetWidth(50);

                c.Add(o => o.Email)
                    .Titled("Email")
                    .Sortable(true)
                    .SetWidth(50);                

                c.Add(o => o.IsReceivedDocument)
                    .Titled("Document Status")
                    .Sortable(true)
                    .SetWidth(50); */    
                
                c.Add()
                    .Titled("Edit")
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(60)
                    .Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn' onclick='fnMasterEntity2(this)' href='javascript:void(0)' data-id='{o.MarketingFeedbackID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_masterentity2' aria-controls='canvas_masterentity'><i class='bx bx-edit'></i> </a>");

            };
            PagedDataTable<MarketingFeedback> pds = _marketingFeedbackService.GetAllMarketingFeedbackAsync(gridpage.ToInt(),
                PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<MarketingFeedback>(pds, query, false, "ordersGrid",
                columns, PAGESIZE, pds.TotalItemCount)
                .Sortable()
                //.Filterable()
                //.WithMultipleFilters()
                .Searchable(true, false)
                //.Groupable(true)
                .ClearFiltersButton(true)
                .Selectable(true)
                //.SetStriped(true)
                //.ChangePageSize(true)
                .WithGridItemsCount()
                //.WithPaging(PAGESIZE, pds.TotalItemCount)
                .ChangeSkip(false)
                .EmptyText("No record found")
                .ClearFiltersButton(false);
            return View("Index",server.Grid);
        }
        /*Feedback List End*/

        /*Marketing Feedback silder Start*/
        [HttpPost]
        public PartialViewResult Get(string id, int key)
        {
            try
            {
                MarketingFeedback model = new MarketingFeedback();

                var PartyTypeTextList = _masterService.GetPartyTypeMasterAsync();
                ViewData["PartyTypeText"] = new SelectList(PartyTypeTextList, "PartyTypeID", "PartyTypeText");

                if (Convert.ToInt32(id) >  0)
                {
                    model = _marketingFeedbackService.GetMarketingFeedbackAsync(id).Result;
                    /*model = new MarketingFeedback() { MarketingFeedbackID = key };*/
                    
                return PartialView("CreateMarketingFeedback", model);
                }
                return PartialView("CreateMarketingFeedback", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        /*Marketing Feedback silder End*/

        /*Feedback Create Page Start*/
        [HttpPost]
        public async Task<IActionResult> CreateMarketingFeedback(MarketingFeedback model)
        {
            model.CreatedOrModifiedBy = USERID;
            var _marketingFeedbackid = await _marketingFeedbackService.MarketingFeedbackCreateAsync(model);

            if (_marketingFeedbackid > 0)
            {
                model.MarketingFeedbackID = _marketingFeedbackid;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
                return Json(new { status = false, message = MessageHelper.Error });
            // If we got this far, something failed, redisplay form
        }
        /*Feedback Create Page End*/
    }
}
