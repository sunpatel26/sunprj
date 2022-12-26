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
    [DisplayName("DocumentType")]
    public class DocumentTypeController : SettingsController
    {
        private readonly ISuperAdminService _superAdmin;

        public DocumentTypeController(ISuperAdminService superAdmin)
        {
            this._superAdmin = superAdmin;
        }

        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            try
            {
                IQueryCollection query = Request.Query;

                Action<IGridColumnCollection<DocumentTypeMasterMetadata>> columns = c =>
                {
                    c.Add(o => o.DocumentTypeName)
                        .Titled("DocumentType")
                        .SortInitialDirection(GridSortDirection.Ascending)
                        .SetWidth(110);
                    c.Add()
                        .Encoded(false)
                        .Sanitized(false)
                        .SetWidth(60)
                     .RenderValueAs(o => $"<a class='btn' onclick = 'fnDocumentType(this)' href = 'javascript:void(0)' data-id='{o.DocumentTypeID}'  data-bs-toggle='offcanvas' data-bs-target='#canvas_documenttype' aria-controls='canvas_documenttype'><i class='bx bx-edit'></i></a>");
                };
                PagedDataTable<DocumentTypeMasterMetadata> pds = _superAdmin.GetAllDocumentTypeAsync(gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
                var server = new GridCoreServer<DocumentTypeMasterMetadata>(pds, query, false, "ordersGrid",
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
                DocumentTypeMasterMetadata model =await _superAdmin.GetDocumentTypeAsync(id);
                if (model == null)
                    model = new DocumentTypeMasterMetadata();
                return PartialView("_add", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(DocumentTypeMasterMetadata model)
        {
            try
            {
                int result = await _superAdmin.InsertOrUpdateDocumentTypeAsync(model);
                if (result > 0)
                {
                    if (model.DocumentTypeID== 0)
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