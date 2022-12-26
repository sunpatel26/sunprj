using Business.Entities;
using Business.Entities.Master.MarketingCompanyFinancialYearMaster;
using Business.Interface.IMaster.IMarketingCompanyFinanicalYearMaster;
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

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    [DisplayName("MarketingCompanyFinanicalYear")]
    public class MarketingCompanyFinancialYearMasterController : SettingsController
    {
        private readonly UserManager<UserMasterMetadata> _userManager;
        private readonly SignInManager<UserMasterMetadata> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMarketingCompanyFinancialYear _iMarketingCompanyFinancialYear;

        public MarketingCompanyFinancialYearMasterController(UserManager<UserMasterMetadata> userManager, SignInManager<UserMasterMetadata> signInManager, IWebHostEnvironment hostEnvironment, IMarketingCompanyFinancialYear iMarketingCompanyFinancialYear)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            _webHostEnvironment = hostEnvironment;
            this._iMarketingCompanyFinancialYear = iMarketingCompanyFinancialYear;
        }

        /*MarketingCompanyFinancialYearMaster Index Page Start*/
        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<FinancialYearMaster>> columns = c =>
            {

                c.Add(o => o.SrNo, "SrNo")
                    .Titled("SrNo")
                    .Sortable(true)
                    .SetWidth(10);

                c.Add(o => o.CurrentYear)
                    .Titled("Running Year")
                    .Sortable(true)
                    .SetWidth(20);

                c.Add(o => o.FinancialYear)
                    .Titled("Financial Year")
                    .Sortable(true)
                    .SetWidth(20);                

                c.Add(o => o.StartMonth)
                    .Titled("Start Month")
                    .Sortable(true)
                    .SetWidth(10);

                c.Add(o => o.EndMonth)
                    .Titled("End Month")
                    .Sortable(true)
                    .SetWidth(10);

                c.Add(o => o.FinYearDesc)
                   .Titled("Description")
                   .Sortable(true)
                   .SetWidth(50);

                c.Add()
                    .Titled("Edit")
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(10)
                    .Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn' onclick='fnFinancialYear(this)' href='javascript:void(0)' data-id='{o.FinancialYearID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_FinancialYear' aria-controls='canvas_FinancialYear'><i class='bx bx-edit'></i> </a>");

            };

            PagedDataTable<FinancialYearMaster> pds = _iMarketingCompanyFinancialYear.GetAllFinancialYearAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<FinancialYearMaster>(pds, query, false, "ordersGrid",
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
        /*MarketingCompanyFinancialYearMaster Index Page End*/

        /*MarketingCompanyFinancialYearMaster silder Start*/
        [HttpPost]
        public PartialViewResult Get(int id, string key)
        {
            try
            {
                FinancialYearMaster model = new FinancialYearMaster();

                if (id > 0)
                {
                    model = _iMarketingCompanyFinancialYear.GetFinancialYearAsync(id).Result;

                    return PartialView("IUFinancialYear", model);
                }
                else
                {
                    return PartialView("IUFinancialYear", model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        /*MarketingCompanyFinancialYearMaster silder End*/

        /*MarketingCompanyFinancialYearMaster Insert or Update Page Start*/
        [HttpPost]
        public async Task<IActionResult> IOrUFinancialYear(FinancialYearMaster model)
        {
            model.CreatedOrModifiedBy = USERID;
            var _FinancialYearID = await _iMarketingCompanyFinancialYear.InsertOrUpdateFinancialYearAsync(model);

            if (_FinancialYearID > 0)
            {
                model.FinancialYearID = _FinancialYearID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
                return Json(new { status = false, message = MessageHelper.Error });
        }
        /*MarketingCompanyFinancialYearMaster Insert or Update Page End*/
    }
}
