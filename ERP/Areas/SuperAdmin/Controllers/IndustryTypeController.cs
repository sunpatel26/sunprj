using Business.Entities;
using Business.Interface;
using Business.SQL;
using ERP.Controllers;
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
    [DisplayName("BusinessType")]
    public class IndustryTypeController : SettingsController
    {
        private readonly ISuperAdminService _superAdmin;

        public IndustryTypeController(ISuperAdminService superAdmin)
        {
            this._superAdmin = superAdmin;
        }

        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            try
            {
                int userid = USERID;
                IQueryCollection query = Request.Query;

                Action<IGridColumnCollection<IndustryTypeMasterMetadata>> columns = c =>
                {
                    c.Add(o => o.IndustryTypeText)
                        .Titled("Industry Type")
                        .SortInitialDirection(GridSortDirection.Ascending)
                        //.ThenSortByDescending(o => o.CompanyID)
                        .SetWidth(110);

                    c.Add()
                        .Encoded(false)
                        .Sanitized(false)
                        .SetWidth(60)
                       .RenderValueAs(o => $"<a class='btn' onclick = 'fnIndustryType(this)' href = 'javascript:void(0)' data-id='{o.IndustryTypeID}'  data-bs-toggle='offcanvas' data-bs-target='#canvas_industry' aria-controls='canvas_industry'><i class='bx bx-edit'></i></a>");



                };
                PagedDataTable<IndustryTypeMasterMetadata> pds = _superAdmin.GetAllIndustryTypeAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
                var server = new GridCoreServer<IndustryTypeMasterMetadata>(pds, query, false, "ordersGrid",
                    columns, PAGESIZE, pds.TotalItemCount)
                    .Sortable()
                    .Searchable(true, false)
                    .ClearFiltersButton(true)
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
                IndustryTypeMasterMetadata model = await _superAdmin.GetIndustryTypeAsync(id);
                if (model == null)
                    model = new IndustryTypeMasterMetadata();
                return PartialView("_add", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Save(IndustryTypeMasterMetadata model)
        {
            try
            {
                int result = await _superAdmin.InsertOrUpdateIndustryTypeAsync(model);
                if (result > 0)
                {
                    if (model.IndustryTypeID == 0)
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