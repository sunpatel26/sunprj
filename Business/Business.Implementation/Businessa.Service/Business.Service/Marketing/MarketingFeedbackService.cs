using Business.Entities.Marketing.Feedback;
using Business.Interface.Marketing;
using Business.SQL;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service.Marketing
{
    public class MarketingFeedbackService : IMarketingFeedbackService

    {

        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public MarketingFeedbackService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<PagedDataTable<MarketingFeedback>> GetAllMarketingFeedbackAsync(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "DepartmentID", string sortBy = "ASC")
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

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_MarketingFeedback", param))
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
                    PagedDataTable<MarketingFeedback> lst = table.ToPagedDataTableList<MarketingFeedback>
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

        public async Task<int> MarketingFeedbackCreateAsync(MarketingFeedback marketingFeedback)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@MarketingFeedbackID", marketingFeedback.MarketingFeedbackID)
                ,new SqlParameter("@FeedbackDate", DateTime.Now)
                ,new SqlParameter("@PartyName", marketingFeedback.PartyName)
                ,new SqlParameter("@PartyTypeID", marketingFeedback.PartyTypeID)
                ,new SqlParameter("@Email", marketingFeedback.Email)
                ,new SqlParameter("@MobileNo", marketingFeedback.MobileNo)
                ,new SqlParameter("@IsReceivedDocument", marketingFeedback.IsReceivedDocument)
                ,new SqlParameter("@Note", marketingFeedback.Note)
                ,new SqlParameter("@CreatedOrModifiedBy", marketingFeedback.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_MarketingFeedback", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<MarketingFeedback> GetMarketingFeedbackAsync(string MarketingFeedbackID)
        {
            MarketingFeedback result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@MarketingFeedbackID", MarketingFeedbackID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_MarketingFeedback", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<MarketingFeedback>();
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

        public MarketingFeedback GetForm(int id, int MarketingFeedbackID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@MarketingFeedbackID", MarketingFeedbackID);
                return QueryHelper.GetTableDetail<MarketingFeedback>(connection, "Usp_Get_MasterList", param);
            }
            catch
            {
                throw;
            }
        }
    }
}
