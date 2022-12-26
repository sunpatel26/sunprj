using Business.Entities.Marketing.EmployeeSalesTargetSummary;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.Marketing.IEmployeeSalesTargetSummary
{
    public interface IEmployeeSalesTargetSummaryService
    {
        Task<PagedDataTable<EmployeeSalesTargetSummary>> GetAllMarketingEmployeeSalesTargetSummaryAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "SalesTargetID", string sortBy = "ASC");
    }
}
