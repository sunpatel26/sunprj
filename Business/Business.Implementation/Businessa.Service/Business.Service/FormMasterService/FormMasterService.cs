using Business.Entities.FormMasterEntitie;
using Business.Interface.IFormMaster;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service.FormMasterService
{
    public class FormMasterService : IFormMasterService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public FormMasterService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        /* FormMaster Index Page Start */
        public async Task<PagedDataTable<FormMaster>> GetAllFormMasterAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "PackageName", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<FormMaster> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_FormMaster", param))
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
                    lst = table.ToPagedDataTableList<FormMaster>(pageNo, pageSize, totalItemCount);
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
        /* FormMaster Index Page End */

        /* FormMaster silder Start */
        //Get detail of individual row
        public async Task<FormMaster> GetFormMasterAsync(int FormID)
        {
            FormMaster result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@FormID", FormID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_FormMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<FormMaster>();
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
        /* FormMaster silder End */

        /* Package Insert or Update Start */
        public async Task<int> InsertOrUpdateFormMasterAsync(FormMaster formMaster)
        {
            try
            {
                SqlParameter[] param = {
                 new SqlParameter("@FormID", formMaster.FormID)
                 ,new SqlParameter("@FormName",formMaster.FormName)
                 ,new SqlParameter("@Area", formMaster.Area)
                 ,new SqlParameter("@Controller", formMaster.Controller)
                 ,new SqlParameter("@Action", formMaster.Action)
                 ,new SqlParameter("@FormTypeID", formMaster.FormTypeID)
                 ,new SqlParameter("@IsActive", formMaster.IsActive)
                 ,new SqlParameter("@CreatedOrModifiedBy", formMaster.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_FormMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* Package Insert or Update End */
    }
}
