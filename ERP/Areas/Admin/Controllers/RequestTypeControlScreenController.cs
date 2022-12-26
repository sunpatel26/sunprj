using Business.Entities.Dynamic;
using Business.Interface.Dynamic;
using ERP.Areas.SuperAdmin.Controllers;
using GridCore.Server;
using GridShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    [DisplayName("RequestTypeControlScreen")]
    public class RequestTypeControlScreenController : BaseController
    {
        private IRequestTypeControlScreenMapping _requestTypeControlScreenMapping;
        public RequestTypeControlScreenController(IRequestTypeControlScreenMapping requestTypeControlScreenMapping)
        {
            _requestTypeControlScreenMapping = requestTypeControlScreenMapping;
        }
        public ActionResult Index(int id = 0)
        {

            try
            {
                int userid = USERID;
                IQueryCollection query = Request.Query;
                List<RequestTypeControlScreenMappingMetadata> pds = _requestTypeControlScreenMapping.GetList(COMPANYID);
                Action<IGridColumnCollection<RequestTypeControlScreenMappingMetadata>> columns = c =>
                {

                    c.Add(o => o.EntityName)
                        .Titled("Entity Name")
                        .SetWidth(110);

                    c.Add(o => o.RequestTypeName)
                        .Titled("Request Type")
                        .SetWidth(110);

                    c.Add(o => o.RequestTypeControlName)
                       .Titled("Control Name")
                       .SetWidth(110);

                    c.Add(o => o.ScreenName)
                        .Titled("Screen Name")
                        .SetWidth(110);

                    c.Add(o => o.RenderTypeName)
                           .Titled("Render TYpe")
                           .SetWidth(110);
                    c.Add()
                        .Encoded(false)
                        .Sanitized(false)
                        .SetWidth(60)
                        .RenderValueAs(o =>
                           string.Format("<a href='{0}' class='btn'>Controls</a>", Url.Action("Index", "RequestTypeControl", new { id = o.RequestTypeControlScreenID })) +
                           string.Format("<a data-bs-toggle='offcanvas' data-bs-target='#canvas_masterentity' aria-controls='canvas_masterentity' href='javascript:void(0)'  onclick = 'fnMasterEntity(this)' data-id='{0}' class='btn'>Edit</a>", o.RequestTypeControlScreenID) +
                           string.Format("<a class='btn' class='btn' onclick = 'fnDelete(this)' data-id='{0}' href = 'javascript:void(0)' >Delete</a>", o.RequestTypeControlScreenID));

                };
                var server = new GridCoreServer<RequestTypeControlScreenMappingMetadata>(pds, query, false, "ordersGrid",
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
        //public ActionResult Index()
        //{
        //    try
        //    {
        //        RequestTypeControlScreenMappingListMetadata model = new RequestTypeControlScreenMappingListMetadata();
        //        model.Lists = _requestTypeControlScreenMapping.GetList(COMPANYID);
        //        model.RenderTypes = _requestTypeControlScreenMapping.GetControlRenderTypes();
        //        model.RequestTypes = _requestTypeControlScreenMapping.GetRequestTypes(COMPANYID);
        //        model.Screens = _requestTypeControlScreenMapping.GetScreenNameTypes();
        //        ViewBag.ValueMasterList = null;
        //        return View(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message);
        //        throw;
        //    }
        //}

        [HttpPost]
        public JsonResult Save(int RequestTypeControlScreenID, int RequestTypeID, int RequestTypeControlID, int? RoleID, string ScreenName, string RenderType)
        {
            try
            {
                bool flag = false;
                RequestTypeControlScreenID = _requestTypeControlScreenMapping.Save(RequestTypeControlScreenID, RequestTypeID, RequestTypeControlID, RoleID, ScreenName, RenderType, USERID);
                flag = true;
                return Json(new { flag, RequestTypeControlScreenID });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public JsonResult Get(int id)
        {
            try
            {
                var model = _requestTypeControlScreenMapping.GetDetail(id);
                bool flag = true;
                return Json(new { flag, model });
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
                flag = _requestTypeControlScreenMapping.Delete(id);
                return Json(new { flag });
            }
            catch (Exception ex)
            {
                string message = (ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                if (message.Contains("DELETE statement conflicted with the REFERENCE constraint"))
                {
                    message = "Cannot delete control mapping";
                    return Json(new { flag, message });
                }
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public JsonResult GetControlsByRequestType(int requestTypeId)
        {
            try
            {
                var model = _requestTypeControlScreenMapping.GetControlsByRequestType(requestTypeId);
                bool flag = true;
                return Json(new { flag, model });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
