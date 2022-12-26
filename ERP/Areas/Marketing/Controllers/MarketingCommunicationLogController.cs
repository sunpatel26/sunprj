using Business.Entities.Marketing.CommunicationLog;
using Business.Interface;
using Business.Interface.Marketing.CommunicatonLog;
using Business.SQL;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    [DisplayName("CommunicationLog")]
    public class MarketingCommunicationLogController : SettingsController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMasterService _masterService;
        private readonly IMarketingCommunicationLogService _iMarketingCommunicationLogService;

        public MarketingCommunicationLogController(IWebHostEnvironment hostEnvironment, IMasterService masterService, IMarketingCommunicationLogService iMarketingCommunicationLogService)
        {
            _webHostEnvironment = hostEnvironment;
            this._masterService = masterService;
            this._iMarketingCommunicationLogService = iMarketingCommunicationLogService;
        }

        /*Communicaton Log Index Start*/
        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<CommunicationLog>> columns = c =>
            {

                c.Add(o => o.SrNo, "SrNo")
                    .Titled("SrNo")
                    .Sortable(true)
                    .SetWidth(10);

                c.Add(o => o.ContactByPerson)
                    .Titled("Contact By")
                    .Sortable(true)
                    .SetWidth(50);

                c.Add(o => o.ContactTo)
                   .Titled("Contact To")
                   .Sortable(true)
                   .SetWidth(50);

                c.Add(o => o.MobileNo)
                    .Titled("Mobile No")
                    .Sortable(true)
                    .SetWidth(50);

                c.Add(o => o.CommunicationLogDate)
                   .Titled("Communication Date")
                   .Sortable(true)
                   .SetWidth(20);

                c.Add(o => o.ContactChannelTypeText)
                    .Titled("Contact Channel")
                    .Sortable(true)
                    .SetWidth(50);

                /* c.Add(o => o.PartyTypeText)
                     .Titled("Party Type")
                     .Sortable(true)
                     .SetWidth(50);

                 c.Add(o => o.VanueTypeText)
                     .Titled("Vanue Type")
                     .Sortable(true)
                     .SetWidth(50);

                c.Add(o => o.Email)
                    .Titled("Email")
                    .Sortable(true)
                    .SetWidth(50);

                c.Add(o => o.IsSentDocument)
                    .Titled("Document Status")
                    .Sortable(true)
                    .SetWidth(50);

                 c.Add(o => o.IsSentMarketingDocument)
                     .Titled("Marketing Doc Status")
                     .Sortable(true)
                     .SetWidth(50);

                 c.Add(o => o.Note)
                     .Titled("Note")
                     .Sortable(true)
                     .SetWidth(50);

                 c.Add(o => o.Feedback)
                     .Titled("Feedback")
                     .Sortable(true)
                     .SetWidth(50);

                 c.Add(o => o.ReferenceBetterBusiness)
                     .Titled("Business")
                     .Sortable(true)
                     .SetWidth(50);

                 c.Add(o => o.ReferenceMobileOrEmail)
                     .Titled("Contact Detail")
                     .Sortable(true)
                     .SetWidth(20);*/

                c.Add()
                    .Titled("Edit")
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(10)
                    .Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn' onclick='fnCommunicatioLog(this)' href='javascript:void(0)' data-id='{o.MarketingCommunicationLogID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_CommunicatioLog' aria-controls='canvas_CommunicatioLog'><i class='bx bx-edit'></i> </a>");

            };

            PagedDataTable<CommunicationLog> pds = _iMarketingCommunicationLogService.GetAllMarketingCommunicationLogAsync(gridpage.ToInt(),
               PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<CommunicationLog>(pds, query, false, "ordersGrid",
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
            return View("Index", server.Grid);
        }
        /*Communicaton Log Index End*/

        /*Communicaton Log  silder Start*/
        [HttpPost]
        public PartialViewResult Get(int id, string key)
        {
            try
            {
                CommunicationLog model = new CommunicationLog();

                var PartyTypeTextList = _masterService.GetPartyTypeMasterAsync();
                ViewData["PartyTypeText"] = new SelectList(PartyTypeTextList, "PartyTypeID", "PartyTypeText");

                var ContactChannelTextList = _masterService.GetContactChannelMasterAsync();
                ViewData["ContactChannelText"] = new SelectList(ContactChannelTextList, "ContactChannelTypeID", "ContactChannelTypeText");

                var VanueTypeTextList = _masterService.GetVanueTypeMasterAsync();
                ViewData["VenueTypeText"] = new SelectList(VanueTypeTextList, "VanueTypeID", "VanueTypeText");

                if (id > 0)
                {
                    model = _iMarketingCommunicationLogService.GetMarketingCommunicationLogAsync(id).Result;

                    return PartialView("CreateMarketingCommunicationLog", model);
                }
                else
                {
                    return PartialView("CreateMarketingCommunicationLog", model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        /*Communicaton Log  silder End*/

        /*Communicaton Log Insert or Update Page Start*/
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateMarketingCommunicationLog(CommunicationLog model)
        {
            model.CreatedOrModifiedBy = USERID;
            var _marketingCommunicationLogID = await _iMarketingCommunicationLogService.MarketingCommunicationLogInsertOrUpdateAsync(model);

            if (_marketingCommunicationLogID > 0)
            {
                model.MarketingCommunicationLogID = _marketingCommunicationLogID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
                return Json(new { status = false, message = MessageHelper.Error });
        }
        /*Communicaton Log Insert or Update Page End*/
    }
}
