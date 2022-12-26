using Business.Entities;
using Business.Entities.Dynamic;
using Business.Interface.Dynamic;
using Business.SQL;
using Dapper;
using MailKit.Search;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Utils.Enums;

namespace Business.Service.Dynamic
{
    public class RequestService : IRequestService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public RequestService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public List<RequestTypeTitleMetadata> GetControls(int requestTypeId, int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RequestTypeID", requestTypeId);
                var requestTypeTitles = QueryHelper.GetList<RequestTypeTitleMetadata>(connection, "GetRequestTypeTitleByRequestType", param);
                foreach (var item in requestTypeTitles)
                {
                    param = new DynamicParameters();
                    param.Add("@TitleID", item.RequestTypeTitleID);
                    item.Controls = QueryHelper.GetList<RequestTypeControlMetadata>(connection, "GetRequestTypeControlsBySection", param);
                    var dropdownKeys = item.Controls.Where(p => p.Type.Equals(ConstVariable.TYPE_RADIOLIST) || p.Type.Equals(ConstVariable.TYPE_DROPDOWN) || p.Type.Equals(ConstVariable.TYPE_DROPDOWN_MULTISELECT)).Select(p => p.DataKey).ToList();

                    param = new DynamicParameters();
                    param.Add("@Keys", string.Join(",", dropdownKeys));
                    param.Add("@CompanyID", CompanyID);
                    item.DropdownValueList = QueryHelper.GetList<MasterEntityMetadata>(connection, "GetMasterListByKeys", param);
                }

                return requestTypeTitles;
            }
            catch
            {
                throw;
            }
        }

        public ModelCaseSubmit Get(int id, int companyID = 0, bool loadDetails = false)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                var modelCase = QueryHelper.GetTableDetail<ModelCaseSubmit>(connection, "GetCase", param);
                if (modelCase == null)
                {
                    throw new Exception("Invalid Case ID");
                }
                param = new DynamicParameters();
                param.Add("@RequestID", id);
                modelCase.Data = QueryHelper.GetList<ModelCaseData>(connection, "GetCaseDetail", param);
                foreach (var data in modelCase.Data)
                {
                    param = new DynamicParameters();
                    param.Add("@RequestDetailID", data.RequestDetailID);
                    data.Files = QueryHelper.GetList<ModelCaseDataFile>(connection, "GetCaseDetailFiles", param);
                }

                param = new DynamicParameters();
                param.Add("@RequestID", id);
                param.Add("@CompanyID", companyID);
                modelCase.Activities = QueryHelper.GetList<ModelCaseActivity>(connection, "GetRequestActivities", param);

                return modelCase;
            }
            catch
            {
                throw;
            }
        }

        public int Save(int ID, int RequestTypeID, int EntityID, int PriorityID, int StatusID, int CreatedBy, int? OwnerShipID, string CC, DateTime? DueDate, int? CandidateID, string Note, string AssignedTo, string CandidateName, string OwnerShipName, string CreatedByName, string AssignedToName, List<ModelCaseData> Data)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                param.Add("@RequestTypeID", RequestTypeID);
                param.Add("@EntityID", EntityID);
                param.Add("@PriorityID", PriorityID);
                param.Add("@StatusID", StatusID);
                param.Add("@CreatedBy", CreatedBy);
                param.Add("@OwnerShipID", OwnerShipID);
                param.Add("@CC", CC);
                param.Add("@DueDate", DueDate);
                param.Add("@CandidateID", CandidateID);
                param.Add("@Note", Note);
                param.Add("@AssignedTo", AssignedTo);
                param.Add("@CandidateName", CandidateName);

                param.Add("@CreatedByName", CreatedByName);
                param.Add("@OwnerShipName", OwnerShipName);
                param.Add("@AssignedToName", AssignedToName);

                int id = QueryHelper.Save(connection, "SaveCase", param);
                foreach (var detail in Data)
                {
                    SaveDetail(id, detail.Key, detail.Value, CreatedBy);
                }
                return id;
            }
            catch
            {
                throw;
            }
        }

        public int SaveDetail(int RequestID, int Key, string Value, int CreatedBy, byte[] File = null)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RequestID", RequestID);
                param.Add("@Key", Key);
                param.Add("@Value", Value);
                param.Add("@CreatedBy", CreatedBy);
                param.Add("@File", File, System.Data.DbType.Binary);
                var id = QueryHelper.Save(connection, "SaveCaseDetail", param);

                if (File != null)
                {

                }
                return id;
            }
            catch
            {
                throw;
            }
        }
        
        public ModelCaseDataFile GetFile(int requestDetailFileID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RequestDetailFileID", requestDetailFileID);
                return QueryHelper.GetTableDetail<ModelCaseDataFile>(connection, "GetCaseDetailFile", param);
            }
            catch
            {
                throw;
            }
        }


        public List<DropdownMetadata> GetCandidateListForCaseFilter(int companyID, int entityID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", companyID);
                param.Add("@EntityID", entityID);
                var result = QueryHelper.GetList<DropdownMetadata>(connection, "GetCandidateList", param);
                return result;
            }
            catch
            {
                throw;
            }
        }
        public List<DropdownMetadata> GetOwnerListForCaseFilter(int companyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", companyID);
                var result = QueryHelper.GetList<DropdownMetadata>(connection, "GetOwnersList", param);
                return result;
            }
            catch
            {
                throw;
            }
        }
        public List<DropdownMetadata> GetClientListForCaseFilter(int companyID, int entityID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", companyID);
                param.Add("@EntityID", entityID);
                var result = QueryHelper.GetList<DropdownMetadata>(connection, "GetClientList", param);
                return result;
            }
            catch
            {
                throw;
            }
        }
        public List<DropdownMetadata> GetTeamListForCaseFilter(int companyID, int entityID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", companyID);
                param.Add("@EntityID", entityID);
                var result = QueryHelper.GetList<DropdownMetadata>(connection, "GetAllTeamList", param);
                return result;
            }
            catch
            {
                throw;
            }
        }
        public List<DropdownMetadata> GetAssignedToListForCaseFilter(int companyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", companyID);
                var result = QueryHelper.GetList<DropdownMetadata>(connection, "GetAssignedToListForCase", param);
                return result;
            }
            catch
            {
                throw;
            }
        }
        public List<DropdownMetadata> GetCreatedByListForCaseFilter(int companyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", companyID);
                var result = QueryHelper.GetList<DropdownMetadata>(connection, "GetCreatedByNameListForCase", param);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public List<DropdownMetadata> GetOwnerList(int companyID, int entityID, string userIds = "")
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", companyID);
                param.Add("@EntityID", entityID);
                param.Add("@Users", userIds);
                var result = QueryHelper.GetList<DropdownMetadata>(connection, "GetAssignedToList", param);
                return result;
            }
            catch
            {
                throw;
            }
        }
        public List<DropdownMetadata> GetOwnerList(int companyID, string userIds = "")
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", companyID);
                param.Add("@Users", userIds);
                var result = QueryHelper.GetList<DropdownMetadata>(connection, "GetCompanyUserList", param);
                return result;
            }
            catch
            {
                throw;
            }
        }
       

        public List<ModelRequestNote> GetNotes(int requestID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RequestID", requestID);
                return QueryHelper.GetList<ModelRequestNote>(connection, "GetRequestNotes", param);
            }
            catch
            {
                throw;
            }
        }

        public List<ModelCaseListData> GetCaseList(int CompanyID, int? ID, string EntityIDs = null, string RequestTypeIDs = null, string PriorityIDs = null, string StatusIDs = null, string CreatedByName = null, string OwnerShipName = null, string AssignedToName = null, string CandidateName = null, DateTime? DueDateFrom = null, DateTime? DueDateTo = null, DateTime? CreatedDateFrom = null, DateTime? CreatedDateTo = null, int PageStart = 0, int PageSize = 10, int SortColumn = 1, string SortOrder = "ASC")
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                param.Add("@CreatedByID", null);
                param.Add("@AssignedToID", null);
                param.Add("@EntityIDs", EntityIDs);
                param.Add("@RequestTypeIDs", RequestTypeIDs);
                param.Add("@PriorityIDs", PriorityIDs);
                param.Add("@StatusIDs", StatusIDs);
                param.Add("@OwnerIDs", null);
                param.Add("@CandidateIDs", null);
                param.Add("@CandidateName", CandidateName.Replace("--Select--", ""));
                param.Add("@CreatedByName", CreatedByName.Replace("--Select--", ""));
                param.Add("@OwnerShipName", OwnerShipName.Replace("--Select--", ""));
                param.Add("@AssignedToName", AssignedToName.Replace("--Select--", ""));
                param.Add("@DueDateFrom", DueDateFrom);
                param.Add("@DueDateTo", DueDateTo);
                param.Add("@CreatedDateFrom", CreatedDateFrom);
                param.Add("@CreatedDateTo", CreatedDateTo);
                param.Add("@PageStart", PageStart);
                param.Add("@PageSize", PageSize);
                param.Add("@SortColumn", SortColumn);
                param.Add("@SortOrder", SortOrder);
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.GetList<ModelCaseListData>(connection, "GetMyCaseList", param);
            }
            catch
            {
                throw;
            }
        }

        public int SaveActivity(int RequestID, int CreatedBy, string Description, short Type, string IP)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RequestID", RequestID);
                param.Add("@CreatedBy", CreatedBy);
                param.Add("@Description", Description);
                param.Add("@Type", Type);
                param.Add("@IP", IP);
                return QueryHelper.Save(connection, "SaveRequestActivity", param);
            }
            catch
            {
                throw;
            }
        }

        public int AcceptCase(int RequestID, int AcceptedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RequestID", RequestID);
                param.Add("@AcceptedBy", AcceptedBy);
                return QueryHelper.Save(connection, "SaveCaseAccepted", param);
            }
            catch
            {
                throw;
            }
        }

        public List<ModelReportByCaseType> GetReportByCaseType(int companyID, int userId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", companyID);
                param.Add("@UserID", userId);
                return QueryHelper.GetList<ModelReportByCaseType>(connection, "GetReportByRequestType", param);
            }
            catch
            {
                throw;
            }
        }
        public List<ModelCaseListData> GetReportDetailByCaseType(int RequestTypeID, string ReportType, int UserId, int PageStart = 0, int PageSize = 10, int SortColumn = 1, string SortOrder = "ASC")
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RequestTypeID", RequestTypeID);
                param.Add("@FilterName", ReportType);
                param.Add("@UserID", UserId);
                param.Add("@PageStart", PageStart);
                param.Add("@PageSize", PageSize);
                param.Add("@SortColumn", SortColumn);
                param.Add("@SortOrder", SortOrder);
                return QueryHelper.GetList<ModelCaseListData>(connection, "GetReportDetail", param);
            }
            catch
            {
                throw;
            }
        }
       
        public int SaveRequestForward(int RequestID, int CreatedBy, string Reason, string ReasonOther, string To, string ToText, string CC)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RequestID", RequestID);
                param.Add("@CreatedBy", CreatedBy);
                param.Add("@Reason", Reason);
                param.Add("@Other", ReasonOther);
                param.Add("@To", To);
                param.Add("@ToText", ToText);
                param.Add("@CC", CC);
                return QueryHelper.Save(connection, "SaveCaseForward", param);
            }
            catch
            {
                throw;
            }
        }
        public int SaveRequestReject(int RequestID, int CreatedBy, string Reason, string ReasonOther)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RequestID", RequestID);
                param.Add("@CreatedBy", CreatedBy);
                param.Add("@Reason", Reason);
                param.Add("@Other", ReasonOther);
                return QueryHelper.Save(connection, "SaveCaseReject", param);
            }
            catch
            {
                throw;
            }
        }

        public List<ModelRequestForward> GetRequestForward(int RequestID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RequestID", RequestID);
                return QueryHelper.GetList<ModelRequestForward>(connection, "GetRequestForward", param);
            }
            catch
            {
                throw;
            }
        }
        public List<ModelRequestReject> GetRequestReject(int RequestID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RequestID", RequestID);
                return QueryHelper.GetList<ModelRequestReject>(connection, "GetRequestReject", param);
            }
            catch
            {
                throw;
            }
        }               
        public CaseReferenceDataModel GetReferenceData(int EntityID, int DataID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@EntityID", EntityID);
                param.Add("@DataID", DataID);
                return QueryHelper.GetTableDetail<CaseReferenceDataModel>(connection, "GetCandidateDetail", param);
            }
            catch
            {
                throw;
            }
        }
        public int SaveNotification(CaseNotificationType Type, int RequestID, int CreatedByID, string AssignedTo = null, int? OwnerID = null, string CC = null, DateTime? DueDate = null, int? CandidateID = null, int? SatusID = null, string Status = null, int? PriorityID = null, string Priority = null, int? AccceptedBy = null, int? RejectedBy = null, string RejectedReason = null, int? ForwardedBy = null, string ForwardedReason = null, string ForwardedTo = null, string ForwardedCC = null)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", Type.ToString());
                param.Add("@RequestID", RequestID);
                param.Add("@CreatedByID", CreatedByID);
                param.Add("@AssignedTo", AssignedTo);
                param.Add("@OwnerID", OwnerID);
                param.Add("@CC", CC);
                param.Add("@DueDate", DueDate);
                param.Add("@CandidateID", CandidateID);
                param.Add("@SatusID", SatusID);
                param.Add("@Status", Status);
                param.Add("@PriorityID", PriorityID);
                param.Add("@Priority", Priority);
                param.Add("@AccceptedBy", AccceptedBy);
                param.Add("@RejectedBy", RejectedBy);
                param.Add("@ForwardedBy", ForwardedBy);

                param.Add("@ForwardedReason", ForwardedReason);
                param.Add("@ForwardedTo", ForwardedTo);
                param.Add("@ForwardedCC", ForwardedCC);
                param.Add("@RejectedReason", RejectedReason);

                int id = QueryHelper.Save(connection, "SaveRequestNotification", param);
                return id;
            }
            catch
            {
                throw;
            }
        }

        public List<ModelCaseListData> GetCandidateCaseList(int CompanyID, int? ID, string EntityIDs = null, string RequestTypeIDs = null, string PriorityIDs = null, string StatusIDs = null, string CreatedByName = null, string OwnerShipName = null, string AssignedToName = null, string CandidateIDs = null, string CandidateName = null, DateTime? DueDateFrom = null, DateTime? DueDateTo = null, DateTime? CreatedDateFrom = null, DateTime? CreatedDateTo = null, int PageStart = 0, int PageSize = 10, int SortColumn = 1, string SortOrder = "ASC")
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                param.Add("@CreatedByID", null);
                param.Add("@AssignedToID", null);
                param.Add("@EntityIDs", EntityIDs);
                param.Add("@RequestTypeIDs", RequestTypeIDs);
                param.Add("@PriorityIDs", PriorityIDs);
                param.Add("@StatusIDs", StatusIDs);
                param.Add("@OwnerIDs", null);
                param.Add("@CandidateIDs", CandidateIDs);
                param.Add("@CandidateName", CandidateName.Replace("--Select--", ""));
                param.Add("@CreatedByName", CreatedByName.Replace("--Select--", ""));
                param.Add("@OwnerShipName", OwnerShipName.Replace("--Select--", ""));
                param.Add("@AssignedToName", AssignedToName.Replace("--Select--", ""));
                param.Add("@DueDateFrom", DueDateFrom);
                param.Add("@DueDateTo", DueDateTo);
                param.Add("@CreatedDateFrom", CreatedDateFrom);
                param.Add("@CreatedDateTo", CreatedDateTo);
                param.Add("@PageStart", PageStart);
                param.Add("@PageSize", PageSize);
                param.Add("@SortColumn", SortColumn);
                param.Add("@SortOrder", SortOrder);
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.GetList<ModelCaseListData>(connection, "GetMyCaseList", param);
            }
            catch
            {
                throw;
            }
        }

        public List<ModelCaseListData> GetClientCaseList(int CompanyID, int? ID, string EntityIDs = null, string RequestTypeIDs = null, string PriorityIDs = null, string StatusIDs = null, string CreatedByName = null, string OwnerShipName = null, string AssignedToName = null, string CandidateIDs = null, string CandidateName = null, DateTime? DueDateFrom = null, DateTime? DueDateTo = null, DateTime? CreatedDateFrom = null, DateTime? CreatedDateTo = null, int PageStart = 0, int PageSize = 10, int SortColumn = 1, string SortOrder = "ASC")
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                param.Add("@CreatedByID", null);
                param.Add("@AssignedToID", null);
                param.Add("@EntityIDs", EntityIDs);
                param.Add("@RequestTypeIDs", RequestTypeIDs);
                param.Add("@PriorityIDs", PriorityIDs);
                param.Add("@StatusIDs", StatusIDs);
                param.Add("@OwnerIDs", null);
                param.Add("@CandidateIDs", CandidateIDs);
                param.Add("@CandidateName", CandidateName.Replace("--Select--", ""));
                param.Add("@CreatedByName", CreatedByName.Replace("--Select--", ""));
                param.Add("@OwnerShipName", OwnerShipName.Replace("--Select--", ""));
                param.Add("@AssignedToName", AssignedToName.Replace("--Select--", ""));
                param.Add("@DueDateFrom", DueDateFrom);
                param.Add("@DueDateTo", DueDateTo);
                param.Add("@CreatedDateFrom", CreatedDateFrom);
                param.Add("@CreatedDateTo", CreatedDateTo);
                param.Add("@PageStart", PageStart);
                param.Add("@PageSize", PageSize);
                param.Add("@SortColumn", SortColumn);
                param.Add("@SortOrder", SortOrder);
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.GetList<ModelCaseListData>(connection, "GetMyCaseList", param);
            }
            catch
            {
                throw;
            }
        }


        public List<ModelCaseListData> GetTeamCaseList(int CompanyID, int? ID, string EntityIDs = null, string RequestTypeIDs = null, string PriorityIDs = null, string StatusIDs = null, string CreatedByName = null, string OwnerShipName = null, string AssignedToName = null, string CandidateIDs = null, string CandidateName = null, DateTime? DueDateFrom = null, DateTime? DueDateTo = null, DateTime? CreatedDateFrom = null, DateTime? CreatedDateTo = null, int PageStart = 0, int PageSize = 10, int SortColumn = 1, string SortOrder = "ASC")
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                param.Add("@CreatedByID", null);
                param.Add("@AssignedToID", null);
                param.Add("@EntityIDs", EntityIDs);
                param.Add("@RequestTypeIDs", RequestTypeIDs);
                param.Add("@PriorityIDs", PriorityIDs);
                param.Add("@StatusIDs", StatusIDs);
                param.Add("@OwnerIDs", null);
                param.Add("@CandidateIDs", CandidateIDs);
                param.Add("@CandidateName", CandidateName.Replace("--Select--", ""));
                param.Add("@CreatedByName", CreatedByName.Replace("--Select--", ""));
                param.Add("@OwnerShipName", OwnerShipName.Replace("--Select--", ""));
                param.Add("@AssignedToName", AssignedToName.Replace("--Select--", ""));
                param.Add("@DueDateFrom", DueDateFrom);
                param.Add("@DueDateTo", DueDateTo);
                param.Add("@CreatedDateFrom", CreatedDateFrom);
                param.Add("@CreatedDateTo", CreatedDateTo);
                param.Add("@PageStart", PageStart);
                param.Add("@PageSize", PageSize);
                param.Add("@SortColumn", SortColumn);
                param.Add("@SortOrder", SortOrder);
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.GetList<ModelCaseListData>(connection, "GetMyCaseList", param);
            }
            catch
            {
                throw;
            }
        }

        public async Task<PagedDataTable<ModelCaseListData>> GetAllRequest(RequestSearchMetadata filters)
        {
            
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<ModelCaseListData> lst = null;
            try
            {
                SqlParameter[] param = {
                     new SqlParameter("@ID", filters.ID)
                    ,new SqlParameter("@CreatedByID", null)
                    ,new SqlParameter("@AssignedToID", null)
                    ,new SqlParameter("@EntityIDs", filters.EntityID)
                    ,new SqlParameter("@RequestTypeIDs", filters.RequestTypeID)
                    ,new SqlParameter("@PriorityIDs", filters.PriorityIDs)
                    ,new SqlParameter("@StatusIDs", filters.StatusIDs)
                    ,new SqlParameter("@OwnerIDs", null)
                    ,new SqlParameter("@CandidateIDs", null)
                    ,new SqlParameter("@CandidateName", filters.CandidateName.IsNotStringNullOrEmpty()?  filters.CandidateName.Replace("--Select--", ""):null)
                    ,new SqlParameter("@CreatedByName",  filters.CreatedByName.IsNotStringNullOrEmpty()?filters.CreatedByName.Replace("--Select--", ""):null)
                    ,new SqlParameter("@OwnerShipName",  filters.OwnerShipName.IsNotStringNullOrEmpty()? filters.OwnerShipName.Replace("--Select--", ""):null)
                    ,new SqlParameter("@AssignedToName",  filters.AssignedToName.IsNotStringNullOrEmpty()?filters.AssignedToName.Replace("--Select--", ""):null)
                    ,new SqlParameter("@DueDateFrom", filters.DueDateFrom)
                    ,new SqlParameter("@DueDateTo", filters.DueDateTo)
                    ,new SqlParameter("@CreatedDateFrom", filters.CreatedDateFrom)
                    ,new SqlParameter("@CreatedDateTo", filters.CreatedDateTo)
                    ,new SqlParameter("@PageStart", filters.PageNo)
                    ,new SqlParameter("@PageSize", filters.PageSize)
                    ,new SqlParameter("@SortColumn", filters.OrderBy)
                    ,new SqlParameter("@SortOrder", filters.SortOrder)
                    ,new SqlParameter("@CompanyID", filters.CompanyID)

            };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "GetMyCaseList", param))
                {
                    if (ds.Tables.Count > 0)
                    {
                        table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            if (table.ContainColumn("TotalCount"))
                                totalItemCount = Convert.ToInt32(table.Rows[0]["TotalCount"]);
                            else
                                totalItemCount = table.Rows.Count;
                        }
                    }
                    lst = table.ToPagedDataTableList<ModelCaseListData>(filters.PageNo, filters.PageSize, totalItemCount);
                    return lst;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }

    }
}
