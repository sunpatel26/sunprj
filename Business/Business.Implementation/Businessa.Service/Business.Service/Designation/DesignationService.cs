using Business.Entities.Designation;
using Business.Interface.Designation;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service.Designation
{
    public class DesignationService : IDesignationService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public DesignationService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        /*Designation List*/
        public async Task<PagedDataTable<DesignationMaster>> GetAllDesignationAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "DesignationText", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<DesignationMaster> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_DesignationMaster", param))
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
                    lst = table.ToPagedDataTableList<DesignationMaster>(pageNo, pageSize, totalItemCount);
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }
        /*Designation List*/

        /* Get Designation Start */
        public async Task<DesignationMaster> GetDesignationAsync(int DesignationID)
        {
            DesignationMaster result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@DesignationID", DesignationID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_DesignationMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<DesignationMaster>();
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
        /* Get Designation End */

        /* Designation Add or Update Start */
        public async Task<int> DesignationCreateOrUpdateAsync(DesignationMaster model)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@DesignationID", model.DesignationID),
                    new SqlParameter("@DesignationText", model.DesignationText),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@DesignationGroupID", model.DesignationGroupID),
                    new SqlParameter("@IsActive", model.IsActive),
                    new SqlParameter("@CreatedOrModifiedBy", model.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_DesignationMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /* Designation Add or Update End */

    }
}
