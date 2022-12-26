using Business.Entities.FormMasterEntitie;
using Business.Interface;
using Business.Interface.IFormMaster;
using Business.SQL;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;

namespace ERP.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin"), Authorize]
    [DisplayName("FormMaster")]
    public class FormMasterController : SettingsController
    {
        private readonly IFormMasterService _iFormMasterService;
        private readonly IMasterService _masterService;
        public FormMasterController(IFormMasterService iFormMasterService, IMasterService masterService)
        {
            _iFormMasterService = iFormMasterService;
            _masterService = masterService;
        }

        /* FormMaster Index Page Start */
        [HttpGet]
        public IActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<FormMaster>> columns = c =>
            {
                c.Add(o => o.SrNo).Titled("SrNo").SetWidth(10);
                c.Add(o => o.FormName).Titled("Form Name").SetWidth(70);
                c.Add(o => o.Area).Titled("Area Name").SetWidth(160);
                c.Add(o => o.Controller).Titled("Cantroller Name").SetWidth(160);                
                c.Add(o => o.Action).Titled("Action Name").SetWidth(160);
                c.Add(o => o.FormTypeText).Titled("Form Type Name").SetWidth(160);
                c.Add(o => o.IsActive).Titled("Status").SetWidth(160);

                c.Add().Titled("Edit").Encoded(false).Sanitized(false).SetWidth(60).Css("hidden-xs") //hide on phones
                    .RenderValueAs(o => $"<a class='btn' onclick='fnForm(this)' href='javascript:void(0)' data-id='{o.FormID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_Form' aria-controls='canvas_Form' ><i class='bx bx-edit'></i></a>");
            };
            PagedDataTable<FormMaster> pds = _iFormMasterService.GetAllFormMasterAsync(gridpage.ToInt(),
                PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<FormMaster>(pds, query, false, "ordersGrid",
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
        /* FormMaster Index Page End */

        /* FormMaster  silder Start */

        [HttpPost]
        public PartialViewResult Get(int id, string key)
        {
            try
            {
                FormMaster model = new FormMaster();

                var FormTypeMasterList = _masterService.GetAllFormTypeMasterAsync();
                ViewData["FormTypeText"] = new SelectList(FormTypeMasterList, "FormTypeID", "FormTypeText");

                var PackageMasterList = _masterService.GetAllPackageMasterAsync();
                ViewData["PackageName"] = new SelectList(PackageMasterList, "PackageID", "PackageName");

                if (id > 0)
                {
                    model = _iFormMasterService.GetFormMasterAsync(id).Result;
                    return PartialView("IUFormMaster", model);
                }
                else
                {
                    return PartialView("IUFormMaster", model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        /* FormMaster  silder End */

        /* FormMaster  Insert or Update Page Start */

        [HttpPost]
        public async Task<IActionResult> IOrUFormMaster(FormMaster model)
        {
            model.CreatedOrModifiedBy = USERID;
            var _FormID = await _iFormMasterService.InsertOrUpdateFormMasterAsync(model);

            if (_FormID > 0)
            {
                model.FormID = _FormID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
                return Json(new { status = false, message = MessageHelper.Error });
        }

        /* FormMaster  Insert or Update Page End */
    }
}
