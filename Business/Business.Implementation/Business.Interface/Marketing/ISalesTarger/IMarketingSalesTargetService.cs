using Business.Entities.Marketing.SalesTarget;
using Business.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface.Marketing.ISalesTarger
{
    public interface IMarketingSalesTargetService
    {
        Task<PagedDataTable<SalesTarget>> GetAllMarketingSalesTargetAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "SalesTargetID", string sortBy = "ASC");
        Task<int> InsertOrUpdateMarketingSalesTargetAsync(SalesTarget salesTarget);
        Task<SalesTarget> GetMarketingSalesTargetAsync(int MarketingSalesTargetID);
    }
}
