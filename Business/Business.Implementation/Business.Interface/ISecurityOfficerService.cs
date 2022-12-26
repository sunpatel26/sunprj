using Business.Entities.SecurityOfficer;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface ISecurityOfficerService
    {
        Task<int> AddUpdateSecurityOfficer(SecurityOfficerMaster securityOfficerMaster);
        Task<PagedDataTable<SecurityOfficerMaster>> GetAllSecurityOfficerAsync(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "SecurityOfficerID", string sortBy = "ASC");
        Task<SecurityOfficerMaster> GetSecurityOfficerAsync(int securityOfficerID);
    }
}
