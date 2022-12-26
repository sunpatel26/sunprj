using Business.Entities.Department;
using Business.Interface.Department;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service.Department
{
    public class DepartmentService : IDepartmentService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public DepartmentService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        /*Department Master Get All List Start*/
        public async Task<PagedDataTable<DepartmentMaster>> GetAllDepartmentAsync(int pageNo=1, int pageSize=5, string searchString = "", string orderBy = "DepartmentID", string sortBy = "ASC")
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

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_DepartmentMaster", param))
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
                    PagedDataTable<DepartmentMaster> lst = table.ToPagedDataTableList<DepartmentMaster>
                        (pageNo, pageSize, totalItemCount, searchString, orderBy,sortBy);
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
        /*Department Master Get All List End*/

        /* Add New Department Start*/
        public async Task<int> DepartmemtCreateOrUpdateAsync(DepartmentMaster model)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@DepartmentID", model.DepartmentID),
                    new SqlParameter("@DepartmentName", model.DepartmentName),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@DepartmentGroupID", model.DepartmentGroupID),
                    new SqlParameter("@IsActive", model.IsActive),
                    new SqlParameter("@CreatedOrModifiedBy", model.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_DepartmentMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /* Add New Department End*/

        /* Update Department Start */
        public async Task<DepartmentMaster> GetDepartmentAsync(int DepartmentID)
        {
            DepartmentMaster result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@DepartmentID", DepartmentID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_DepartmentMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<DepartmentMaster>();
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
        /* Update Department End */
    }
}
