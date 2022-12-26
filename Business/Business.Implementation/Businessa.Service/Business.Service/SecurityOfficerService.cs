using Business.Interface;
using Business.SQL;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using Business.Entities.SecurityOfficer;
using Microsoft.Extensions.Configuration;
using Business.Entities.Employee;

namespace Business.Service
{
    public class SecurityOfficerService : ISecurityOfficerService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public SecurityOfficerService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }
        public async Task<PagedDataTable<SecurityOfficerMaster>> GetAllSecurityOfficerAsync(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "SecurityOfficerID", string sortBy = "ASC")
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
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_SecurityOfficer", param))
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
                    PagedDataTable<SecurityOfficerMaster> lst = table.ToPagedDataTableList<SecurityOfficerMaster>
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
        public async Task<int> AddUpdateSecurityOfficer(SecurityOfficerMaster securityOfficerMaster)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@SecurityOfficerID",securityOfficerMaster.SecurityOfficerID),
                new SqlParameter("@SecurityOfficerName", securityOfficerMaster.SecurityOfficerName ),
                new SqlParameter("@SecurityAgencyName", securityOfficerMaster.SecurityAgencyName),
                new SqlParameter("@SecurityOfficerMobile", securityOfficerMaster.SecurityOfficerMobile),
                new SqlParameter("@IdentityProofTypeID", securityOfficerMaster.IdentityProofTypeID ),
                new SqlParameter("@IdentityProofNumber", securityOfficerMaster.IdentityProofNumber ),
                new SqlParameter("@CompanyID", securityOfficerMaster.CompanyID ),
                new SqlParameter("@IsActive", securityOfficerMaster.IsActive ? true : true ),
                new SqlParameter("@CreatedOrModifiedBy", securityOfficerMaster.CreatedOrModifiedBy ),
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_SecurityOfficer", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<SecurityOfficerMaster> GetSecurityOfficerAsync(int securityOfficerID)
        {
            SecurityOfficerMaster result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@SecurityOfficerID", securityOfficerID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_SecurityOfficer", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<SecurityOfficerMaster>();
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
