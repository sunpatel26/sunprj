using Business.Entities.Marketing.Feedback;
using Business.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface.Marketing
{
    public interface IMarketingFeedbackService
    {
        Task<PagedDataTable<MarketingFeedback>> GetAllMarketingFeedbackAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "DepartmentID", string sortBy = "ASC");
        Task<int> MarketingFeedbackCreateAsync(MarketingFeedback marketingFeedback);

        Task<MarketingFeedback> GetMarketingFeedbackAsync(string MarketingFeedbackID);

        MarketingFeedback GetForm(int id, int MarketingFeedbackID);

    }
}
