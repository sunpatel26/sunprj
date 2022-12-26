using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;

namespace Business.Entities.Dynamic
{
    public class RequestSearchMetadata
    {
        public long draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public int CandidateID { get; set; }
        public int ClientID { get; set; }
        public int MemberID { get; set; }
        public int EntityID { get; set; }
        public int RequestTypeID { get; set; }
        public int CompanyID { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string OrderBy { get; set; }
        public string SortBy { get; set; }


        public string ID { get; set; }
        public string PriorityIDs { get; set; }
        public string StatusIDs { get; set; }
        public string CandidateName { get; set; }
        public string CreatedByName { get; set; }
        public string OwnerShipName { get; set; }
        public string AssignedToName { get; set; }
        public string DueDateFrom { get; set; }
        public string DueDateTo { get; set; }
        public string CreatedDateFrom { get; set; }
        public string CreatedDateTo { get; set; }
        public string SortOrder { get; set; }
        

    }
    public class ModelCaseSubmit
    {
        public int RequestID { get; set; }
        public int RequestTypeID { get; set; }
        public int EntityID { get; set; }
        public int PriorityID { get; set; }
        public int StatusID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public int? OwnerShipID { get; set; }
        public string CC { get; set; }
        public DateTime? DueDate { get; set; }
        public int? CandidateID { get; set; }
        public string CandidateName { get; set; }
        public string AssignedTo { get; set; }

        public int? AcceptedBy { get; set; }

        public DateTime? AcceptedOn { get; set; }
        public List<ModelCaseData> Data { get; set; }

        public List<DropdownMetadata> ForwardReason { get; set; }

        public List<DropdownMetadata> RejectReason { get; set; }
        public List<DropdownMetadata> Entities { get; set; }
        public List<DropdownMetadata> Statuses { get; set; }
        public List<DropdownMetadata> Priorities { get; set; }
        public List<DropdownMetadata> Owners { get; set; }
        public List<DropdownMetadata> Candidates { get; set; }

        public List<DropdownMetadata> AssignedToList { get; set; }
        public List<RequestTypeTitleMetadata> Sections { get; set; }
        public List<DropdownMetadata> ValidationRules { get; set; }
        public List<RequestTypeControlScreenMappingMetadata> ControlScreenMapping { get; set; }
        public RequestTypeMetadata RequestType { get; set; }
        public string RequestTypeName { get; set; }
        public string EntityName { get; set; }

        public List<ModelRequestNote> Notes { get; set; }

        public List<ModelCaseActivity> Activities { get; set; }

        public ModelRequestForward Forward { get; set; }
        public ModelRequestReject Reject { get; set; }
        public CaseReferenceDataModel ReferenceData { get; set; }
        public int ReferenceID { get; set; }

    }

    public class ModelCaseData
    {
        public int RequestDetailID { get; set; }
        public int RequestID { get; set; }
        public int Key { get; set; }
        public string Value { get; set; }
        public List<ModelCaseDataFile> Files { get; set; }

        public string FieldName { get; set; }
        public string FieldType { get; set; }
    }

    public class ModelCaseDataFile
    {
        public int RequestDetailFileID { get; set; }
        public int RequestDetailID { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }

        public DateTime UploadDate { get; set; }
        public int UploadedBy { get; set; }
    }

    public class ModelMyRequestList
    {
        public long draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }

        public List<ModelMyRequest> data { get; set; }
    }

    public class ModelMyRequest
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comments { get; set; }
    }

    public class ModelRequestNote
    {
        public int ID { get; set; }
        public int RequestID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Note { get; set; }
        public string CreatedByID { get; set; }
        public string CreatedByName { get; set; }
    }

    public class ModelCaseList
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public List<DropdownMetadata> Entities { get; set; }
        public List<DropdownMetadata> Statuses { get; set; }
        public List<DropdownMetadata> Priorities { get; set; }
        public List<DropdownMetadata> AssignedToList { get; set; }
        public List<DropdownMetadata> CreatedByList { get; set; }
        public List<DropdownMetadata> Candidates { get; set; }
        public List<DropdownMetadata> Clients { get; set; }
        public List<DropdownMetadata> Members { get; set; }
        public List<DropdownMetadata> OwnersList { get; set; }
        public List<DropdownMetadata> RequestTypes { get; set; }

        public List<ModelCaseListData> data = new List<ModelCaseListData>();

        public long draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public int CandidateID { get; set; }
        public int ClientID { get; set; }
        public int MemberID { get; set; }
        public int EntityID { get; set; }

    }

    public class ModelCaseListData
    {
        public int RequestID { get; set; }
        public int RequestTypeID { get; set; }
        public int EntityID { get; set; }
        public int PriorityID { get; set; }
        public int StatusID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? OwnerShipID { get; set; }

        public string CC { get; set; }
        public DateTime? DueDate { get; set; }

        public int? CandidateID { get; set; }

        public string EntityName { get; set; }
        public string RequestTypeName { get; set; }
        public string PriorityName { get; set; }
        public string StatusName { get; set; }
        public string CreatedByName { get; set; }
        public string UpdateByName { get; set; }
        public string OwnerShipName { get; set; }
        public string CandidateName { get; set; }
        public string AssignedToName { get; set; }

        public int RowNum { get; set; }
        public int TotalCount { get; set; }
    }

    public class ModelCaseActivity
    {
        public int RequestActivityID { get; set; }
        public int RequestID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
        public short Type { get; set; }
        public string IP { get; set; }
        public string CreatedByName { get; set; }

        public string TypeName
        {
            get
            {
                MemberInfo memberInfo = typeof(ActivityType).GetMember(((ActivityType)Type).ToString()).First();
                var descriptionAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();
                if (descriptionAttribute != null)
                {
                    return descriptionAttribute.Description;
                }
                else
                {
                    return Type.ToString();
                }
            }
        }
    }



    public class ModelReportByCaseType
    {
        public int RequestTypeID { get; set; }
        public string Name { get; set; }
        public int TotalOpenCases { get; set; }
        public int TotalCloseCases { get; set; }
        public int TotalCases { get; set; }
        public int TotalPastDueCases { get; set; }
        public int TotalAssignedToMe { get; set; }
        public int TotalPastDueCasesAssignedToMe { get; set; }
        public int TotalCreatedByMe { get; set; }
        public int TotalMyOwnership { get; set; }
        public int TotalClosedByMe { get; set; }
    }

    public class ModelReportByCaseTypeList
    {
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        public List<ModelReportByCaseType> Data { get; set; }
    }

    public class ModelRequestForward
    {
        public int RequestForwardID { get; set; }
        public int RequestID { get; set; }
        public string Reason { get; set; }
        public string ReasonOther { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class ModelRequestReject
    {
        public int RequestRejectID { get; set; }
        public int RequestID { get; set; }
        public string Reason { get; set; }
        public string ReasonOther { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class CaseReferenceDataModel
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Label { get; set; }
    }


}
