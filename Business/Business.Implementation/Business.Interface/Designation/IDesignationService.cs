using Business.Entities.Designation;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.Designation
{
    public interface IDesignationService
    {
        Task<PagedDataTable<DesignationMaster>> GetAllDesignationAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "DesignationText", string sortBy = "ASC");
        Task<DesignationMaster> GetDesignationAsync(int DesignationID);
        Task<int> DesignationCreateOrUpdateAsync(DesignationMaster designation);
    }
}
