using Business.Entities.Dynamic;
using System.Collections.Generic;

namespace Business.Interface.Dynamic
{
    public interface IRequestTypeControlScreenMapping
    {
        int Save(int RequestTypeControlScreenID, int RequestTypeID, int RequestTypeControlID, int? RoleID, string ScreenName, string RenderType, int CreatedBy);
        List<RequestTypeControlScreenMappingMetadata> GetList(int CompanyID);
        List<RequestTypeControlScreenMappingMetadata> GetList(string screenName);
        RequestTypeControlScreenMappingMetadata GetDetail(int id);
        bool Delete(int id);
        List<DropdownMetadata> GetRequestTypes(int CompanyID);
        List<DropdownMetadata> GetControlsByRequestType(int id);
        List<DropdownMetadata> GetScreenNameTypes();
        List<DropdownMetadata> GetControlRenderTypes();
    }
}
