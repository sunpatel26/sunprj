using Business.Entities.Marketing.CompanySale;
using Business.Interface.Marketing.ICompanyTarget;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service.Marketing.CompanySales
{
    public class MarketingCompanyTargetService : IMarketingCompanyTargetService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public MarketingCompanyTargetService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<PagedDataTable<CompanyTarget>> GetAllMarketingCompanyTargetAsync(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "CompanyTargetID", string sortBy = "ASC")
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

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CompanyTarget", param))
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
                    PagedDataTable<CompanyTarget> lst = table.ToPagedDataTableList<CompanyTarget>
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

        public async Task<int> InsertOrUpdateMarketingCompanyTargetAsync(CompanyTarget companyTarget)
        {
            try
            {
                SqlParameter[] param = {
                  new SqlParameter("@CompanyTargetID", companyTarget.CompanyTargetID)
                 ,new SqlParameter("@CompanyID", companyTarget.CompanyID)
                 ,new SqlParameter("@StartDate", companyTarget.StartDate)
                 ,new SqlParameter("@EndDate", companyTarget.EndDate)
                 ,new SqlParameter("@TargetValue", companyTarget.TargetValue)
                 ,new SqlParameter("@FinancialYearID", companyTarget.FinancialYearID)
                 ,new SqlParameter("@IsActive", companyTarget.IsActive)
                 ,new SqlParameter("@CreatedOrModifiedBy", companyTarget.CreatedOrModifiedBy)
                 };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyTarget", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CompanyTarget> GetMarketingCompanyTargetAsync(int CompanyTargetID)
        {
            CompanyTarget result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CompanyTargetID", CompanyTargetID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CompanyTarget", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CompanyTarget>();
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
