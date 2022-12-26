using Business.Entities.Marketing.EmployeeSalesTargetSummary;
using Business.Interface.Marketing.IEmployeeSalesTargetSummary;
using Business.SQL;
using ERP.Controllers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Web.Mvc;

namespace ERP.Areas.Marketing.Controllers
{
    [Area("Marketing"), Authorize]
    [DisplayName("MESTargetSummery")]
    public class MarketingEmployeeSalesTargetSummaryController : SettingsController
    {
        private readonly IEmployeeSalesTargetSummaryService _iEmployeeSalesTargetSummaryService;

            public MarketingEmployeeSalesTargetSummaryController(IEmployeeSalesTargetSummaryService iEmployeeSalesTargetSummaryService) {
            _iEmployeeSalesTargetSummaryService = iEmployeeSalesTargetSummaryService;
        }
        /*Marketing Employee Sales Target Summary Index Page Start*/
        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<EmployeeSalesTargetSummary>> columns = c =>
            {
                c.Add(o => o.SrNo, "SrNo").Titled("SrNo").Sortable(true).SetWidth(10);
                c.Add(o => o.CompanyName).Titled("Company Name").Sortable(true).SetWidth(10);                
                c.Add(o => o.CompanyTargetValue).Titled("Comapny Target").Sortable(true).SetWidth(10);
                c.Add(o => o.MarketingEmployeeName).Titled("Marketing Person").Sortable(true).SetWidth(10);
                c.Add(o => o.CustomerName).Titled("Customer").Sortable(true).SetWidth(10);
                c.Add(o => o.TargetValue).Titled("Comapny Target").Sortable(true).SetWidth(10);
                c.Add(o => o.FinancialYear).Titled("Financial Year").Sortable(true).SetWidth(10);            
                /*c.Add(o => o.StartDate).Titled("Start Date").Sortable(true).SetWidth(10).ToString();
                c.Add(o => o.EndDate).Titled("End Date").Sortable(true).SetWidth(15);*/
                /*c.Add()
                    .Titled("Edit")
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(10)
                    .Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn' onclick='fn(this)' href='javascript:void(0)' data-id='{o.CompanyTargetID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_' aria-controls='canvas_'><i class='bx bx-edit'></i> </a>");*/

            };

            PagedDataTable<EmployeeSalesTargetSummary> pds = _iEmployeeSalesTargetSummaryService.GetAllMarketingEmployeeSalesTargetSummaryAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<EmployeeSalesTargetSummary>(pds, query, false, "ordersGrid",
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
        /*Marketing Employee Sales Target Summary Index Page End*/
    }
}
