using Business.Entities.Dynamic;
using Business.SQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils.Enums;

namespace Business.Interface.Dynamic
{
    public interface IRequestService
    {

        List<RequestTypeTitleMetadata> GetControls(int requestTypeId, int CompanyID);
        ModelCaseSubmit Get(int id, int companyID, bool loadDetails = false);
        int Save(int ID, int RequestTypeID, int EntityID, int PriorityID, int StatusID, int CreatedBy, int? OwnerShipID, string CC, DateTime? DueDate, int? CandidateID, string Note, string AssignedTo, string CandidateName, string OwnerShipName, string CreatedByName, string AssignedToName, List<ModelCaseData> Data);
        int SaveDetail(int RequestID, int Key, string Value, int CreatedBy, byte[] File);
        ModelCaseDataFile GetFile(int requestDetailFileID);

        List<DropdownMetadata> GetCandidateListForCaseFilter(int companyID, int entityID);
        List<DropdownMetadata> GetOwnerListForCaseFilter(int companyID);
        List<DropdownMetadata> GetClientListForCaseFilter(int companyID, int entityID);
        List<DropdownMetadata> GetTeamListForCaseFilter(int companyID, int entityID);        
        List<DropdownMetadata> GetAssignedToListForCaseFilter(int companyID);
        List<DropdownMetadata> GetCreatedByListForCaseFilter(int companyID);

        List<DropdownMetadata> GetOwnerList(int companyID, string userIds = "");
        List<DropdownMetadata> GetOwnerList(int companyID, int entityID, string userIds = "");

        List<ModelRequestNote> GetNotes(int requestID);
        List<ModelCaseListData> GetCaseList(int CompanyID, int? ID, string EntityIDs = null, string RequestTypeIDs = null, string PriorityIDs = null, string StatusIDs = null, string CreatedByName = null, string OwnerShipName = null, string AssignedToName = null, string CandidateName = null, DateTime? DueDateFrom = null, DateTime? DueDateTo = null, DateTime? CreatedDateFrom = null, DateTime? CreatedDateTo = null, int PageStart = 0, int PageSize = 10, int SortColumn = 1, string SortOrder = "ASC");

        int SaveActivity(int RequestID, int CreatedBy, string Description, short Type, string IP);
        int AcceptCase(int RequestID, int AcceptedBy);

        List<ModelReportByCaseType> GetReportByCaseType(int companyID, int userId);

        List<ModelCaseListData> GetReportDetailByCaseType(int RequestTypeID, string ReportType, int UserId, int PageStart = 0, int PageSize = 10, int SortColumn = 1, string SortOrder = "ASC");

        int SaveRequestForward(int RequestID, int CreatedBy, string Reason, string ReasonOther, string To, string ToText, string CC);
        int SaveRequestReject(int RequestID, int CreatedBy, string Reason, string ReasonOther);
        List<ModelRequestForward> GetRequestForward(int RequestID);
        List<ModelRequestReject> GetRequestReject(int RequestID);

        CaseReferenceDataModel GetReferenceData(int EntityID, int DataID);
        
        int SaveNotification(CaseNotificationType Type, int RequestID, int CreatedByID, string AssignedTo = null, int? OwnerID = null, string CC = null, DateTime? DueDate = null, int? CandidateID = null, int? SatusID = null, string Status = null, int? PriorityID = null, string Priority = null, int? AccceptedBy = null, int? RejectedBy = null, string RejectedReason = null, int? ForwardedBy = null, string ForwardedReason = null, string ForwardedTo = null, string ForwardedCC = null);

        List<ModelCaseListData> GetCandidateCaseList(int CompanyID, int? ID, string EntityIDs = null, string RequestTypeIDs = null, string PriorityIDs = null, string StatusIDs = null, string CreatedByName = null, string OwnerShipName = null, string AssignedToName = null, string CandidateIDs = null, string CandidateName = null, DateTime? DueDateFrom = null, DateTime? DueDateTo = null, DateTime? CreatedDateFrom = null, DateTime? CreatedDateTo = null, int PageStart = 0, int PageSize = 10, int SortColumn = 1, string SortOrder = "ASC");
        List<ModelCaseListData> GetClientCaseList(int CompanyID, int? ID, string EntityIDs = null, string RequestTypeIDs = null, string PriorityIDs = null, string StatusIDs = null, string CreatedByName = null, string OwnerShipName = null, string AssignedToName = null, string CandidateIDs = null, string CandidateName = null, DateTime? DueDateFrom = null, DateTime? DueDateTo = null, DateTime? CreatedDateFrom = null, DateTime? CreatedDateTo = null, int PageStart = 0, int PageSize = 10, int SortColumn = 1, string SortOrder = "ASC");
        List<ModelCaseListData> GetTeamCaseList(int CompanyID, int? ID, string EntityIDs = null, string RequestTypeIDs = null, string PriorityIDs = null, string StatusIDs = null, string CreatedByName = null, string OwnerShipName = null, string AssignedToName = null, string CandidateIDs = null, string CandidateName = null, DateTime? DueDateFrom = null, DateTime? DueDateTo = null, DateTime? CreatedDateFrom = null, DateTime? CreatedDateTo = null, int PageStart = 0, int PageSize = 10, int SortColumn = 1, string SortOrder = "ASC");


       Task<PagedDataTable<ModelCaseListData>> GetAllRequest(RequestSearchMetadata filters);

    }
}
