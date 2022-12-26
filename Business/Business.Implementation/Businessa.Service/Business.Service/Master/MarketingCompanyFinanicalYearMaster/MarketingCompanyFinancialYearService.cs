using Business.Entities.Master.MarketingCompanyFinancialYearMaster;
using Business.Interface.IMaster.IMarketingCompanyFinanicalYearMaster;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service.Master.MarketingCompanyFinanicalYearMaster
{
    public class MarketingCompanyFinancialYearService : IMarketingCompanyFinancialYear
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public MarketingCompanyFinancialYearService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<PagedDataTable<FinancialYearMaster>> GetAllFinancialYearAsync(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "FinancialYearID", string sortBy = "ASC")
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

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_FinancialYearMaster", param))
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
                    PagedDataTable<FinancialYearMaster> lst = table.ToPagedDataTableList<FinancialYearMaster>
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

        public async Task<FinancialYearMaster> GetFinancialYearAsync(int FinancialYearID)
        {
            FinancialYearMaster result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@FinancialYearID", FinancialYearID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_FinancialYearMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<FinancialYearMaster>();
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

        public async Task<int> InsertOrUpdateFinancialYearAsync(FinancialYearMaster financialYearMaster)
        {
            try
            {
                SqlParameter[] param = {
                 new SqlParameter("@FinancialYearID", financialYearMaster.FinancialYearID)
                 ,new SqlParameter("@FinancialYear", financialYearMaster.FinancialYear)
                 ,new SqlParameter("@StartMonth", financialYearMaster.StartMonth)
                 ,new SqlParameter("@EndMonth", financialYearMaster.EndMonth)
                 ,new SqlParameter("@FinYearDesc", financialYearMaster.FinYearDesc)                 
                 ,new SqlParameter("@CreatedOrModifiedBy", financialYearMaster.CreatedOrModifiedBy)
                 };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_FinancialYearMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
