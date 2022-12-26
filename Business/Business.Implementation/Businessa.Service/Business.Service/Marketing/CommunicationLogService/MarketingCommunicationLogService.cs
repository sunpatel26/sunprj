using Business.Entities.Marketing.CommunicationLog;
using Business.Interface.Marketing.CommunicatonLog;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service.Marketing.CommunicationLogService
{
    public class MarketingCommunicationLogService : IMarketingCommunicationLogService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public MarketingCommunicationLogService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<PagedDataTable<CommunicationLog>> GetAllMarketingCommunicationLogAsync(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "MarketingCommunicationLogID", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)
                        };

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_MarketingCommunicationLog", param))
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
                    PagedDataTable<CommunicationLog> lst = table.ToPagedDataTableList<CommunicationLog>
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

        public async Task<int> MarketingCommunicationLogInsertOrUpdateAsync(CommunicationLog communicationLog)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@MarketingCommunicationLogID", communicationLog.MarketingCommunicationLogID)
                ,new SqlParameter("@ContactByPerson", communicationLog.ContactByPerson)
                ,new SqlParameter("@ContactTo", communicationLog.ContactTo)
                ,new SqlParameter("@ContactChannelTypeID", communicationLog.ContactChannelTypeID)
                ,new SqlParameter("@Email", communicationLog.Email)
                ,new SqlParameter("@MobileNo", communicationLog.MobileNo)
                ,new SqlParameter("@VanueTypeID", communicationLog.VanueTypeID)
                ,new SqlParameter("@PartyTypeID", communicationLog.PartyTypeID)
                ,new SqlParameter("@CommunicationLogDate", communicationLog.CommunicationLogDate)
                ,new SqlParameter("@PlaceOfMeeting", communicationLog.PlaceOfMeeting)
                ,new SqlParameter("@IsSentDocument", communicationLog.IsSentDocument)
                ,new SqlParameter("@IsSentMarketingDocument", communicationLog.IsSentMarketingDocument)
                ,new SqlParameter("@ReferenceBetterBusiness", communicationLog.ReferenceBetterBusiness)
                ,new SqlParameter("@ReferenceMobileOrEmail", communicationLog.ReferenceMobileOrEmail)
                ,new SqlParameter("@Note", communicationLog.Note)
                ,new SqlParameter("@Feedback", communicationLog.Feedback)
                ,new SqlParameter("@CreatedOrModifiedBy", communicationLog.CreatedOrModifiedBy)                
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_MarketingCommunicationLog", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CommunicationLog> GetMarketingCommunicationLogAsync(int MarketingCommunicationLogID)
        {
            CommunicationLog result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@MarketingCommunicationLogID", MarketingCommunicationLogID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_MarketingCommunicationLog", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CommunicationLog>();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
