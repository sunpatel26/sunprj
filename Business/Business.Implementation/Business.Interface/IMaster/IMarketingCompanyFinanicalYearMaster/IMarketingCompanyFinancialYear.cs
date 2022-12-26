using Business.Entities.Master.MarketingCompanyFinancialYearMaster;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.IMaster.IMarketingCompanyFinanicalYearMaster
{
    public interface IMarketingCompanyFinancialYear
    {
        Task<PagedDataTable<FinancialYearMaster>> GetAllFinancialYearAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "FinancialYearID", string sortBy = "ASC");
        Task<FinancialYearMaster> GetFinancialYearAsync(int FinancialYearID);
        Task<int> InsertOrUpdateFinancialYearAsync(FinancialYearMaster financialYearMaster);


    }
}
