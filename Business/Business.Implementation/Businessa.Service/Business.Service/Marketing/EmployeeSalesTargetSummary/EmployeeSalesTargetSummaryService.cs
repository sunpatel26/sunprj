using Business.Interface.Marketing.IEmployeeSalesTargetSummary;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service.Marketing.EmployeeSalesTargetSummary
{
    public class EmployeeSalesTargetSummaryService : IEmployeeSalesTargetSummaryService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public EmployeeSalesTargetSummaryService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<PagedDataTable<Entities.Marketing.EmployeeSalesTargetSummary.EmployeeSalesTargetSummary>> GetAllMarketingEmployeeSalesTargetSummaryAsync(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "SalesTargetID", string sortBy = "ASC")
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

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeSalesTargetSummary", param))
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
                    PagedDataTable<Entities.Marketing.EmployeeSalesTargetSummary.EmployeeSalesTargetSummary> lst = table.ToPagedDataTableList<Entities.Marketing.EmployeeSalesTargetSummary.EmployeeSalesTargetSummary>
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
