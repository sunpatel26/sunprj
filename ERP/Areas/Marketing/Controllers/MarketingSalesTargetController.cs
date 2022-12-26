using Business.Entities;
using Business.Entities.Marketing.SalesTarget;
using Business.Interface;
using Business.Interface.Marketing.ISalesTarger;
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
    [DisplayName("SalesTarget")]
    public class MarketingSalesTargetController : SettingsController
    {
        private readonly UserManager<UserMasterMetadata> _userManager;
        private readonly SignInManager<UserMasterMetadata> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMarketingSalesTargetService _iMarketingSalesTargetService;
        private readonly IMasterService _masterService;

        public MarketingSalesTargetController(UserManager<UserMasterMetadata> userManager, SignInManager<UserMasterMetadata> signInManager, IWebHostEnvironment hostEnvironment, IMarketingSalesTargetService iMarketingSalesTargetService, IMasterService masterService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            _webHostEnvironment = hostEnvironment;
            _iMarketingSalesTargetService = iMarketingSalesTargetService;
            _masterService = masterService;
        }


        /*public IActionResult Index()
        {
            return View();
        }*/

        /*Sales Target Index Page Start*/

        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<SalesTarget>> columns = c =>
            {

                c.Add(o => o.SrNo, "SrNo")
                    .Titled("SrNo")
                    .Sortable(true)
                    .SetWidth(10);

                c.Add(o => o.CompanyName)
                    .Titled("Company")
                    .Sortable(true)
                    .SetWidth(10);

                c.Add(o => o.MarketingEmployeeName)
                    .Titled("Marketing Person")
                    .Sortable(true)
                    .SetWidth(10);

                c.Add(o => o.CustomerName)
                    .Titled("Client")
                    .Sortable(true)
                    .SetWidth(10);

                c.Add(o => o.TargetValue)
                    .Titled("Sales Target")
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
                   .SetWidth(10)
                   .Format("{0:dd/MM/yyyy}");

                c.Add(o => o.FinancialYear)
                    .Titled("Financial Year")
                    .Sortable(true)
                    .SetWidth(10);

                c.Add()
                    .Titled("Edit")
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(10)
                    .Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn' onclick='fnSalesTarget(this)' href='javascript:void(0)' data-id='{o.SalesTargetID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_SalesTarget' aria-controls='canvas_SalesTarget'><i class='bx bx-edit'></i> </a>");                
                System.Diagnostics.Debug.WriteLine("c----> " + c);
            };

            PagedDataTable<SalesTarget> pds = _iMarketingSalesTargetService.GetAllMarketingSalesTargetAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<SalesTarget>(pds, query, false, "ordersGrid",
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

        /*Sales Target Index Page End*/

        /*Sales Target silder Start*/
        [HttpPost]
        public PartialViewResult Get(int id, string key)
        {
            //System.Diagnostics.Debug.WriteLine("id-- "+id);

            try
            {
                SalesTarget model = new SalesTarget();

                var CompanyMasterList = _masterService.GetAllCompanyMasterAsync();
                ViewData["CompanyName"] = new SelectList(CompanyMasterList, "CompanyID", "CompanyName");

                var Employeeslist = _masterService.GetAllEmployees();
                ViewData["EmployeeIdName"] = new SelectList(Employeeslist, "EmployeeID", "EmployeeName");
                ViewData["ReportingHeadName"] = new SelectList(Employeeslist, "EmployeeID", "EmployeeName");

                var FinancialYearList = _masterService.GetAllFinancialYearMasterAsync();
                ViewData["FinancialYear"] = new SelectList(FinancialYearList, "FinancialYearID", "FinancialYear");

                var CustomerList = _masterService.GetAllCustomerMasterAsync();
                ViewData["CustomerName"] = new SelectList(CustomerList, "CustomerID", "CustomerName");



                if (id > 0)
                {
                    model = _iMarketingSalesTargetService.GetMarketingSalesTargetAsync(id).Result;

                    return PartialView("IUMarketingSalesTarget", model);
                }
                else
                {
                    return PartialView("IUMarketingSalesTarget", model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        /*Sales Target silder End*/

        /*Sales Target Insert or Update Page Start*/

        [HttpPost]
        public async Task<IActionResult> IOrUMarketingSalesTarget(SalesTarget model)
        {
            model.CreatedOrModifiedBy = USERID;
            var _marketingSalesTargetID = await _iMarketingSalesTargetService.InsertOrUpdateMarketingSalesTargetAsync(model);

            if (_marketingSalesTargetID > 0)
            {
                model.SalesTargetID = _marketingSalesTargetID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
                return Json(new { status = false, message = MessageHelper.Error });
        }

        /*Sales Target Insert or Update Page End*/
    }
}
