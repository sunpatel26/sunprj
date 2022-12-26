using Business.Entities;
using Business.Interface;
using Business.SQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Business.Service
{
    public class SuperAdminService : ISuperAdminService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public SuperAdminService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<int> InsertError(ErrorMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                            new SqlParameter("@ErrorMessage",item.ErrorMessage)
                           ,new SqlParameter("@ErrorDescription",item.ErrorDescription)
                        };
                object sb = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Beta_usp_I_Errors", param);
                return (sb != null) ? (int)sb : 0;
            }
            catch { throw; }
        }

        #region "permission"
        public async Task AddPermission(PermissionMasterMetadata item)
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@PermissionDesc", item.PermissionDesc)
                        , new SqlParameter("@Controller", item.Controller)
                        , new SqlParameter("@Action", item.Action)
                        , new SqlParameter("@Area", item.Area)

                };
                await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_I_PermissionMaster", param);
            }
            catch
            {
                throw;
            }
        }
        public async Task<PagedDataTable<PermissionMasterMetadata>> GetAllPermission(int roleid)
        {
            try
            {
                DataTable table = new DataTable();
                int totalItemCount = 0;
                PagedDataTable<PermissionMasterMetadata> lst = null;
                try
                {
                    SqlParameter[] param = { new SqlParameter("@RoleID", roleid) };
                    using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_PermisisonMaster", param))
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
                        lst = table.ToPagedDataTableList<PermissionMasterMetadata>(1, 0, totalItemCount);
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
            catch
            {
                throw;
            }
        }
        #endregion

        #region "Address Type Master"
        public async Task<AddressTypeMasterMetadata> GetAddressTypeAsync(int addressTypeID)
        {
            AddressTypeMasterMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@AddressTypeID", addressTypeID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_AddressTypeMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<AddressTypeMasterMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> InsertOrUpdateAddressTypeAsync(AddressTypeMasterMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@AddressTypeID", item.AddressTypeID)
                ,new SqlParameter("@AddressTypeText", item.AddressTypeText)

                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_AddressTypeMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }
        public async Task<int> DeleteAddressTypeAsync(int addressTypeID)
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@AddressTypeID", addressTypeID) };
                var obj = await SqlHelper.ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, "usp_Delete_AddressTypeMaster", param);
                return obj;
            }
            catch
            {
                throw;
            }
        }
        public async Task<PagedDataTable<AddressTypeMasterMetadata>> GetAllAddressTypeAsync(int pageNo = 0, int pageSize = 0, string searchString = "", string orderBy = "AddressTypeText", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<AddressTypeMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_AddressTypeMaster"))
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
                    lst = table.ToPagedDataTableList<AddressTypeMasterMetadata>(pageNo, pageSize, totalItemCount);
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
        #endregion

        #region "Business Type Master"
        public async Task<BusinessTypeMasterMetadata> GetBusinessTypeAsync(int businessTypeID)
        {
            BusinessTypeMasterMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("BusinessTypeID", businessTypeID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_BusinessTypeMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<BusinessTypeMasterMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> InsertOrUpdateBusinessTypeAsync(BusinessTypeMasterMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@BusinessTypeID", item.BusinessTypeID)
                ,new SqlParameter("@BusinessTypeText", item.BusinessTypeText)

                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_BusinessTypeMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }
        public async Task<int> DeleteBusinessTypeAsync(int businessTypeID)
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@BusinessTypeID", businessTypeID) };
                var obj = await SqlHelper.ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, "usp_Delete_BusinessTypeMaster", param);
                return obj;
            }
            catch
            {
                throw;
            }
        }
        public async Task<PagedDataTable<BusinessTypeMasterMetadata>> GetAllBusinessTypeAsync(int pageNo = 0, int pageSize = 0, string searchString = "", string orderBy = "BusinessTypeText", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<BusinessTypeMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_BusinessTypeMaster", param))
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
                    lst = table.ToPagedDataTableList<BusinessTypeMasterMetadata>(pageNo, pageSize, totalItemCount);
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
        #endregion

        #region Industry Type Master"
        public async Task<IndustryTypeMasterMetadata> GetIndustryTypeAsync(int industryTypeID)
        {
            IndustryTypeMasterMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@IndustryTypeID", industryTypeID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_IndustryTypeMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<IndustryTypeMasterMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> InsertOrUpdateIndustryTypeAsync(IndustryTypeMasterMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@IndustryTypeID", item.IndustryTypeID)
                ,new SqlParameter("@IndustryTypeText", item.IndustryTypeText)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_IndustryTypeMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }
        public async Task<int> DeleteIndustryTypeAsync(int industryTypeID)
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@IndustryTypeID", industryTypeID) };
                var obj = await SqlHelper.ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, "usp_Delete_IndustryTypeMaster", param);
                return obj;
            }
            catch
            {
                throw;
            }
        }
        public async Task<PagedDataTable<IndustryTypeMasterMetadata>> GetAllIndustryTypeAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "IndustryTypeText", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<IndustryTypeMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_IndustryTypeMaster", param))
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
                    lst = table.ToPagedDataTableList<IndustryTypeMasterMetadata>(pageNo, pageSize, totalItemCount);
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
        #endregion

        #region "Department Master"
        public async Task<PagedDataTable<DepartmentMasterMetadata>> GetAllDepartmentAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "DepartmentName", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<DepartmentMasterMetadata> lst = null;
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
                    lst = table.ToPagedDataTableList<DepartmentMasterMetadata>(pageNo, pageSize, totalItemCount);
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
        #endregion

        #region "Designation Master"
        public async Task<PagedDataTable<DesignationMasterMetadata>> GetAllDesignationAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "DesignationText", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<DesignationMasterMetadata> lst = null;
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
                    lst = table.ToPagedDataTableList<DesignationMasterMetadata>(pageNo, pageSize, totalItemCount);
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

        #endregion

        #region "Email Group  Master"
        public async Task<PagedDataTable<EmailGroupMasterMetadata>> GetAllEmailGroupAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "EmailGroupName", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmailGroupMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmailGroupMaster", param))
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
                    lst = table.ToPagedDataTableList<EmailGroupMasterMetadata>(pageNo, pageSize, totalItemCount);
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

        #endregion

        #region "Country Master"
        public async Task<CountryMasterMetadata> GetCountryAsync(int id)
        {
            CountryMasterMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CountryID", id) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CountryMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CountryMasterMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> InsertOrUpdateCountryAsync(CountryMasterMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@CountryID", item.CountryID)
                ,new SqlParameter("@CountryName", item.CountryName)
                ,new SqlParameter("@CountryShortName", item.CountryShortName)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CountryMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }
        public async Task<int> DeleteCountryAsync(int id)
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@CountryID", id) };
                var obj = await SqlHelper.ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, "usp_Delete_CountryMaster", param);
                return obj;
            }
            catch
            {
                throw;
            }
        }
        public async Task<PagedDataTable<CountryMasterMetadata>> GetAllCountryAsync(int pageNo = 0, int pageSize = 0, string searchString = "", string orderBy = "CountryName", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<CountryMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CountryMaster", param))
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
                    lst = table.ToPagedDataTableList<CountryMasterMetadata>(pageNo, pageSize, totalItemCount);
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
        #endregion

        #region "State Master"
        public async Task<StateMasterMetadata> GetStateAsync(int id)
        {
            StateMasterMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@StateID", id) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_StateMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<StateMasterMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> InsertOrUpdateStateAsync(StateMasterMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                     new SqlParameter("@StateID", item.StateID)
                ,new SqlParameter("@CountryID", item.CountryID)
                ,new SqlParameter("@StateName", item.StateName)
                ,new SqlParameter("@StateShortName", item.StateShortName)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_StateMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }
        public async Task<int> DeleteStateAsync(int id)
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@StateID", id) };
                var obj = await SqlHelper.ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, "usp_Delete_StateMaster", param);
                return obj;
            }
            catch
            {
                throw;
            }
        }
        public async Task<PagedDataTable<StateMasterMetadata>> GetAllStateAsync(int pageNo = 0, int pageSize = 0, string searchString = "", string orderBy = "StateName", string sortBy = "ASC", int countryID = 0)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<StateMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)
                        ,new SqlParameter("@CountryID",countryID)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_StateMaster", param))
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
                    lst = table.ToPagedDataTableList<StateMasterMetadata>(pageNo, pageSize, totalItemCount);
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
        #endregion

        #region "City Master"
        public async Task<CityMasterMetadata> GetCityAsync(int id)
        {
            CityMasterMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CityID", id) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CityMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CityMasterMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> InsertOrUpdateCityAsync(CityMasterMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                     new SqlParameter("@CityID", item.CityID)
                    ,new SqlParameter("@CityName", item.CityName)
                    ,new SqlParameter("@StateID", item.StateID)
                };
                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CityMaster", param);
                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> DeleteCityAsync(int id)
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@CityID", id) };
                var obj = await SqlHelper.ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, "usp_Delete_CityMaster", param);
                return obj;
            }
            catch
            {
                throw;
            }
        }
        public async Task<PagedDataTable<CityMasterMetadata>> GetAllCityAsync(int pageNo = 0, int pageSize = 0, string searchString = "", string orderBy = "CityName", string sortBy = "ASC", int stateID = 0)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<CityMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)
                         ,new SqlParameter("@StateID",stateID)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CityMaster", param))
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
                    lst = table.ToPagedDataTableList<CityMasterMetadata>(pageNo, pageSize, totalItemCount);
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
        #endregion

        #region "District Master"
        public async Task<DistrictMasterMetadata> GetDistrictAsync(int id)
        {
            DistrictMasterMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@DistrictID", id) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_DistrictMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<DistrictMasterMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> InsertOrUpdateDistrictAsync(DistrictMasterMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                     new SqlParameter("@DistrictID", item.DistrictID)
                    ,new SqlParameter("@DistrictName", item.DistrictName)
                    ,new SqlParameter("@StateID", item.StateID)
                };
                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_DistrictMaster", param);
                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> DeleteDistrictAsync(int id)
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@DistrictID", id) };
                var obj = await SqlHelper.ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, "usp_Delete_DistrictMaster", param);
                return obj;
            }
            catch
            {
                throw;
            }
        }
        public async Task<PagedDataTable<DistrictMasterMetadata>> GetAllDistrictAsync(int pageNo = 0, int pageSize = 0, string searchString = "", string orderBy = "DistrictName", string sortBy = "ASC", int stateID = 0)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<DistrictMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)
                         ,new SqlParameter("@StateID",stateID)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_DistrictMaster", param))
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
                    lst = table.ToPagedDataTableList<DistrictMasterMetadata>(pageNo, pageSize, totalItemCount);
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
        #endregion

        #region "Taluka Master"
        public async Task<TalukaMasterMetadata> GetTalukaAsync(int id)
        {
            TalukaMasterMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@TalukaID", id) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_TalukaMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<TalukaMasterMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> InsertOrUpdateTalukaAsync(TalukaMasterMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                     new SqlParameter("@TalukaID", item.TalukaID)
                    ,new SqlParameter("@TalukaName", item.TalukaName)
                    ,new SqlParameter("@DistrictID", item.DistrictID)
                };
                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_TalukaMaster", param);
                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> DeleteTalukaAsync(int id)
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@TalukaID", id) };
                var obj = await SqlHelper.ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, "usp_Delete_TalukaMaster", param);
                return obj;
            }
            catch
            {
                throw;
            }
        }
        public async Task<PagedDataTable<TalukaMasterMetadata>> GetAllTalukaAsync(int pageNo = 0, int pageSize = 0, string searchString = "", string orderBy = "TalukaName", string sortBy = "ASC", int districtID = 0)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<TalukaMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)
                         ,new SqlParameter("@DistrictID",districtID)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_TalukaMaster", param))
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
                    lst = table.ToPagedDataTableList<TalukaMasterMetadata>(pageNo, pageSize, totalItemCount);
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
        #endregion

        #region "Zipcode Master"
        public async Task<ZipcodeMasterMetadata> GetZipcodeAsync(int id)
        {
            ZipcodeMasterMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@ZIPCodeID", id) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_ZIPCodeMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<ZipcodeMasterMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> InsertOrUpdateZipcodeAsync(ZipcodeMasterMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                     new SqlParameter("@ZIPCodeID", item.ZIPCodeID)
                   ,new SqlParameter("@ZIPCode", item.ZIPCode)
                   ,new SqlParameter("@CityID", item.CityID)
                   ,new SqlParameter("@DistrictID", item.DistrictID)
                   ,new SqlParameter("@Latitude", item.Latitude)
                   ,new SqlParameter("@Longitude", item.Longitude)
                   ,new SqlParameter("@Timezone", item.Timezone)
                   ,new SqlParameter("@DaylightSaving", item.DaylightSaving)
                   ,new SqlParameter("@TalukaId", item.TalukaID)
                   ,new SqlParameter("@AreaOfficeName", item.AreaOfficeName)
                };
                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_ZIPCodeMaster", param);
                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> DeleteZipcodeAsync(int id)
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@ZIPCodeID", id) };
                var obj = await SqlHelper.ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, "usp_Delete_ZIPCodeMaster", param);
                return obj;
            }
            catch
            {
                throw;
            }
        }
        public async Task<PagedDataTable<ZipcodeMasterMetadata>> GetAllZipcodeAsync(int pageNo = 0, int pageSize = 0, string searchString = "", string orderBy = "TalukaName", string sortBy = "ASC", int districtID = 0)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<ZipcodeMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)
                         ,new SqlParameter("@DistrictID",districtID)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_ZIPCodeMaster", param))
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
                    lst = table.ToPagedDataTableList<ZipcodeMasterMetadata>(pageNo, pageSize, totalItemCount);
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
        public async Task<PagedDataTable<ZipcodeMasterMetadata>> GetAllZipcodeAutoCompletAsync(string searchString)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<ZipcodeMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@SearchString",searchString)
                   };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_ZIPCodeMaster_Autocomplete", param))
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
                    lst = table.ToPagedDataTableList<ZipcodeMasterMetadata>();
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

        #endregion

        public PagedDataTable<IdentityProofTypeMetadata> GetIdentityProofTypeAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<IdentityProofTypeMetadata> lst = new PagedDataTable<IdentityProofTypeMetadata>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_IdentityProofTypeMasterData"))
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
                    lst = table.ToPagedDataTableList<IdentityProofTypeMetadata>
                       (1, 20, totalItemCount, null, "IdentityProofTypeText", "ASC");
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
            return lst;
        }

        public PagedDataTable<VehicleTypeMasterMetaData> GetVehicleTypeAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<VehicleTypeMasterMetaData> lst = new PagedDataTable<VehicleTypeMasterMetaData>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_VehicleTypeMasterData"))
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
                    lst = table.ToPagedDataTableList<VehicleTypeMasterMetaData>
                       (1, 20, totalItemCount, null, "VehicleTypeText", "ASC");
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
            return lst;
        }


        public PagedDataTable<FeedbackQuestionMasterMetadata> GetFeedbackQuestions()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<FeedbackQuestionMasterMetadata> lst = new PagedDataTable<FeedbackQuestionMasterMetadata>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_FeedbackQuestionMaster"))
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
                    lst = table.ToPagedDataTableList<FeedbackQuestionMasterMetadata>
                       (1, 20, totalItemCount, null, "FeedbackQuestionID", "ASC");
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
            return lst;
        }

        public PagedDataTable<ZipcodeMasterMetadata> GetZipCodeAsync(string search)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<ZipcodeMasterMetadata> lst = new PagedDataTable<ZipcodeMasterMetadata>();
            try
            {
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@SearchString", search) };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_ZipCodeMaster", sp))
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
                    lst = table.ToPagedDataTableList<ZipcodeMasterMetadata>
                       (1, 20, totalItemCount, null, "ZipCode", "ASC");
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
            return lst;
        }

        #region "Document Type Master"
        public async Task<DocumentTypeMasterMetadata> GetDocumentTypeAsync(int documentTypeID)

        {
            DocumentTypeMasterMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@DocumentTypeID", documentTypeID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_DocumentTypeMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<DocumentTypeMasterMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> InsertOrUpdateDocumentTypeAsync(DocumentTypeMasterMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@DocumentTypeID", item.DocumentTypeID)
                ,new SqlParameter("@DocumentTypeName", item.DocumentTypeName)
                 ,new SqlParameter("@IsActive", item.IsActive)
                 ,new SqlParameter("@CreatedOrModifiedBy", item.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_DocumentTypeMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }
        public async Task<int> DeleteDocumentTypeAsync(int documentTypeID)

        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@DocumentTypeID", documentTypeID) };
                var obj = await SqlHelper.ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, "usp_Delete_DocumentTypeMaster", param);
                return obj;
            }
            catch
            {
                throw;
            }
        }
        public async Task<PagedDataTable<DocumentTypeMasterMetadata>> GetAllDocumentTypeAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "DocumentTypeName", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<DocumentTypeMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_DocumentTypeMaster", param))
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
                    lst = table.ToPagedDataTableList<DocumentTypeMasterMetadata>(pageNo, pageSize, totalItemCount);
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
        #endregion
    }
}

