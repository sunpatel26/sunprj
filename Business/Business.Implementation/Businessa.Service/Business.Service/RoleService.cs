using Business.Entities;
using Business.Interface;
using Business.SQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Service
{
    public class SiteRoleRepository : ISiteRoleRepository
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public SiteRoleRepository(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }
        public async Task<PagedDataTable<RoleMasterMetadata>> GetAllRolesAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<RoleMasterMetadata> lst = null;
            try
            {
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_RoleMaster"))
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
                    lst = table.ToPagedDataTableList<RoleMasterMetadata>(1,0, totalItemCount);
                    return lst;
                }
            }
            catch 
            {
                throw ;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }
        public async Task<PagedDataTable<RoleMasterMetadata>> GetAllRolesAsync(int companyID)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<RoleMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CompanyID", companyID) };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_RoleMaster",param))
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
                    lst = table.ToPagedDataTableList<RoleMasterMetadata>(1, 0, totalItemCount);
                    return lst;
                }
            }
            catch 
            {
                throw ;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }
        /// <summary>
        /// Create new Role
        /// Possbible check needed to see if exists?
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateAsync(RoleMasterMetadata role, CancellationToken cancellationToken)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@Name", role.Name)
                ,new SqlParameter("@NormalizedName", role.NormalizedName)
                ,new SqlParameter("@Description", role.Description ?? "")
                };

                await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_I_RoleMaster", param);

                return IdentityResult.Success;

            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Code = ex.HResult.ToString(), Description = ex.Message });
            }
        }

        /// <summary>
        /// Update existing Role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateAsync(RoleMasterMetadata role, CancellationToken cancellationToken)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@RoleID", role.RoleID),
                    new SqlParameter("@Name", role.Name)
                ,new SqlParameter("@NormalizedName", role.NormalizedName)
                ,new SqlParameter("@Description", role.Description ?? "")
                };

                await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_U_RoleMaster", param);

                return IdentityResult.Success;

            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Code = ex.HResult.ToString(), Description = ex.Message });
            }
        }

        /// <summary>
        /// Retrieve role by the supplied Id
        /// </summary>
        /// <param name="normalizedRoleName"></param>
        /// <returns>SiteRole</returns>
        public async Task<RoleMasterMetadata> FindByIdAsync(string roleId)
        {
            var user = new RoleMasterMetadata();
            try
            {
                SqlParameter[] param = { new SqlParameter("@RoleID", roleId) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_RoleMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            user = dr.ToPagedDataTableList<RoleMasterMetadata>();
                        }
                    }
                }
                return user;
            }
            catch 
            {
                throw ;
            }
        }

        /// <summary>
        /// Returns a list of roles the user is in as strings
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Roles</returns>
        public async Task<PagedDataTable<RoleMasterMetadata>> GetRolesByUserIdAsync(UserMasterMetadata user, CancellationToken cancellationToken)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<RoleMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = {new SqlParameter("@UserID",user.UserID)
                        };

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_UserRolesBy_UserId", param))
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
                    lst = table.ToPagedDataTableList<RoleMasterMetadata>();
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

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> DeleteAsync(RoleMasterMetadata role, CancellationToken cancellationToken)
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@RoleID", role.RoleID) };
                await SqlHelper.ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, "usp_Delete_RoleMaster", param);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Code = ex.HResult.ToString(), Description = ex.Message });
            }
        }
        /// <summary>
        /// Retrieve role by the supplied name
        /// </summary>
        /// <param name="normalizedRoleName"></param>
        /// <returns>SiteRole</returns>
        public async Task<RoleMasterMetadata> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            RoleMasterMetadata role = null;// new RoleMasterMetadata();
            try
            {
                SqlParameter[] param = { new SqlParameter("@NormalizedRoleName", normalizedRoleName) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "usp_Get_RoleMaster_Name", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            role = dr.ToPagedDataTableList<RoleMasterMetadata>();
                        }
                    }
                }
                return role;
            }
            catch
            {

                throw ;
            }
        }

        /* Role silder Start */
        public async Task<RoleMasterMetadata> GetRoleAsync(int RoleID)
        {
            RoleMasterMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@RoleID", RoleID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_RoleMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<RoleMasterMetadata>();
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
        /* Role silder End */

        /* PratyTypeRegister Action Start */
        public async Task<int> CreateOrUpdateRole(RoleMasterMetadata model)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@RoleID", model.RoleID)
                ,new SqlParameter("@Name", model.Name)
                /*,new SqlParameter("@IsActive", model.IsActive)*/
                ,new SqlParameter("@CreatedOrModifiedBy", model.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_I_RoleMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /* PratyTypeRegister Action End */



        #region "Roles Claim"
        public async Task<PagedDataTable<RoleClaimsMetadata>> GetAllClaims(int roleID,int compnayID)
        {
            PagedDataTable<RoleClaimsMetadata> role = null;// new RoleMasterMetadata();
            try
            {
                SqlParameter[] param = { new SqlParameter("@RoleID", roleID)
                    ,new SqlParameter("@CompanyID", compnayID)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "usp_GetAll_RoleClaims", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                           role = ds.Tables[0].ToPagedDataTableList<RoleClaimsMetadata>();
                        }
                    }
                }
                return role;
            }
            catch 
            {
                throw ;
            }
        }
        public async Task<IdentityResult> AddPermissionClaim(RoleClaimsMetadata item)
        {
            try
            {
                SqlParameter[] param = { 
                    new SqlParameter("@CompanyID", item.CompanyID)
                    ,new SqlParameter("@RoleID", item.RoleID)
                    ,new SqlParameter("@ClaimType", item.ClaimType)
                    ,new SqlParameter("@ClaimValue", item.ClaimValue)
                    ,new SqlParameter("@Selected", item.Selected)
                };
                await SqlHelper.ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, "usp_IU_RoleClaims", param);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Code = ex.HResult.ToString(), Description = ex.Message });
            }
        }
        public async Task<IdentityResult> AddPermissionClaim(UserClaimsMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CompanyID", item.CompanyID)
                    ,new SqlParameter("@UserID", item.UserID)
                    ,new SqlParameter("@ClaimType", item.ClaimType)
                    ,new SqlParameter("@ClaimValue", item.ClaimValue)
                };
                await SqlHelper.ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, "usp_IU_UserClaims", param);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Code = ex.HResult.ToString(), Description = ex.Message });
            }
        }
        #endregion

    }
}
