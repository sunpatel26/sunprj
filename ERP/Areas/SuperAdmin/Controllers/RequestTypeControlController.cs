using Business.Entities.Dynamic;
using Business.Interface.Dynamic;
using Business.SQL;
using DocumentFormat.OpenXml.Office2010.Excel;
using ERP.Controllers;
using ERP.Helpers;
using GridCore.Server;
using GridShared;
using GridShared.Sorting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin"), Authorize]
    [DisplayName("RequestTypeControl")]
    public class RequestTypeControlController : SettingsController
    {
        private readonly IRequestTypeControl _requestTypeControl;
        private readonly IRequestTypeModule _requestTypeModule;
        private IMasterEntity _masterEntity;
        public RequestTypeControlController(IMasterEntity masterEntity, IRequestTypeControl requestTypeControl, IRequestTypeModule requestTypeModule)
        {
            _masterEntity = masterEntity;
            _requestTypeControl = requestTypeControl;
            _requestTypeModule = requestTypeModule;
        }
        public ActionResult Index(int id,int cid,[FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            try
            {
                int userid = USERID;
                IQueryCollection query = Request.Query;
                PagedDataTable<RequestTypeControlMetadata> pds = _requestTypeControl.GetList(id, cid, gridpage.ToInt(), PAGESIZE, orderby, sortby == "0" ? "ASC" : "DESC", search);
                Action<IGridColumnCollection<RequestTypeControlMetadata>> columns = c =>
                {
                  
                    c.Add(o => o.Label)
                        .Titled("Control Label")
                        .SetWidth(110);
                    c.Add(o => o.RequestTypeName)
                      .Titled("Case Type")
                      .SetWidth(110);

                    c.Add(o => o.RequestTypeTitle)
                      .Titled("Section")
                      .SetWidth(110);

                    c.Add(o => o.Type)
                      .Titled("Data Type")
                      .SetWidth(110);

                    c.Add(o => o.IsActive)
                       .Titled("Active")
                       .SetWidth(110)
                       .RenderValueAs(o => o.IsActive.toStringWithYAndN());
                    c.Add(o => o.SortOrder)
                      .Titled("Display Order")
                      .SetWidth(110);
                    c.Add()
                        .Encoded(false)
                        .Sanitized(false)
                        .SetWidth(60)
                        .RenderValueAs(o =>
                            string.Format("<a data-bs-toggle=\"offcanvas\"\r\n data-bs-target=\"#canvas_masterentity\"\r\n aria-controls=\"canvas_masterentity\" href='javascript:void(0)' onclick = 'fnControls(this)' data-id='{0}'  class='btn'>Edit</a>", o.RequestTypeControlID) +
                             string.Format("<a class='btn' class='btn' onclick = 'fnDeleteControls(this)' data-id='{0}' href = 'javascript:void(0)' >Delete</a>",o.RequestTypeControlID));

                };
                GridSettings settings = new GridSettings();
                settings.QueryString = id.ToString();
                var server = new GridCoreServer<RequestTypeControlMetadata>(pds, query, false, "ordersGrid",
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
                    .CommonSettings(settings)
                ;
                return View(server.Grid);
            }
            catch
            {
                throw;
            }
        }

        //public ActionResult Index(int id, int? caseTypeId = null)
        //{
        //    try
        //    {
        //        RequestTypeControlListMetadata model = new RequestTypeControlListMetadata() { CaseTypeIdQuerystringParam = caseTypeId };
        //        model.RequestTypeTitle = _requestTypeModule.GetDetail(id);
        //        model.Lists = _requestTypeControl.GetList(id, COMPANYID);
        //        model.ValidationRules = _requestTypeControl.GetValidationRules().Select(p => new DropdownMetadata() { Text = p.Name, Value = p.ControlValidationRuleID.ToString() }).ToList();
        //        model.Types = _requestTypeControl.GetControlTypes();
        //        model.DataKeys = _masterEntity.GetDropdownKeys(COMPANYID);

        //        return View(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message);
        //        throw;
        //    }
        //}

        [HttpPost]
        public JsonResult Save(RequestTypeControlMetadata model)
        {
            bool flag = false;
            try
            {
                _requestTypeControl.Save(model.RequestTypeControlID, model.RequestTypeTitleID, model.Label, model.Type, model.IsRequired, model.DefaultValue, model.DataKey, model.IsActive, model.AccessByRoles, model.ControlValidationRuleID, USERID, model.Tooltip, model.MinLength, model.MaxLength, model.SortOrder);
                if (model.RequestTypeControlID == 0)
                    return Json(new { status = true, message = MessageHelper.Added });
                else
                    return Json(new { status = true, message = MessageHelper.Updated });
            }
            catch (Exception ex)
            {
                string message = (ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                string error = GetError(ex);
                _logger.LogError(error);
                if (ex.Message.ToUpper().Contains("UNIQUE KEY constraint"))
                {
                    return Json(new { status = false, message = "Control already exist." });
                    }
                else
                {
                    return Json(new { status = false, message = MessageHelper.Error });
                }                
                throw;
            }
        }
        [HttpPost]
        public PartialViewResult Get(int id)
        {
            try
            {
                RequestTypeControlMetadata model = _requestTypeControl.GetDetail(id);

                model ??= new RequestTypeControlMetadata();
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
                flag = _requestTypeControl.Delete(id);
                return Json(new { flag });
            }
            catch (Exception ex)
            {
                string message = (ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                if (message.Contains("DELETE statement conflicted with the REFERENCE constraint"))
                {
                    message = "Cannot delete control, there are one or more mapping exist.";
                    return Json(new { status = false, message = message });
                }
                _logger.LogError(ex, ex.Message);                
                throw;
            }
        }

        public ActionResult AllControls()
        {
            try
            {
                RequestTypeControlListMetadata model = new RequestTypeControlListMetadata() { };
                model.Lists = _requestTypeControl.GetList(null, COMPANYID);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                RequestTypeControlListMetadata model = new RequestTypeControlListMetadata() { ControlID = id };
                var control = _requestTypeControl.GetDetail(id);
                model.RequestTypeTitle = _requestTypeModule.GetDetail(control.RequestTypeTitleID);
                model.Lists = _requestTypeControl.GetList(control.RequestTypeTitleID);
                model.ValidationRules = _requestTypeControl.GetValidationRules().Select(p => new DropdownMetadata() { Text = p.Name, Value = p.ControlValidationRuleID.ToString() }).ToList();
                model.Types = _requestTypeControl.GetControlTypes();
                model.DataKeys = _masterEntity.GetDropdownKeys(COMPANYID);

                return View("Index", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
