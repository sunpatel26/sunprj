using Business.Entities.Marketing.Meeting;
using Business.Interface.Marketing.IMeeting;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service.Marketing.Meeting
{
    public class MarketinggMeetingService : IMarketingMeeting

    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public MarketinggMeetingService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<int> MarketingMeetingInsertOrUpdateAsync(MarketingMeeting MarketingMeeting)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@MarketingMeetingID", MarketingMeeting.MarketingMeetingID)
                ,new SqlParameter("@Subject", MarketingMeeting.Subject)
                ,new SqlParameter("@StartDate", MarketingMeeting.StartDate)
                ,new SqlParameter("@EndDate", MarketingMeeting.EndDate)
                ,new SqlParameter("@MeetingDuration", MarketingMeeting.MeetingDuration)
                ,new SqlParameter("@Remainder", MarketingMeeting.Remainder)
                ,new SqlParameter("@Description", MarketingMeeting.Description)
                ,new SqlParameter("@MeetingStatusID", MarketingMeeting.MeetingStatusID)
                ,new SqlParameter("@ContactPerson", MarketingMeeting.ContactPerson)
                ,new SqlParameter("@MeetingRelatedTo", MarketingMeeting.MeetingRelatedTo)
                ,new SqlParameter("@MeetingLocation", MarketingMeeting.MeetingLocation)
                ,new SqlParameter("@AssignTo", MarketingMeeting.AssignTo)
                ,new SqlParameter("@CreatedOrModifiedBy", MarketingMeeting.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_MarketingMeeting", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedDataTable<MarketingMeeting>> GetAllMarketingMeetingAsync(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "DepartmentID", string sortBy = "ASC")
        {

            {
                DataTable table = new DataTable();
                int totalItemCount = 0;
                try
                {
                   /* SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)
                        };*/

                    using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_MarketingMeeting"))
                    {
                        if (ds.Tables.Count > 0)
                        {
                            table = ds.Tables[0];
                            if (table.Rows.Count > 0)
                            {
                                if (table.ContainColumn("TotalCount"))
                                    totalItemCount = Convert.ToInt32(table.Rows[0]["TotalCount"]);
                                else
                                    totalItemCount = table.Rows.Count;
                            }
                        }
                        PagedDataTable<MarketingMeeting> lst = table.ToPagedDataTableList<MarketingMeeting>
                            (pageNo, pageSize, totalItemCount, searchString, orderBy, sortBy);
                        return lst;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (table != null)
                        table.Dispose();
                }
            }
        }
    }
}
