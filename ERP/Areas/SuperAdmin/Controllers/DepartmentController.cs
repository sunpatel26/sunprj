using Business.Entities.Department;
using Business.Interface;
using Business.Interface.Department;
using Business.SQL;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;

namespace ERP.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin"), Authorize]
    [DisplayName("Department")]
    public class DepartmentController : SettingsController
    {
        private readonly IMasterService _masterService;
        private readonly IDepartmentService _departmentService;
        private readonly IDepartmentGroupService _departmentGroupService;
        public DepartmentController(IDepartmentService idepartmentservice, IDepartmentGroupService departmentGroupService, IMasterService masterService)
        {
            _departmentService = idepartmentservice;
            _departmentGroupService = departmentGroupService;
            _masterService = masterService;
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        /*Department List*/
        public ActionResult Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            int userid = USERID;
            IQueryCollection query = Request.Query;

            Action<IGridColumnCollection<DepartmentMaster>> columns = c =>
            {

                c.Add(o => o.SrNo, "SrNo")
                    .Titled("SrNo")
                    .SetWidth(20);
                c.Add(o => o.DepartmentName)
                    .Titled("Department Name")
                    .Sortable(true)
                    .SetWidth(20);
                c.Add(o => o.DepartmentGroupText)
                    .Titled("Department Group")
                    .Sortable(true)
                    .SetWidth(50);

                c.Add(o => o.IsActive)
                    .Titled("Active")
                    .Sortable(true);

                c.Add()
                    .Titled("Edit")
                    .Encoded(false)
                    .Sanitized(false)
                    .SetWidth(60)
                    .Css("hidden-xs")
                    .RenderValueAs(o => $"<a class='btn' onclick='fnDepartment(this)' href='javascript:void(0)' data-id='{o.DepartmentID}' data-bs-toggle='offcanvas' data-bs-target='#canvas_Department' aria-controls='canvas_masterentity'><i class='bx bx-edit'></a>");


            };
            PagedDataTable<DepartmentMaster> pds = _departmentService.GetAllDepartmentAsync(gridpage.ToInt(),
                PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC").Result;
            var server = new GridCoreServer<DepartmentMaster>(pds, query, false, "ordersGrid",
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
        /*Department List*/

        /* Department silder Start*/
        [HttpPost]
        public PartialViewResult Get(int id, int key)
        {
            try
            {                
                DepartmentMaster model = new DepartmentMaster();

                var DepartmentGroupIDList = _masterService.GetDepartmentGroupsMasterAsync();
                ViewData["DepartmentGroupText"] = new SelectList(DepartmentGroupIDList, "DepartmentGroupID", "DepartmentGroupText");

                if (Convert.ToInt32(id) > 0)
                {
                    model = _departmentService.GetDepartmentAsync(id).Result;                    
                    return PartialView("CreateOrUpdateDepartment", model);
                }
                else 
                {
                    return PartialView("CreateOrUpdateDepartment", model);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        /* Department silder End*/

        /* Department Create or Update Page Start*/
        [HttpPost]
        public async Task<IActionResult> DepartmemtCreateOrUpdate(DepartmentMaster model)
        {
            model.CreatedOrModifiedBy = USERID;
            var _DepartmentID = await _departmentService.DepartmemtCreateOrUpdateAsync(model);

            if (_DepartmentID > 0)
            {
                model.DepartmentID = _DepartmentID;
                return Json(new { status = true, message = MessageHelper.Added });
            }
            else
                return Json(new { status = false, message = MessageHelper.Error });
        }
        /* Department Create or Update Page End*/

        /* Add Department Group Page Start */
        public PartialViewResult AddDepartmentGroup()
        {
            var model = new Business.Entities.Department.DepartmentGroupMaster();
            model.CreatedOrModifiedBy = USERID;
            /*return PartialView("AddDepartmentGroup",model);*/
            return PartialView("AddDepartmentGroup");
        }
        /* Add Department Group Page End */

        /* Add Department Group Name page Start */
        [HttpPost]
        public async Task<IActionResult> DepartmentGroupRegister(DepartmentGroupMaster model)
        {
            model.CreatedOrModifiedBy = USERID;
            var result = await _departmentGroupService.CreateDepartmentGroupAsync(model);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("AddDepartmentGroup");
            }
        }
        /* Add Department Group Name page End */
    }
}
