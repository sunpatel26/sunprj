using Business.Entities.Dynamic;
using Business.SQL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interface.Dynamic
{
    public interface IRequestType
    {
        Task<PagedDataTable<RequestTypeMetadata>> GetAllList(int compnayID, int page = 1, int pagesize = 20, string search = "", string orderby = "Name", string sortby = "ASC");

        int Save(int ID, int EntityID, string Name, string AccessByRoles, string AssignedTo, bool IsActive, int UserId, int CompanyID);
        List<RequestTypeMetadata> GetList(int CompanyID);
        RequestTypeMetadata GetDetail(int id, int CompanyID);
        bool Delete(int id);
        List<DropdownMetadata> GetDropdownList(int EntityID);
    }
}
