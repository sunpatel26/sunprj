using Business.Entities.Designation;
using System.Threading.Tasks;

namespace Business.Interface.Designation
{
    public interface IDesignationGroupService
    {
        Task<int> CreateDesignationtGroupAsync(DesignationGroupMaster designationGroupMaster);
    }
}
