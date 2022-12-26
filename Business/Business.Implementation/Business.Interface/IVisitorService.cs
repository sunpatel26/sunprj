using Business.Entities;
using Business.SQL;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IVisitorService
    {
        int AddVisitorRequestAsync(VisitorMetaData user, int LoggedInUserID, IFormFile files, string filePath);

        PagedDataTable<VisitorMetaData> GetAll(int pageNo = 1, int pageSize = 0, string orderBy = "VisitorMeetingRequestID", string sortBy = "ASC", string searchString = "", int id = 0, int userid  = 0 );

        int ApproveVisitorRequest(VisitorRequestApproveMetaData user, int LoggedInUserID);
        Task<VisitorRequestApproveMetaData> GetVisitorAsync(string QRCode);
        Task<VisitorMeetingStatus> GetVisitorMeetingDetail(int VisitorMeetingRequestID);
        int setVisitorStatus(VisitorMeetingStatus model, int LoggedInUserID);

        int InsertFeedback(VisitorFeedbackMetaData model);

        VisitorMeetingRequestFile GetVisitorMeetingRequestFile(int VisitorMeetingRequestID);
        Task<DashbaordCount> GetDashboardCounts(int userid);
    }
}
