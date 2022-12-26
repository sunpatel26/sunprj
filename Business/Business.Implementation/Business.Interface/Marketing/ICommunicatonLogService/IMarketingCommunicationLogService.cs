using Business.Entities.Marketing.CommunicationLog;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.Marketing.CommunicatonLog
{
    public interface IMarketingCommunicationLogService
    {
        Task<PagedDataTable<CommunicationLog>> GetAllMarketingCommunicationLogAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "MarketingCommunicationLogID", string sortBy = "ASC");
        Task<int> MarketingCommunicationLogInsertOrUpdateAsync(CommunicationLog communicationLog);
        Task<CommunicationLog> GetMarketingCommunicationLogAsync(int MarketingCommunicationLogID);
    }
}
