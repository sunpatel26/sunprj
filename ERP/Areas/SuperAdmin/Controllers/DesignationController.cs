using Business.Entities.Designation;
using Business.Interface;
using Business.Interface.Designation;
using Business.SQL;
using ERP.Controllers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Web.Mvc;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;
using System.Threading.Tasks;
using ERP.Helpers;

namespace ERP.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin"), Authorize]
    [DisplayName("Designation")]
    public class DesignationController : SettingsController
    {
        private readonly IMasterService _iMasterService;
        private readonly IDesignationService _iDesignationService;
        private readonly IDesignationGroupService _iDesignationGroupService;

        public DesignationController(IMasterService iMasterService, IDesignationService iDesignationService, IDesignationGroupService iDesignationGroupService)
        {
            _iMasterService = iMasterService;
            _iDesignationService = iDesignationService;
            _iDesignationGroupService = iDesignationGroupService;            
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        /* Designation List */
        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<DesignationMaster>> columns = c =>
            {

                c.Add(o => o.SrNo).Titled("SrNo").SetWidth(20);

                c.Add(o => o.DesignationText).Titled("Designation").SetWidth(80);

                c.Add(o => o.Description).Titled("Designation Description").SetWidth(90);
                //.ThenSortByDescending(o => o.CompanyID)

                c.Add(o => o.DesignationLevel).Titled("Level").SetWidth(100);

                c.Add()
                    .Titled("Edit")
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(60)
                    .Css("hidden-xs")
                    .RenderValueAs(o => $"<a class='btn' onclick='fnDesignation(this)' href='javascript:void(0)' data-id='{o.DesignationID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_Designation' aria-controls='canvas_Desigantion'><i class='bx bx-edit'></a>");


            };
            PagedDataTable<DesignationMaster> pds = _iDesignationService.GetAllDesignationAsync(gridpage.ToInt(),
                PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<DesignationMaster>(pds, query, false, "ordersGrid",
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
            return View(server.Grid);
        }
        /* Designation List */

        /* Department silder Start*/
        [HttpPost]
        public PartialViewResult Get(int id, int key)
        {
            try
            {
                DesignationMaster model = new DesignationMaster();

                var DesignationGroupIDList = _iMasterService.GetDesignationGroupMasterAsync();
                ViewData["DesignationGroupText"] = new SelectList(DesignationGroupIDList, "DesignationGroupID", "DesignationGroupText");

                if (Convert.ToInt32(id) > 0)
                {
                    model = _iDesignationService.GetDesignationAsync(id).Result;
                    return PartialView("CreateOrUpdateDesignation", model);
                }
                else
                {
                    return PartialView("CreateOrUpdateDesignation", model);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        /* Department silder End*/

        /* Designation Create or Update Page Start*/
        [HttpPost]
        public async Task<IActionResult> DesignationCreateOrUpdate(DesignationMaster model)
        {
            model.CreatedOrModifiedBy = USERID;
            var _DesignationID = await _iDesignationService.DesignationCreateOrUpdateAsync(model);

            if (_DesignationID > 0)
            {
                model.DesignationID = _DesignationID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
                return Json(new { status = false, message = MessageHelper.Error });
        }
        /* Designation Create or Update Page End*/

        /* Add Department Group Page Start */
        public PartialViewResult AddDesignationGroup()
        {
            var model = new Business.Entities.Designation.DesignationGroupMaster();
            model.CreatedOrModifiedBy = USERID;
            /*return PartialView("AddDesignationGroup",model);*/
            return PartialView("AddDesignationGroup");
        }
        /* Add Department Group Page End */

        /* Add Department Group Name Start */
        [HttpPost]
        public async Task<IActionResult> DesignationGroupRegister(DesignationGroupMaster model)
        {
            model.CreatedOrModifiedBy = USERID;
            var result = await _iDesignationGroupService.CreateDesignationtGroupAsync(model);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("AddDesignationGroup");
            }
        }
        /* Add Department Group Name End */
    }
}
