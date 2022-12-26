using Business.Entities;
using Business.Entities.Marketing.CompanySale;
using Business.Interface;
using Business.Interface.Marketing.ICompanyTarget;
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
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;

namespace ERP.Areas.Marketing.Controllers
{

    [Area("Marketing"), Authorize]
    [DisplayName("CompanyTarget")]
    public class MarketingCompanyTargetController : SettingsController
    {
        private readonly UserManager<UserMasterMetadata> _userManager;
        private readonly SignInManager<UserMasterMetadata> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMarketingCompanyTargetService _iMarketingCompanyTargetService;
        private readonly IMasterService _masterService;

        public MarketingCompanyTargetController(UserManager<UserMasterMetadata> userManager, SignInManager<UserMasterMetadata> signInManager, IWebHostEnvironment hostEnvironment, IMarketingCompanyTargetService iMarketingCompanyTargetService, IMasterService masterService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            _webHostEnvironment = hostEnvironment;
            this._iMarketingCompanyTargetService = iMarketingCompanyTargetService;
            this._masterService = masterService;
        }


        /*CompanyTarget Index Page Start*/
        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<CompanyTarget>> columns = c =>
            {

                c.Add(o => o.SrNo, "SrNo")
                    .Titled("SrNo")
                    .Sortable(true)
                    .SetWidth(10);

                c.Add(o => o.CompanyName)
                    .Titled("Company Name")
                    .Sortable(true)
                    .SetWidth(10);

                c.Add(o => o.TargetValue)
                    .Titled("Target")
                    .Sortable(true)
                    .SetWidth(10);

                c.Add(o => o.StartDate)
                    .Titled("Start Date")
                    .Sortable(true)
                    .SetWidth(10)                    
                    .Format("{0:dd/MM/yyyy}");

                c.Add(o => o.EndDate)
                   .Titled("End Date")
                   .Sortable(true)
                   .SetWidth(15)
                   .Format("{0:dd/MM/yyyy}");

                c.Add(o => o.FinancialYear)
                    .Titled("Financial Year")
                    .Sortable(true)
                    .SetWidth(50);

                c.Add()
                    .Titled("Edit")
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(10)
                    .Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn' onclick='fnCompanyTarget(this)' href='javascript:void(0)' data-id='{o.CompanyTargetID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_CompanyTarget' aria-controls='canvas_CompanyTarget'><i class='bx bx-edit'></i> </a>");

            };

            PagedDataTable<CompanyTarget> pds = _iMarketingCompanyTargetService.GetAllMarketingCompanyTargetAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<CompanyTarget>(pds, query, false, "ordersGrid",
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
        /*CompanyTarget Index Page End*/

        /*CompanyTarget silder Start*/
        [HttpPost]
        public PartialViewResult Get(int id, string key)
        {
            try
            {
                //System.Diagnostics.Debug.WriteLine("id-- " + id);

                CompanyTarget model = new CompanyTarget();

                var CompanyMasterList = _masterService.GetAllCompanyMasterAsync();
                ViewData["CompanyName"] = new SelectList(CompanyMasterList, "CompanyID", "CompanyName");

                var FinancialYearList = _masterService.GetAllFinancialYearMasterAsync();
                ViewData["FinancialYear"] = new SelectList(FinancialYearList, "FinancialYearID", "FinancialYear");

                if (id > 0)
                {
                    model = _iMarketingCompanyTargetService.GetMarketingCompanyTargetAsync(id).Result;

                    return PartialView("IUMarketingCompanyTarget", model);
                }
                else
                {
                    return PartialView("IUMarketingCompanyTarget", model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        /*CompanyTarget silder End*/

        /*CompanyTarget Insert or Update Page Start*/

        [HttpPost]
        public async Task<IActionResult> IOrUMarketingCompanyTarget(CompanyTarget model)
        {
            model.CreatedOrModifiedBy = USERID;
            var _marketingCompanyTargetID = await _iMarketingCompanyTargetService.InsertOrUpdateMarketingCompanyTargetAsync(model);

            if (_marketingCompanyTargetID > 0)
            {
                model.CompanyTargetID = _marketingCompanyTargetID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
                return Json(new { status = false, message = MessageHelper.Error });
        }

        /*CompanyTarget Insert or Update Page End*/
    }
}
