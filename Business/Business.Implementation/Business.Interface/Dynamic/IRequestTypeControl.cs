using Business.Entities.Dynamic;
using Business.SQL;
using System.Collections.Generic;

namespace Business.Interface.Dynamic
{
    public interface IRequestTypeControl
    {
        int Save(int RequestTypeControlID, int RequestTypeTitleID, string Label, string Type, bool IsRequired, string DefaultValue, string DataKey, bool IsActive, string AccessByRoles, short? ControlValidationRuleID, int CreatedBy, string Tooltip, short? MinLength, short? MaxLength, short? SortOrder);
        List<RequestTypeControlMetadata> GetList(int CompanyID);
        PagedDataTable<RequestTypeControlMetadata> GetList(int? requestTypeTitleID, int CompanyID, int pageNo = 1, int pageSize = 0, string orderBy = "RequestTypeTitle", string sortBy = "ASC", string searchString = "");
        List<RequestTypeControlMetadata> GetListByRequestType(int? requestTypeID);
        RequestTypeControlMetadata GetDetail(int id);
        bool Delete(int id);
        List<ControlValidationRuleMetadata> GetValidationRules();
        List<DropdownMetadata> GetControlTypes();
    }
}
