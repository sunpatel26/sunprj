using Business.Entities.Department;
using System.Threading.Tasks;

namespace Business.Interface.Department
{
    public interface IDepartmentGroupService
    {
        Task<int> CreateDepartmentGroupAsync(DepartmentGroupMaster departmentGroupMaster);
    }
}
