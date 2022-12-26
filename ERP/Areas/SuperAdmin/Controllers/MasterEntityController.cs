using Business.Entities;
using Business.Entities.Dynamic;
using Business.Interface.Dynamic;
using Business.SQL;
using DocumentFormat.OpenXml.Wordprocessing;
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
    [DisplayName("MasterEntity")]
    public class MasterEntityController : SettingsController
    {
        private IMasterEntity _masterEntity;
        public MasterEntityController(IMasterEntity masterEntity)
        {
            _masterEntity = masterEntity;
        }
        public async Task<ActionResult> Index([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-column")] string orderby = "", [FromQuery(Name = "grid-dir")] string sortby = "0", [FromQuery(Name = "grid-filter")] string gridfilter = "", [FromQuery(Name = "grid-search")] string search = "")
        {
            try
            {
                int userid = USERID;
                IQueryCollection query = Request.Query;
                MasterEntityListMetadata model = new MasterEntityListMetadata();
                //model.MasterLists = _masterEntity.GetDistinctNameList(COMPANYID);
                Action<IGridColumnCollection<MasterEntityMetadata>> columns = c =>
                {
                    c.Add(o => o.Name)
                        .Titled("Name")
                        .SortInitialDirection(GridSortDirection.Ascending)
                        .SetWidth(110);
                    c.Add()
                        .Encoded(false)
                        .Sanitized(false)
                        .SetWidth(60)
                        .RenderValueAs(o => string.Format("<a href='MasterEntity/GetKeyByName?id={0}' class='btn'><i class='bx bx-list-ol'></i></a>", o.Name) +
                             string.Format("<a class='btn' class='btn' onclick = 'fnDeleteMasterEntityByKey(this)' data-key='{0}' data-id='{1}' href = 'javascript:void(0)' ><i class='bx bx-trash-alt'></i></a>", o.Name,o.MasterListID));

            };
                PagedDataTable<MasterEntityMetadata> pds = await _masterEntity.GetDistinctNameList(COMPANYID, gridpage.ToInt(), PAGESIZE, search, orderby.RemoveSpace(), sortby == "0" ? "ASC" : "DESC");
                var server = new GridCoreServer<MasterEntityMetadata>(pds, query, false, "ordersGrid",
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

        public ActionResult GetKeyByName(string id)
        {
            try
            {
                int userid = USERID;
                IQueryCollection query = Request.Query;

                List<MasterEntityMetadata> model = new List<MasterEntityMetadata>();
                model = _masterEntity.GetListByName(id, COMPANYID);
                Action<IGridColumnCollection<MasterEntityMetadata>> columns = c =>
                {
                    c.Add(o => o.Name)
                        .Titled("Name")
                        .SortInitialDirection(GridSortDirection.Ascending)
                        .SetWidth(110);
                    c.Add(o => o.Value)
                        .Titled("Value")
                        .SortInitialDirection(GridSortDirection.Ascending)
                        .SetWidth(110);
                    c.Add()
                        .Encoded(false)
                        .Sanitized(false)
                        .SetWidth(60)
                        .RenderValueAs(o => string.Format("<a class='btn' onclick = 'fnMasterEntity(this)' href = 'javascript:void(0)'  data-key='{0}' data-id='{1}'  data-bs-toggle='offcanvas' data-bs-target='#canvas_masterentity' aria-controls='canvas_masterentity'><i class='bx bx-edit'></i></a>", o.Name, o.MasterListID)+ 
                        string.Format("<a class='btn' class='btn' onclick = 'fnDeleteMasterEntity(this)' data-key='{0}' data-id='{1}' href = 'javascript:void(0)' ><i class='bx bx-trash-alt'></i></a>", o.Name, o.MasterListID));

            };
                GridSettings settings = new GridSettings();
                settings.QueryString = id;
                var server = new GridCoreServer<MasterEntityMetadata>(model, query, false, "ordersGrid",
                    columns, PAGESIZE, model.Count)
                    .Sortable()
                    // .Searchable(true, false)
                    .ClearFiltersButton(false)
                    .SetStriped(true)
                    .ChangePageSize(true)
                    .WithGridItemsCount()
                    .WithPaging(PAGESIZE, model.Count)
                    .ChangeSkip(false)
                    .EmptyText("No record found")
                    .CommonSettings(settings);
                ;
                return View("Index", server.Grid);
            }
            catch
            {
                throw;
            }
        }
        //public ActionResult Index()
        //{
        //    try
        //    {
        //        MasterEntityListMetadata model = new MasterEntityListMetadata();
        //        model.MasterLists = _masterEntity.GetDistinctNameList(COMPANYID);
        //        model.EntryTypeLists = _masterEntity.GetMasterEntryTypeList();
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
        public JsonResult Save(MasterEntityMetadata item)
        {
            bool flag = false;
            
            try
            {
               int id = _masterEntity.Save(item.MasterListID, item.Name, item.Value, item.SortOrder, item.IsActive, USERID, item.EntryTypeID, COMPANYID, IsDefaultSelected:item.IsDefaultSelected);
                if (item.MasterListID == 0)
                    return Json(new { status = true, message = MessageHelper.Added });
                else
                    return Json(new { status = true, message = MessageHelper.Updated });
            }
            catch (Exception ex)
            {
                string error = GetError(ex);
                if (error.Contains("Cannot insert duplicate key"))
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
        public PartialViewResult Get(int id, string key)
        {

            try
            {
                MasterEntityMetadata model = new MasterEntityMetadata();
                model = _masterEntity.GetDetail(id, COMPANYID);
                if (model == null)
                {
                    model = new MasterEntityMetadata() { Name = key };
                }
                return PartialView("_add", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        //public JsonResult Get(int id)
        //{
        //    try
        //    {
        //        MasterEntityMetadata model = new MasterEntityMetadata();
        //        bool flag = false;
        //        model = _masterEntity.GetDetail(id, COMPANYID);
        //        flag = true;
        //        return Json(new { flag, model });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message);
        //        throw;
        //    }
        //}
        [HttpPost]
        public JsonResult DeleteMasterList(int id)
        {
            try
            {
                bool flag = false;
                flag = _masterEntity.Delete(id);
                if (flag)
                    return Json(new { status = flag, message = MessageHelper.Deleted });
                return Json(new { status = flag, message = MessageHelper.RecordUsed });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }

        public ActionResult GetMasterListKeyDetail(string name)
        {
            try
            {
                List<MasterEntityMetadata> model = new List<MasterEntityMetadata>();
                model = _masterEntity.GetListByName(name, COMPANYID);
                ViewBag.ValueMasterList = model;
                var firstItem = model.FirstOrDefault();
                ViewBag.DefaultType = firstItem != null ? firstItem.EntryTypeID : 0;
                ViewBag.Name = firstItem != null ? firstItem.Name : "";
                return PartialView("P_List");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        [HttpPost]
        public JsonResult DeleteMasterListByName(string name)
        {
            try
            {
                bool flag = false;
                flag = _masterEntity.Delete(name);
                if(flag)
                    return Json(new { status = flag, message = MessageHelper.Deleted });
                return Json(new { status = flag, message = MessageHelper.RecordUsed });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }
    }
}
