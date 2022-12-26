using Business.Entities.Dynamic;
using System.Collections.Generic;

namespace Business.Interface.Dynamic
{
    public interface IRequestTypeModule
    {
        int Save(int ID, int RequestTypeID, string Name, int SortOrder, bool IsActive, int UserId);
        List<RequestTypeTitleMetadata> GetList(int? requestTypeID, int CompanyID);
        RequestTypeTitleMetadata GetDetail(int id);
        bool Delete(int id);
    }
}
