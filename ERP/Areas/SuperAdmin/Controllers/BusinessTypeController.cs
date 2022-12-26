using Business.Entities;
using Business.Interface;
using Business.SQL;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using GridShared.Sorting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils.Messaging;
using NLog.Targets;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
namespace ERP.Areas.SuperAdmin.Controllers
{


    [Area("SuperAdmin"), Authorize]
    [DisplayName("BusinessType")]
    public class BusinessTypeController : SettingsController
    {
        private readonly ISuperAdminService _superAdmin;

        public BusinessTypeController(ISuperAdminService superAdmin)
        {
            this._superAdmin = superAdmin;
        }

        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            try
            {
                int userid = USERID;
                IQueryCollection query = Request.Query;

                Action<IGridColumnCollection<BusinessTypeMasterMetadata>> columns = c =>
                {
                    c.Add(o => o.BusinessTypeText)
                        .Titled("Business Type")
                        .SortInitialDirection(GridSortDirection.Ascending)
                        .SetWidth(110);
                    c.Add()
                        .Encoded(false)
                        .Sanitized(false)
                        .SetWidth(60)
                        .RenderValueAs(o => $"<a class='btn' onclick = 'fnBusinessType(this)' href = 'javascript:void(0)' data-id='{o.BusinessTypeID}'  data-bs-toggle='offcanvas' data-bs-target='#canvas_business' aria-controls='canvas_business'><i class='bx bx-edit'></i></a>");
                     
                };
                PagedDataTable<BusinessTypeMasterMetadata> pds = _superAdmin.GetAllBusinessTypeAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
                var server = new GridCoreServer<BusinessTypeMasterMetadata>(pds, query, false, "ordersGrid",
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
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public async Task<PartialViewResult> Get(int id)
        {

            try
            {
                BusinessTypeMasterMetadata model =await _superAdmin.GetBusinessTypeAsync(id);
                if (model == null)
                    model = new BusinessTypeMasterMetadata();
                return PartialView("_add", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(BusinessTypeMasterMetadata model)
        {
            try
            {
                int result = await _superAdmin.InsertOrUpdateBusinessTypeAsync(model);
                if (result > 0)
                {
                    if (model.BusinessTypeID== 0)
                        return Json(new { status = true, message = MessageHelper.Added });
                    else
                        return Json(new { status = true, message = MessageHelper.Updated });
                }
                return Json(new { status = false, message = MessageHelper.Error });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }
    }
}