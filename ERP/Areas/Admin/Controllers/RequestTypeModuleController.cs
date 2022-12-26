using Business.Entities.Dynamic;
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    [DisplayName("RequestTypeModule")]
    public class RequestTypeModuleController : SettingsController
    {
        private IRequestTypeModule _requestTypeModule;
        private IRequestType _requestType;
        public RequestTypeModuleController(IRequestTypeModule requestTypeModule, IRequestType requestType)
        {
            _requestTypeModule = requestTypeModule;
            _requestType = requestType;
        }
        // GET: /MasterList/

        public ActionResult Index(int id = 0)
        {

            try
            {
                int userid = USERID;
                IQueryCollection query = Request.Query;
                List<RequestTypeTitleMetadata> pds= _requestTypeModule.GetList(id, COMPANYID);
                Action<IGridColumnCollection<RequestTypeTitleMetadata>> columns = c =>
                {
                   
                    c.Add(o => o.RequestTypeName)
                        .Titled("Request Type")
                        .SetWidth(110);

                    c.Add(o => o.Name)
                        .Titled("Name")
                        .SetWidth(110);

                    c.Add(o => o.IsActive)
                       .Titled("Active")
                       .SetWidth(110)
                       .RenderValueAs(o => o.IsActive.toStringWithYAndN());

                    c.Add(o => o.SortOrder)
                        .Titled("Sort Order")
                        .SetWidth(110);
                    c.Add()
                        .Encoded(false)
                        .Sanitized(false)
                        .SetWidth(60)
                        .RenderValueAs(o =>
                           string.Format("<a href='{0}' class='btn'>Controls</a>",Url.Action("Index","RequestTypeControl",new { id=o.RequestTypeTitleID })) +
                           string.Format("<a data-bs-toggle='offcanvas' data-bs-target='#canvas_masterentity' aria-controls='canvas_masterentity' href='javascript:void(0)'  onclick = 'fnMasterEntity(this)' data-id='{0}' class='btn'>Edit</a>", o.RequestTypeTitleID) +
                           string.Format("<a class='btn' class='btn' onclick = 'fnDelete(this)' data-id='{0}' href = 'javascript:void(0)' >Delete</a>", o.RequestTypeTitleID));

                };
                var server = new GridCoreServer<RequestTypeTitleMetadata>(pds, query, false, "ordersGrid",
                    columns, PAGESIZE, pds.Count)
                    .Sortable()
                    .Searchable(true, false)
                    .ClearFiltersButton(false)
                    .SetStriped(true)
                    .ChangePageSize(true)
                    .WithGridItemsCount()
                    .WithPaging(PAGESIZE, pds.Count)
                    .ChangeSkip(false)
                    .EmptyText("No record found")
                    ;
                return View(server.Grid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            
        }

        [HttpPost]
        public JsonResult Save(RequestTypeTitleMetadata model)
        {            
            try
            {
                _requestTypeModule.Save(model.RequestTypeTitleID, model.RequestTypeID, model.Name, model.SortOrder, model.IsActive, USERID);
                if (model.RequestTypeTitleID == 0)
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
                RequestTypeTitleMetadata model = _requestTypeModule.GetDetail(id);

                if (model == null)
                {
                    model = new RequestTypeTitleMetadata();
                }
                return PartialView("_add", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        
        public JsonResult Delete(int id)
        {
            bool flag = false;
            try
            {
                flag = _requestTypeModule.Delete(id);
                return Json(new { flag });
            }
            catch (Exception ex)
            {
                string message = (ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                if (message.Contains("DELETE statement conflicted with the REFERENCE constraint"))
                {
                    message = "Cannot delete case section, there are one or more controls exist.";
                    return Json(new { status = false, message = message });
                }
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
