using Business.Entities.PartyType;
using Business.Interface.IPartyTypeService;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

/* Party Type Service */

namespace Business.Service
{
    public class PartyTypeService : IPartyTypeService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public PartyTypeService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        /* Party Type Listing Page Start */
        public async Task<PagedDataTable<PartyType>> GetAllPartyTypeAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "CompanyName", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<PartyType> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_PartyTypeMaster", param))
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
                    lst = table.ToPagedDataTableList<PartyType>(pageNo, pageSize, totalItemCount);
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
        /* Party Type Listing Page End */

        /* PratyTypeRegister Action Start */
        public async Task<int> PartyTypeCreateOrUpdateAsync(PartyType model)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@PartyTypeID", model.PartyTypeID)
                ,new SqlParameter("@PartyTypeText", model.PartyTypeText)
                ,new SqlParameter("@Remark", model.Remark)
                ,new SqlParameter("@IsActive", model.IsActive)
                ,new SqlParameter("@CreatedOrModifiedBy", model.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_PartyTypeMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /* PratyTypeRegister Action End */

        /* Update Party Type Start */
        public async Task<PartyType> GetPartyTypeAsync(int PartyTypeID)
        {
            PartyType result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@PartyTypeID", PartyTypeID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_PartyTypeMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<PartyType>();
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
        /* Update Party Type End */
    }
}
