using Business.Entities;
using Business.Interface;
using Business.SQL;
using ERP.Controllers;
using ERP.Extensions;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using GridShared.Sorting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
namespace ERP.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin"), Authorize]
    [DisplayName("Taluka")]
    public class TalukaController : BaseController
    {
        private readonly ISuperAdminService _superAdmin;

        public TalukaController(ISuperAdminService superAdmin)
        {
            this._superAdmin = superAdmin;
        }

        public async Task<IActionResult> Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            try
            {
                IQueryCollection query = Request.Query;

                Action<IGridColumnCollection<TalukaMasterMetadata>> columns = c =>
                {
                    c.Add(o => o.TalukaName)
                      .Titled("Taluka Name")
                      .SortInitialDirection(GridSortDirection.Ascending)
                      .SetWidth(110);

                    c.Add(o => o.DistrictName)
                       .Titled("District Name")
                       .SetWidth(110);
                    c.Add(o => o.StateName)
                        .Titled("State Name")
                        .SetWidth(110);

                    c.Add(o => o.CountryName)
                        .Titled("Country Name")
                        .SortInitialDirection(GridSortDirection.Ascending)
                        .SetWidth(110);
                    c.Add(o => o.CountryShortName)
                           .Titled("Country Short Name")
                           .SortInitialDirection(GridSortDirection.Ascending)
                           .SetWidth(110);
                    c.Add()
                        .Encoded(false)
                        .Sanitized(false)
                        .SetWidth(60)
                     .RenderValueAs(o => $"<a class='btn' onclick = 'fnTaluka(this)' href = 'javascript:void(0)' data-id='{o.TalukaID}'  data-bs-toggle='offcanvas' data-bs-target='#canvas_taluka' aria-controls='canvas_taluka'><i class='bx bx-edit'></i></a>");
                };
                PagedDataTable<TalukaMasterMetadata> pds =await _superAdmin.GetAllTalukaAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC");
                var server = new GridCoreServer<TalukaMasterMetadata>(pds, query, false, "ordersGrid",
                    columns, PAGESIZE, pds.TotalItemCount)
                    .Sortable()
                    .Searchable(true, false)
                    .ClearFiltersButton(false)
                    .SetStriped(true)
                    .ChangePageSize(true)
                    .WithGridItemsCount()
                    .WithPaging(PAGESIZE, pds.TotalItemCount)
                    .ChangeSkip(false)
                    .EmptyText("No record found")
                    ;
                return View(server.Grid);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<PartialViewResult> Get(int id)
        {
            try
            {
                TalukaMasterMetadata model = await _superAdmin.GetTalukaAsync(id);
                if (model == null)
                    model = new TalukaMasterMetadata();
                return PartialView("_add", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(TalukaMasterMetadata model)
        {
            try
            {
                int result = await _superAdmin.InsertOrUpdateTalukaAsync(model);
                if (result > 0)
                {
                    if (model.StateID == 0)
                        return Json(new { status = true, message = MessageHelper.Added });
                    else
                        return Json(new { status = true, message = MessageHelper.Updated });
                }
                return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }               
    }
}