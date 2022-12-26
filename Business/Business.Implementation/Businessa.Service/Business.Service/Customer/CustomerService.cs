using Business.Entities.Employee;
using Business.Interface.ICustomer;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Business.Entities.Customer;

namespace Business.Service.Customer
{
    public class CustomerService : ICustomerService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public CustomerService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<PagedDataTable<CustomerMaster>> GetAllCustomerAsync(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "CustomerID", string sortBy = "ASC")
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

                //    using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeMaster", param))     Change SP to customers

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CustomerMaster", param))
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
                    PagedDataTable<CustomerMaster> lst = table.ToPagedDataTableList<CustomerMaster>
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

        public async Task<CustomerMaster> GetCustomerAsync(int customerId)
        {
            CustomerMaster result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CustomerID", customerId) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CustomerMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CustomerMaster>();
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

        public async Task<int> AddUpdateCustomer(CustomerMaster customerMaster)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerID", customerMaster.CustomerID),
                    new SqlParameter("@CustomerCode", customerMaster.CustomerCode),
                    new SqlParameter("@CustomerName", customerMaster.CustomerName),
                    new SqlParameter("@GroupName", customerMaster.GroupName),
                    new SqlParameter("@OwnerName", customerMaster.OwnerName),
                    new SqlParameter("@ContactPhone1", customerMaster.ContactPhone1),
                    new SqlParameter("@Mobile1", customerMaster.Mobile1),
                    new SqlParameter("@FaxNo", customerMaster.FaxNo),
                    new SqlParameter("@IndustryTypeID", customerMaster.IndustryTypeID),
                    new SqlParameter("@BusinessTypeID", customerMaster.BusinessTypeID),
                    new SqlParameter("@IsActive", customerMaster.IsActive),
                    new SqlParameter("@CreatedOrModifiedBy", customerMaster.CreatedOrModifiedBy),
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CustomerMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateCustomerLogoImage(CustomerLogoImage customerLogoImage)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@CustomerID", customerLogoImage.CustomerID),
                new SqlParameter("@LogoImageName", customerLogoImage.LogoImageName),
                new SqlParameter("@LogoImagePath", customerLogoImage.LogoImagePath),
                new SqlParameter("@IsActive", customerLogoImage.IsActive),
                new SqlParameter("@CreatedOrModifiedBy", customerLogoImage.CreatedOrModifiedBy ),

                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CustomerLogoImage", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Contact Person Detail
        public async Task<PagedDataTable<CustomerContactTxn>> GetCustomerAllContactPerson(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int customerId = 0)
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
                        ,new SqlParameter("@CustomerID",customerId)
                        };

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CustomerContactTxn", param))
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
                    PagedDataTable<CustomerContactTxn> lst = table.ToPagedDataTableList<CustomerContactTxn>
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

        public async Task<CustomerContactTxn> GetCustomerContactPerson(int customerContactID, int customerId)
        {
            CustomerContactTxn result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerContactID", customerContactID),
                    new SqlParameter("@CustomerID", customerId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CustomerContactTxn", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CustomerContactTxn>();
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

        public async Task<int> AddUpdateCustomerContactPerson(CustomerContactTxn customerContactTxn)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerContactID", customerContactTxn.CustomerContactID)
                    ,new SqlParameter("@CustomerID", customerContactTxn.CustomerID)
                    ,new SqlParameter("@Prefix", customerContactTxn.Prefix)
                    ,new SqlParameter("@ContactPerson", customerContactTxn.ContactPersonName)
                    ,new SqlParameter("@DesignationID", customerContactTxn.DesignationID)
                    ,new SqlParameter("@DepartmentID", customerContactTxn.DepartmentID)
                    ,new SqlParameter("@PersonalMobile", customerContactTxn.PersonalMobile)
                    ,new SqlParameter("@OfficeMobile", customerContactTxn.OfficeMobile)
                    ,new SqlParameter("@PersonalEmailID", customerContactTxn.PersonalEmailID)
                    ,new SqlParameter("@OfficeEmailID", customerContactTxn.OfficeEmailID)
                    ,new SqlParameter("@AlternativeMobile", customerContactTxn.AlternativeMobile)
                    ,new SqlParameter("@EmailGroupName", customerContactTxn.EmailGroupName)
                    ,new SqlParameter("@BirthDate", customerContactTxn.BirthDate)
                    ,new SqlParameter("@Religion", customerContactTxn.Religion)
                    ,new SqlParameter("@IsActive", customerContactTxn.IsActive)
                    ,new SqlParameter("@Notes", customerContactTxn.Notes)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CustomerContactTxn", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion Contact Person Detail

        #region AddressList
        public async Task<PagedDataTable<EmployeeAddressTxn>> GetEmployeesAllAddressAsync(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0)
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
                        ,new SqlParameter("@EmployeeID",employeeId)
                        };

                var list = SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeAddressTxn", param);

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeAddressTxn", param))
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
                    PagedDataTable<EmployeeAddressTxn> lst = table.ToPagedDataTableList<EmployeeAddressTxn>
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
        #endregion AddressList
    }
}
