using Business.Interface.Marketing.ISalesTarger;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service.Marketing.SalesTarget
{
    public class MarketingSalesTargetService : IMarketingSalesTargetService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public MarketingSalesTargetService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<PagedDataTable<Entities.Marketing.SalesTarget.SalesTarget>> GetAllMarketingSalesTargetAsync(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "CompanyTargetID", string sortBy = "ASC")
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

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_SalesTarget", param))
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
                    PagedDataTable<Entities.Marketing.SalesTarget.SalesTarget> lst = table.ToPagedDataTableList<Entities.Marketing.SalesTarget.SalesTarget>
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

        public async Task<int> InsertOrUpdateMarketingSalesTargetAsync(Entities.Marketing.SalesTarget.SalesTarget salesTarget)
        {
            try
            {
                SqlParameter[] param = {
                 new SqlParameter("@SalesTargetID", salesTarget.SalesTargetID)
                 ,new SqlParameter("@StartDate", salesTarget.StartDate)
                 ,new SqlParameter("@EndDate", salesTarget.EndDate)
                 ,new SqlParameter("@TargetValue", salesTarget.TargetValue)
                 ,new SqlParameter("@FinancialYearID", salesTarget.FinancialYearID)
                 ,new SqlParameter("@IsActive", salesTarget.IsActive)
                 ,new SqlParameter("@CreatedOrModifiedBy", salesTarget.CreatedOrModifiedBy)
                 ,new SqlParameter("@MarketingEmployeeID", salesTarget.MarketingEmployeeID)
                 ,new SqlParameter("@CustomerID", salesTarget.CustomerID)
                 ,new SqlParameter("@ReportingHeadID", salesTarget.ReportingHeadID)
                 ,new SqlParameter("@CompanyID", salesTarget.CompanyID)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_SalesTarget", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Entities.Marketing.SalesTarget.SalesTarget> GetMarketingSalesTargetAsync(int SalesTargetID)
        {
            Entities.Marketing.SalesTarget.SalesTarget result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@SalesTargetID", SalesTargetID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_SalesTarget", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<Entities.Marketing.SalesTarget.SalesTarget>();
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
