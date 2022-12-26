using Business.Entities.Marketing.VisitingDetail;
using Business.Interface;
using Business.Interface.Marketing.IVisitingDetailService;
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
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;

namespace ERP.Areas.Marketing.Controllers
{
    [Area("Marketing"), Authorize]
    [DisplayName("VisitingDetail")]
    public class MarketingVisitingDetailController : SettingsController
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMasterService _masterService;
        private readonly IMarketingVisitingDetailService _iMarketingVisitingDetailService;

        public MarketingVisitingDetailController(IWebHostEnvironment hostEnvironment, IMasterService masterService, IMarketingVisitingDetailService iMarketingVisitingDetailService)
        {
            _webHostEnvironment = hostEnvironment;
            this._masterService = masterService;
            _iMarketingVisitingDetailService = iMarketingVisitingDetailService;
        }


        /* Visiting Detail Index Start */
        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<VisitingDetail>> columns = c =>
            {

                c.Add(o => o.SrNo, "SrNo")
                    .Titled("SrNo")
                    .Sortable(true)
                    .SetWidth(10);

                c.Add(o => o.VisitedByPerson)
                   .Titled("Visited By")
                   .Sortable(true)
                   .SetWidth(20);

                c.Add(o => o.VisitedTo)
                    .Titled("Meeting With")
                    .Sortable(true)
                    .SetWidth(20);

                c.Add(o => o.CompanyOrOrganazationName)
                   .Titled("Company Name")
                   .Sortable(true)
                   .SetWidth(20);

                c.Add(o => o.MobileNo)
                    .Titled("Mobile No")
                    .Sortable(true)
                    .SetWidth(50);

                c.Add(o => o.DateTime)
                   .Titled("Meeting Date")
                   .Sortable(true)
                   .SetWidth(20);

                c.Add(o => o.MeetingTotalTime)
                    .Titled("Meeting Duration")
                    .Sortable(true)
                    .SetWidth(50);                

                /*c.Add(o => o.PartyTypeText)
                    .Titled("Party Type")
                    .Sortable(true)
                    .SetWidth(50);

                c.Add(o => o.VanueTypeText)
                    .Titled("Vanue Type")
                    .Sortable(true)
                    .SetWidth(50);

                c.Add(o => o.PlaceOfMeeting)
                    .Titled("Place")
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

                c.Add(o => o.IsSentMarketingDocs)
                    .Titled("Marketing Doc Status")
                    .Sortable(true)
                    .SetWidth(50);

                c.Add(o => o.MOM)
                    .Titled("Note")
                    .Sortable(true)
                    .SetWidth(50);

                c.Add(o => o.Feedback)
                    .Titled("Feedback")
                    .Sortable(true)
                    .SetWidth(50);                

                c.Add(o => o.ReferenceBetterBusiness)
                    .Titled("Business Reference")
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
                    .SetWidth(60)
                    .Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn' onclick='fnVisitingDetail(this)' href='javascript:void(0)' data-id='{o.MarketingVisitedDetailID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_VisitingDetail' aria-controls='canvas_VisitingDetail'><i class='bx bx-edit'></i> </a>");

            };

            PagedDataTable<VisitingDetail> pds = _iMarketingVisitingDetailService.GetAllMarketingVisitingDetailAsync(gridpage.ToInt(),
               PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<VisitingDetail>(pds, query, false, "ordersGrid",
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
        /* Visiting Detail Index End */

        /* Visiting Detail  silder Start */
        [HttpPost]
        public PartialViewResult Get(int id, string key)
        {
            try
            {
                VisitingDetail model = new VisitingDetail();

                var PartyTypeTextList = _masterService.GetPartyTypeMasterAsync();
                ViewData["PartyTypeText"] = new SelectList(PartyTypeTextList, "PartyTypeID", "PartyTypeText");

                var VanueTypeTextList = _masterService.GetVanueTypeMasterAsync();
                ViewData["VenueTypeText"] = new SelectList(VanueTypeTextList, "VanueTypeID", "VanueTypeText");

                if (id > 0)
                {
                    model = _iMarketingVisitingDetailService.GetMarketingVisitingDetailAsync(id).Result;

                    return PartialView("IU_MarketingVisitingDetail", model);
                }
                else
                {
                    return PartialView("IU_MarketingVisitingDetail", model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        /* Visiting Detail silder Start */

        /* Visiting Detail Insert or Update Page Start */
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateMarketingVisitingDetail(VisitingDetail model)
        {
            model.CreatedOrModifiedBy = USERID;
            var _marketingVisitedDetailID = await _iMarketingVisitingDetailService.MarketingVisitingDetailInsertOrUpdateAsync(model);

            if (_marketingVisitedDetailID > 0)
            {
                model.MarketingVisitedDetailID = _marketingVisitedDetailID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
                return Json(new { status = false, message = MessageHelper.Error });
        }
        /* Visiting Detail Insert or Update Page End */
    }
}
