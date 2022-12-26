using Business.Entities.Dynamic;
using Business.Interface.Dynamic;
using Business.SQL;
using ERP.Controllers;
using ERP.Helpers;
using GridMvc.Server;
using GridShared;
using GridShared.Sorting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;
using ERP.Extensions;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ERP.Areas.Admin.Controllers

{
    [Area("Admin"), Authorize]
    [DisplayName("Request")]
    public class RequestController : SettingsController
    {
        #region "Private Variable And Constructor"
        private readonly IRequestTypeControl _requestTypeControl;
        private readonly IRequestTypeModule _requestTypeModule;
        private readonly IRequestService _requestService;
        private readonly IRequestType _requestType;
        private IMasterEntity _masterEntity;
        private readonly IRequestTypeControlScreenMapping _requestControlScreenMapping;
        public RequestController(IMasterEntity masterEntity, IRequestTypeControl requestTypeControl, IRequestTypeModule requestTypeModule, IRequestService requestService, IRequestType requestType, IRequestTypeControlScreenMapping requestControlScreenMapping)
        {
            _masterEntity = masterEntity;
            _requestTypeControl = requestTypeControl;
            _requestTypeModule = requestTypeModule;
            _requestService = requestService;
            _requestType = requestType;
            _requestControlScreenMapping = requestControlScreenMapping;
        }
        #endregion

        #region "Create case or request"
        public IActionResult Index()
        {
            try
            {
                ModelCaseSubmit model = null;
                if (model == null)
                {
                    model = new ModelCaseSubmit() { RequestType = new RequestTypeMetadata(), OwnerShipID = USERID };
                }
                    return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion

        #region "Requet Type"
        [HttpGet]
        public JsonResult GetRequestTypes(int entityId)
        {
            try
            {
                var data = _requestType.GetDropdownList(entityId);
                return Json(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion

        #region "Get Controls"
        public ActionResult GetControls(int caseId, int requestTypeId)
        {
            try
            {
                ModelCaseSubmit model = null;
                //Assign already saved data to each controls default value.
                if (caseId > 0)
                {
                    model = _requestService.Get(caseId, COMPANYID, true);
                }

                if (model == null)
                {
                    model = new ModelCaseSubmit();
                }
                model.RequestType = _requestType.GetDetail(requestTypeId, COMPANYID);
                model.ValidationRules = _requestTypeControl.GetValidationRules().Where(p => !string.IsNullOrWhiteSpace(p.Rule)).Select(p => new DropdownMetadata() { Text = p.Name.Replace("+", "").Replace(" ", "").ToLower(), Value = p.Rule }).ToList();
                if (model.RequestID == 0)
                {
                    model.ControlScreenMapping = _requestControlScreenMapping.GetList("New Case");
                }
                else
                {
                    model.ControlScreenMapping = _requestControlScreenMapping.GetList("Update Case");
                }

                model.Sections = _requestService.GetControls(requestTypeId, COMPANYID);
                if (model.Data != null)
                {
                    foreach (var section in model.Sections)
                    {
                        foreach (var control in section.Controls)
                        {
                            var data = model.Data.FirstOrDefault(p => p.Key == control.RequestTypeControlID);
                            if (data != null)
                            {
                                if (control.Type.Equals(ConstVariable.TYPE_FILEUPLOAD))
                                {
                                    control.DefaultValue = Newtonsoft.Json.JsonConvert.SerializeObject(data.Files);
                                }
                                else
                                {
                                    control.DefaultValue = data.Value;
                                }
                            }
                        }
                    }
                }
                return PartialView("_controls", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion

        #region "Submit Request"
        [HttpPost]
        public IActionResult SubmitCase(IFormCollection data)
        {
            try
            {
                int ID = 0;
                int EntityID = 0;
                int RequestTypeID = 0;
                int PriorityID = 0;
                int StatusID = 0;
                int? OwnerShipID = null;
                int? CandidateID = null;
                string CandidateName = null;
                string CC = "";
                DateTime? DueDate = null;
                string Notes = "";
                string AssignedTo = "";
                string OwnerShipName = "";
                string CreatedByName = USERNAME;
                string AssignedToName = "";

                List<ModelCaseData> caseDetail = new List<ModelCaseData>();
                foreach (string key in data.Keys)
                {
                    string value=data[key];     
                    switch (key)
                    {
                        case ConstVariable.ID:
                            ID = value.ToInt();
                            break;
                        case ConstVariable.EntityID:
                            EntityID = value.ToInt();
                            break;
                        case ConstVariable.RequestTypeID:
                            RequestTypeID = value.ToInt();
                            break;
                        case ConstVariable.PriorityID:
                            PriorityID = value.ToInt();
                            break;
                        case ConstVariable.StatusID:
                            StatusID = value.ToInt();
                            break;
                        case ConstVariable.CC:
                            CC = value;
                            break;
                        case ConstVariable.OwnerShipID:
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                OwnerShipID = value.ToInt();
                            }
                            break;
                        case ConstVariable.CandidateID:
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                CandidateID = value.ToInt();
                            }
                            break;
                        case ConstVariable.CandidateName:
                            CandidateName = value;
                            break;
                        case ConstVariable.OwnerShipName:
                            OwnerShipName = value;
                            break;
                        case ConstVariable.AssignedToName:
                            AssignedToName = value;
                            break;
                        case ConstVariable.DueDate:
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                DueDate = Convert.ToDateTime(value);
                            }
                            break;
                        case ConstVariable.Notes:
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                Notes = value;
                            }
                            break;
                        case ConstVariable.AssignedTo:
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                AssignedTo = value;
                            }
                            break;
                        default:
                            if (key.StartsWith("_") && !key.Contains("__RequestVerificationToken"))
                            {
                                caseDetail.Add(new ModelCaseData() { Key = Convert.ToInt32(key.Trim('_')), Value = value});
                            }
                            break;
                    }
                }

                var requestType = _requestType.GetDetail(RequestTypeID, COMPANYID);


                AssignedTo = requestType.AssignedTo;


                ModelCaseSubmit existingCaseDetails = null;
                if (ID > 0)
                {
                    existingCaseDetails = _requestService.Get(ID, COMPANYID);
                }
                var caseId = _requestService.Save(ID, RequestTypeID, EntityID, PriorityID, StatusID, USERID, OwnerShipID, CC, DueDate, CandidateID, Notes, AssignedTo, CandidateName, OwnerShipName, CreatedByName, AssignedToName, caseDetail);

                //var notificationId = _requestService.SaveNotification(existingCaseDetails == null ? CaseNotificationType.Created : CaseNotificationType.Updated, caseId, USERID, AssignedTo, OwnerShipID, CC, DueDate, CandidateID, StatusID,
                //    _masterEntity.GetDetail(StatusID, COMPANYID).Value, PriorityID,
                //    _masterEntity.GetDetail(PriorityID, COMPANYID).Value);

                if (data.Files != null)
                {
                    var files = HttpContext.Request.Form.Files;
                    foreach (var fileo in files)
                    {
                        string file=fileo.Name;
                        var key = Convert.ToInt32(file.Substring(0, file.IndexOf("<FILE>")).TrimStart('_'));
                        var postedFile = fileo.GetBytes();
                        byte[] fileData = fileo.GetBytes().Result;
                        if (fileData.Length > 0)
                        {                            
                            _requestService.SaveDetail(caseId, key, fileo.FileName, USERID, fileData);
                        }
                    }
                }

                #region Save Logs

                if (existingCaseDetails != null)
                {
                    StringBuilder sbChangeLog = new StringBuilder();

                    #region compare existing values with new values 

                    if (existingCaseDetails.PriorityID != PriorityID)
                        sbChangeLog.Append(string.Format("'Priority' changed to '{0}'<br />", _masterEntity.GetDetail(PriorityID, COMPANYID).Value));

                    if (existingCaseDetails.StatusID != StatusID)
                        sbChangeLog.Append(string.Format("'Status' changed to '{0}'<br />", _masterEntity.GetDetail(StatusID, COMPANYID).Value));

                    if ((!existingCaseDetails.OwnerShipID.HasValue && OwnerShipID.HasValue) || (existingCaseDetails.OwnerShipID.HasValue && !OwnerShipID.HasValue) || (existingCaseDetails.OwnerShipID.HasValue && OwnerShipID.HasValue && existingCaseDetails.OwnerShipID.Value != OwnerShipID.Value))
                    {
                        var user = _requestService.GetOwnerList(COMPANYID, requestType.EntityID, OwnerShipID.Value.ToString()).FirstOrDefault();
                        sbChangeLog.Append(string.Format("'Owner' changed to '{0}'<br />", (user != null ? user.Text : "")));
                    }


                    if ((!existingCaseDetails.DueDate.HasValue && DueDate.HasValue) || (existingCaseDetails.DueDate.HasValue && !DueDate.HasValue) || (existingCaseDetails.DueDate.HasValue && DueDate.HasValue && existingCaseDetails.DueDate.Value != DueDate.Value))
                        sbChangeLog.Append(string.Format("'Due Date' changed to '{0}'<br />", (DueDate.HasValue ? DueDate.Value.ToShortDateString() : "-")));

                    if (GetSafeText(existingCaseDetails.CC) != GetSafeText(CC))
                        sbChangeLog.Append(string.Format("'CC' changed to '{0}'<br />", GetSafeText(CC)));

                    //if (GetSafeText(existingCaseDetails.AssignedTo) != GetSafeText(AssignedTo))
                    //{
                    //    sbChangeLog.Append(string.Format("'CC' changed to '{0}'<br />", _userList.GetAssignedToName(GetSafeText(AssignedTo))));
                    //}

                    foreach (var casedetail in caseDetail)
                    {
                        var existingdetail = existingCaseDetails.Data.FirstOrDefault(p => p.Key == casedetail.Key);
                        if (existingdetail != null && (!string.IsNullOrWhiteSpace(existingdetail.Value) && !string.IsNullOrWhiteSpace(casedetail.Value) && !existingdetail.Value.Equals(casedetail.Value)))
                        {
                            string value = casedetail.Value;
                            if (existingdetail.FieldType.Equals(ConstVariable.TYPE_DROPDOWN) || existingdetail.FieldType.Equals(ConstVariable.TYPE_DROPDOWN_MULTISELECT) || existingdetail.FieldType.Equals(ConstVariable.TYPE_RADIOLIST))
                            {
                                value = string.IsNullOrWhiteSpace(casedetail.Value) ? "" : _masterEntity.GetDetail(Convert.ToInt32(casedetail.Value), COMPANYID).Value;
                            }
                            sbChangeLog.Append(string.Format("'{0}' changed to '{1}'<br />", existingdetail.FieldName, value));
                        }
                    }

                    //if (Request.Files != null)
                    //{
                    //    foreach (string file in Request.Files)
                    //    {
                    //        var key = Convert.ToInt32(file.Substring(0, file.IndexOf("<FILE>")).TrimStart('_'));
                    //        var postedFile = Request.Files[file];
                    //        sbChangeLog.Append(string.Format("File '{0}' uploaded for '{1}'<br />", postedFile.FileName, _requestTypeControl.GetDetail(key).Label));
                    //    }
                    //}

                    #endregion

                    string log = sbChangeLog.ToString();
                    if (!string.IsNullOrEmpty(log))
                    {
                        log = string.Format("Case updated<br />{0}", log);
                        _requestService.SaveActivity(existingCaseDetails.RequestID, USERID, log, (short)ActivityType.EditCase, ClientIP);
                    }
                }
                else
                {
                    _requestService.SaveActivity(caseId, USERID, "Case created.", (short)ActivityType.CreatedCase, ClientIP);
                }

                #endregion
                if (caseId > 0)
                {

                    return Json(new { status = true, message = MessageHelper.Added });

                }
                return Json(new { status = false, message = MessageHelper.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Json(new { status = false, message = MessageHelper.Error });
            }
        }
        #endregion

        #region "List And Search"
        public IActionResult GetAll()
        {
            try
            {
                CacheFunctions.RemoveSession("Request");
                RequestSearchMetadata filters = new RequestSearchMetadata();
                filters.CompanyID = COMPANYID;

               
                CacheFunctions.SetComplexData("Request", filters);
                return RedirectToAction("GetAllRequest");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        [HttpPost]
        public IActionResult Search(RequestSearchMetadata filter)
        {
            try
            {
                filter.CompanyID = COMPANYID;
                
                CacheFunctions.SetComplexData("Request", filter);
                return RedirectToAction("GetAllRequest");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public async Task<IActionResult> GetAllRequest([FromQuery(Name = "grid-page")] string gridpage = "1", [FromQuery(Name = "grid-dir")] string griddir = "0")
        {
            try
            {
                RequestSearchMetadata filters = CacheFunctions.GetComplexData<RequestSearchMetadata>("Request");
                if (filters != null)
                {
                    filters.PageNo = gridpage.ToInt();
                    filters.PageSize = PAGESIZE;
                    PagedDataTable<ModelCaseListData> pds = await _requestService.GetAllRequest(filters);
                    
                    IQueryCollection query = Request.Query;
                    Action<IGridColumnCollection<ModelCaseListData>> columns = c =>
                    {
                        c.Add(o => o.RowNum, "RowNum")
                        .Titled("Sr. No")
                        .SortInitialDirection(GridSortDirection.Descending)
                        .SetWidth(210);
                        c.Add(o => o.EntityName)
                            .Titled("Entity Name")
                            .SetWidth(210);

                        c.Add(o => o.RequestTypeName)
                           .Titled("Request")
                           .SetWidth(110);
                        c.Add()
                            .Encoded(false)
                            .Sanitized(false)
                            .SetWidth(60)
                            .RenderValueAs(o => $"<a class='btn' href='Edit/{o.RequestID}' ><i class='bx bx-edit'></i></a>");

                    };
                    var server = new GridServer<ModelCaseListData>(pds, query, false, "Request",
                        columns, PAGESIZE, pds.TotalItemCount)
                        .Selectable(false)
                        .SetStriped(true)
                        .ChangePageSize(true)
                        .WithGridItemsCount("Total Records")
                        .EmptyText("No record found")
                        .ChangeSkip(false)
                       ;
                    //filters.GridSummary = server.Grid;
                    return View("GetAll", server.Grid);
                }
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public IActionResult Edit(int id)
        {
            try
            {
                ModelCaseSubmit model = null;
                if (id > 0)
                {                   

                    model = _requestService.Get(id,COMPANYID);
                    model.Forward = _requestService.GetRequestForward(model.RequestID).OrderByDescending(p => p.RequestForwardID).FirstOrDefault();
                    model.Reject = _requestService.GetRequestReject(model.RequestID).OrderByDescending(p => p.RequestRejectID).FirstOrDefault();
                    if (model.Forward != null && model.Reject != null)
                    {
                        if (model.Forward.CreatedOn > model.Reject.CreatedOn)
                        {
                            model.Reject = null;
                        }
                        else
                        {
                            model.Forward = null;
                        }
                    }
                    model.RequestType = _requestType.GetDetail(model.RequestTypeID, COMPANYID);
                    model.Notes = _requestService.GetNotes(model.RequestID);

                    //model.AssignedToList = (from item in _userList.GetAssignedToList(
                    //                                CompanyId,
                    //                                model.RequestType.EntityID,
                    //                                model.RequestType.AccessByRoles.Split(',').Select(p => Convert.ToInt32(p)).ToList())
                    //                        select new DropdownModel() { Value = item.Value, Text = item.Text }).ToList();

                    if (model.CandidateID.HasValue)
                    {
                        model.ReferenceData = _requestService.GetReferenceData(model.RequestType.EntityID, model.CandidateID.Value);
                        model.ReferenceID = model.CandidateID.Value;
                    }

                    //if (model.EntityID == CaseUserMaster)
                    //{
                    //    model.RequestType.AssignedTo = model.ReferenceData.ID.ToString();
                    //    model.RequestType.AssignedToName = model.ReferenceData.Name.ToString();
                    //    model.AssignedTo = model.ReferenceData.ID.ToString();
                    //    model.AssignedToList = new List<DropdownModel>() { new DropdownModel() { Value = model.ReferenceData.ID.ToString(), Text = model.ReferenceData.Name, IsSelected = true } };
                    //}
                }
                return View("Index",model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        [HttpGet]
        public FileResult DownloadFile(int id = 0)
        {
            try
            {
                var caseFile = _requestService.GetFile(id);
                return File(caseFile.File, System.Net.Mime.MediaTypeNames.Application.Octet, caseFile.FileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion
    }
}
