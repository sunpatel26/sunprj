using Business.SQL;
using System.Threading.Tasks;
using Business.Entities.Department;

namespace Business.Interface.Department
{
    public interface IDepartmentService
    {
        Task<PagedDataTable<DepartmentMaster>> GetAllDepartmentAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "DepartmentID", string sortBy = "ASC");

        Task<int> DepartmemtCreateOrUpdateAsync(DepartmentMaster department);

        Task<DepartmentMaster> GetDepartmentAsync(int DepartmentID);
    }
}
