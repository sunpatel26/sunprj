using Business.Entities.Marketing.CompanySale;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.Marketing.ICompanyTarget

{
    public interface IMarketingCompanyTargetService
    {
        Task<PagedDataTable<CompanyTarget>> GetAllMarketingCompanyTargetAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "CompanyTargetID", string sortBy = "ASC");
        Task<int> InsertOrUpdateMarketingCompanyTargetAsync(CompanyTarget companyTarget);
        Task<CompanyTarget> GetMarketingCompanyTargetAsync(int MarketingCompanyTargetID);
    }
}
