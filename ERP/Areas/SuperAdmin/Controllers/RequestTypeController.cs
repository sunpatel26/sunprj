using Business.Entities.Dynamic;
using Business.Interface;
using Business.Interface.Dynamic;
using Business.SQL;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using GridShared.Sorting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin"), Authorize]
    [DisplayName("RequestType")]
    public class RequestTypeController : SettingsController
    {
        private IEntity _masterEntity;
        private IRequestType _requestType;
        private readonly ISiteUserService _usersService;
        private readonly ISiteRoleRepository _roleService;
        public RequestTypeController(IEntity masterEntity, IRequestType requestType, ISiteRoleRepository roleService, ISiteUserService users)
        {
            this._masterEntity = masterEntity;
            this._requestType = requestType;
            this._usersService = users;
            this._roleService = roleService;
        }

        public async Task<ActionResult> Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            try
            {
                int userid = USERID;
                IQueryCollection query = Request.Query;
               
                Action<IGridColumnCollection<RequestTypeMetadata>> columns = c =>
                {
                    c.Add(o => o.CompanyName)
                       .Titled("Company Name")
                       .SortInitialDirection(GridSortDirection.Ascending)
                       .SetWidth(110);
                    c.Add(o => o.EntityName)
                       .Titled("Entity")
                       ;
                    c.Add(o => o.Name)
                        .Titled("Name")
                        .SetWidth(110);
                    c.Add(o => o.IsActive)
                       .Titled("Active")
                       .SetWidth(110)
                       .RenderValueAs(o => o.IsActive.toStringWithYAndN());
                    c.Add()
                        .Encoded(false)
                        .Sanitized(false)
                        .SetWidth(60)
                        .RenderValueAs(o =>
                            string.Format("<a href='RequestTypeModule/Index?id={0}&cid={1}' class='btn'>Section</a>", o.RequestTypeID,o.CompanyID) +
                            string.Format("<a data-bs-toggle='offcanvas' data-bs-target='#canvas_masterentity' aria-controls='canvas_masterentity' href='javascript:void(0)'  onclick = 'fnMasterEntity(this)' data-id='{0}' class='btn'>Edit</a>", o.RequestTypeID) +
                             string.Format("<a class='btn' class='btn' onclick = 'fnDelete(this)' data-id='{0}' href = 'javascript:void(0)' >Delete</a>", o.RequestTypeID));

                };
                PagedDataTable<RequestTypeMetadata> pds = await _requestType.GetAllList(0, gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC");
                var server = new GridCoreServer<RequestTypeMetadata>(pds, query, false, "ordersGrid",
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
        public JsonResult SaveRequestType(RequestTypeMetadata model)
        {
            try
            {
                _requestType.Save(model.RequestTypeID, model.EntityID, model.Name, model.AccessByRoles, model.AssignedTo, model.IsActive, USERID, model.CompanyID);
                if (model.RequestTypeID == 0)
                    return Json(new { status = true, message = MessageHelper.Added });
                else
                    return Json(new { status = true, message = MessageHelper.Updated });
            }
            catch (Exception ex)
            {
                string message = (ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                string error = GetError(ex);
                _logger.LogError(error);
                if (ex.Message.ToUpper().Contains("UNIQUE KEY"))
                {
                    return Json(new { status = false, message = MessageHelper.Duplicate });
                }
                else
                {
                    return Json(new { status = false, message = MessageHelper.Error });
                }
            }
        }
        [HttpPost]
        public PartialViewResult Get(int id)
        {
            try
            {
                RequestTypeMetadata model = _requestType.GetDetail(id, COMPANYID);
              
                if (model == null)
                {
                    model = new RequestTypeMetadata();
                }
                return PartialView("_add", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        
        public JsonResult DeleteRequestType(int id)
        {
            bool flag = false;
            try
            {
                flag = _requestType.Delete(id);
                return Json(new { flag });
            }
            catch (Exception ex)
            {
                string message = (ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                if (message.Contains("DELETE statement conflicted with the REFERENCE constraint"))
                {
                    message = "Cannot delete case type, there are one or more section exist.";
                    return Json(new { status = false, message = message });
                }
                throw;
            }
        }

        //[HttpPost]
        //public JsonResult GetAssignedToUsers(int entityId, string roles)
        //{
        //    if (string.IsNullOrEmpty(roles))
        //    {
        //        return Json(new { flag = false });
        //    }
        //    List<DropdownMetadata> users = (from item in _userList.GetAssignedToList(
        //                                                    CompanyId,
        //                                                    entityId,
        //                                                    roles.Split(',').Select(p => Convert.ToInt32(p)).ToList()
        //                                                    )
        //                                 select new DropdownMetadata() { Value = item.Value, Text = item.Text }).ToList();
        //    return Json(new { flag = true, result = users });
        //}

        //[HttpPost]
        //public JsonResult GetRoles(int entityId)
        //{
        //    //var roles = _role.GetList(COMPANYID, entityId);
        //    return Json(new { flag = true, result = roles });
        //}
    }
}
