using Business.Entities.Employee;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Threading.Tasks;
using Business.Interface.IEmployee;

namespace Business.Service
{
    public class EmployeeService : IEmployeeService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public EmployeeService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }
        public async Task<PagedDataTable<EmployeeMaster>> GetAllEmployeesAsync(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "EmployeeID", string sortBy = "ASC")
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

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeMaster", param))
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
                    PagedDataTable<EmployeeMaster> lst = table.ToPagedDataTableList<EmployeeMaster>
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
        #region Basic detail
        public async Task<int> AddUpdateEmployee(AddUpdateEmployee addUpdateEmployee)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeID",addUpdateEmployee.EmployeeID),
                new SqlParameter("@EmployeeCode", addUpdateEmployee.EmployeeCode ),
                new SqlParameter("@PrefixName", addUpdateEmployee.PrefixName ),
                new SqlParameter("@FirstName", addUpdateEmployee.FirstName ),
                new SqlParameter("@MiddleName", addUpdateEmployee.MiddleName ),
                new SqlParameter("@LastName", addUpdateEmployee.LastName ),
                new SqlParameter("@GenderID", addUpdateEmployee.GenderID ),
                new SqlParameter("@EmployeeBloodGroupID", addUpdateEmployee.EmployeeBloodGroupID),
                //new SqlParameter("@IsActive", addUpdateEmployee.IsActive ),
                new SqlParameter("@CreatedOrModifiedBy", addUpdateEmployee.CreatedOrModifiedBy ),
                new SqlParameter("@CompanyID", addUpdateEmployee.CompanyID ),
                new SqlParameter("@JobTitle", addUpdateEmployee.JobTitle ),
                new SqlParameter("@DepartmentID", addUpdateEmployee.DepartmentID ),
                new SqlParameter("@DesignationID", addUpdateEmployee.DesignationID ),
                new SqlParameter("@ReportingTo", addUpdateEmployee.ReportingTo),
                new SqlParameter("@PersonalMobileNo", addUpdateEmployee.PersonalMobileNo ),
                new SqlParameter("@OfficeMobileNo", addUpdateEmployee.OfficeMobileNo ),
                new SqlParameter("@AlternativeMobileNo", addUpdateEmployee.AlternativeMobileNo ),
                new SqlParameter("@IsResigned", addUpdateEmployee.IsResigned ),
                new SqlParameter("@Note", addUpdateEmployee.Note ),
                new SqlParameter("@EmailGroupID", addUpdateEmployee.EmailGroupID ),
                new SqlParameter("@PersonalEmail", addUpdateEmployee.PersonalEmail ),
                new SqlParameter("@OfficeEmail", addUpdateEmployee.OfficeEmail ),
                new SqlParameter("@BirthDate", addUpdateEmployee.BirthDate ),
                new SqlParameter("@Religion", addUpdateEmployee.Religion ),
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> UpdateEmployeeProfilePhoto(EmployeeProfileImage employeeProfileImage)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@EmployeeID", employeeProfileImage.EmployeeID),
                new SqlParameter("@ImageName", employeeProfileImage.ImageName),
                new SqlParameter("@ImagePath", employeeProfileImage.ImagePath),
                new SqlParameter("@IsActive", employeeProfileImage.IsActive),
                new SqlParameter("@CreatedOrModifiedBy", employeeProfileImage.CreatedOrModifiedBy ),

                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeProfileImage", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<AddUpdateEmployee> GetEmployeeAsync(int employeeId)
        {
            AddUpdateEmployee result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@EmployeeID", employeeId) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<AddUpdateEmployee>();
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
        #endregion Basic detail

        #region Address detail
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
        public async Task<int> CreateOrUpdateEmployeeAddressAsync(EmployeeAddressTxn addressMaster)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeAddressTxnID", addressMaster.EmployeeAddressTxnID)
                    ,new SqlParameter("@EmployeeID", addressMaster.EmployeeID)
                    ,new SqlParameter("@Address1", addressMaster.PlotNoName)
                    ,new SqlParameter("@Address2", addressMaster.StreetNoName)
                    //,new SqlParameter("@Address3", addressMaster.Address3)
                    ,new SqlParameter("@Landmark", addressMaster.Landmark)
                    ,new SqlParameter("@Area", addressMaster.Area)
                    ,new SqlParameter("@ZIPCode", addressMaster.ZIPCode)
                    ,new SqlParameter("@IsActive", addressMaster.IsActive)
                    ,new SqlParameter("@City", addressMaster.City)
                    ,new SqlParameter("@District", addressMaster.DistrictName)
                    ,new SqlParameter("@Taluka", addressMaster.Taluka)
                    ,new SqlParameter("@AddressTypeID", addressMaster.AddressTypeID)
                    ,new SqlParameter("@CountryID", addressMaster.CountryID)
                    ,new SqlParameter("@StateID", addressMaster.StateID)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeAddressTxn", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<EmployeeAddressTxn> GetEmployeeAddressTxn(int employeeAddressTxnId, int employeeId)
        {
            EmployeeAddressTxn result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeAddressTxnID", employeeAddressTxnId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeAddressTxn", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeAddressTxn>();
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
        #endregion Address detail

        #region Family Background detail
        public async Task<EmployeeFamilyDetail> GetEmployeeFamily(int employeeId)
        {
            EmployeeFamilyDetail result = null;
            try
            {
                SqlParameter[] param = {
                    //new SqlParameter("@EmployeeFamilyDetailID", employeeFamilyId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeFamilyDetail", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeFamilyDetail>();
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

        public async Task<int> CreateOrUpdateEmployeeFamilyBackgroundAsync(EmployeeFamilyDetail employeeFamilyDetail)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeFamilyDetailID",employeeFamilyDetail.EmployeeFamilyDetailID)
                    ,new SqlParameter("@EmployeeID",employeeFamilyDetail.EmployeeID)
                    //,new SqlParameter("@EmployeeBloodGroupID",employeeFamilyDetail.EmployeeBloodGroupID)
                    ,new SqlParameter("@MaritalStatusID",employeeFamilyDetail.MaritalStatusID)
                    ,new SqlParameter("@FatherName",employeeFamilyDetail.FatherName)
                    ,new SqlParameter("@MotherName",employeeFamilyDetail.MotherName)
                    ,new SqlParameter("@WifeName",employeeFamilyDetail.WifeName)
                    ,new SqlParameter("@MotherMobileNumber",employeeFamilyDetail.MotherContact)
                    ,new SqlParameter("@MotherBloodGroupID",employeeFamilyDetail.MotherBloodGroupID)
                    ,new SqlParameter("@FatherMobileNumber",employeeFamilyDetail.FatherContact)
                    ,new SqlParameter("@FatherBloodGroupID",employeeFamilyDetail.FatherBloodGroupID)
                    ,new SqlParameter("@BrotherName",employeeFamilyDetail.BrotherName)
                    ,new SqlParameter("@BrotherMobileNumber",employeeFamilyDetail.BrotherContact)
                    ,new SqlParameter("@BrotherBloodGroupID",employeeFamilyDetail.BrotherBloodGroupID)
                    ,new SqlParameter("@SisterName",employeeFamilyDetail.SisterName)
                    ,new SqlParameter("@SisterMobileNumber",employeeFamilyDetail.SisterContact)
                    ,new SqlParameter("@SisterBloodGroupID",employeeFamilyDetail.SisterBloodGroupID)
                    ,new SqlParameter("@WifeMobileNumber",employeeFamilyDetail.WifeContact)
                    ,new SqlParameter("@WifeBloodGroupID",employeeFamilyDetail.WifeBloodGroupID)
                    ,new SqlParameter("@NoofChild",employeeFamilyDetail.NoofChild)
                    ,new SqlParameter("@NoofBikeScooty",employeeFamilyDetail.NoofBikeScooty)
                    ,new SqlParameter("@NoofCar",employeeFamilyDetail.NoofCar)
                    ,new SqlParameter("@EmergencyMobileNumber",employeeFamilyDetail.EmergencyMobileNumber)
                    ,new SqlParameter("@WhatsAppNo",employeeFamilyDetail.WhatsAppNo)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeFamilyDetail", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion Family Background detail

        #region Employee banking detail
        public async Task<PagedDataTable<EmployeeBankDetails>> GetEmployeesAllBankAccount(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0)
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
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeBankDetails", param))
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
                    PagedDataTable<EmployeeBankDetails> lst = table.ToPagedDataTableList<EmployeeBankDetails>
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

        public async Task<EmployeeBankDetails> GetEmployeeBankAccount(int employeeBankDetailId, int employeeId)
        {
            EmployeeBankDetails result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeBankDetailsID", employeeBankDetailId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeBankDetails", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeBankDetails>();
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

        public async Task<int> CreateOrUpdateEmployeeBankDetail(EmployeeBankDetails employeeBankDetails)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeBankDetailsID", employeeBankDetails.EmployeeBankDetailsID)
                    ,new SqlParameter("@EmployeeID", employeeBankDetails.EmployeeID)
                    ,new SqlParameter("@BankName", employeeBankDetails.BankName)
                    ,new SqlParameter("@EmpNameAsperBank", employeeBankDetails.EmpNameAsperBank)
                    ,new SqlParameter("@IFSCCode", employeeBankDetails.IFSCCode)
                    ,new SqlParameter("@AccountNO", employeeBankDetails.AccountNO)
                    ,new SqlParameter("@BranchLocation", employeeBankDetails.BranchLocation)
                    ,new SqlParameter("@City", employeeBankDetails.City)
                    ,new SqlParameter("@BankCode", employeeBankDetails.BankCode)
                    ,new SqlParameter("@BICSwiftCode", employeeBankDetails.BICSwiftCode)
                    ,new SqlParameter("@CountryID", employeeBankDetails.CountryID)
                    ,new SqlParameter("@IsDefaultBankAccount" , employeeBankDetails.IsDefaultBankAccount)
                    ,new SqlParameter("@IsActive", employeeBankDetails.IsActive)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeBankDetails", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion Employee banking detail

        #region Employee document
        public async Task<PagedDataTable<EmployeeDocument>> GetEmployeesAllDocuments(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0)
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
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeDocument", param))
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
                    PagedDataTable<EmployeeDocument> lst = table.ToPagedDataTableList<EmployeeDocument>
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

        public async Task<EmployeeDocument> GetEmployeeDocument(int employeeDocumentId, int employeeId)
        {
            EmployeeDocument result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeDocumentID", employeeDocumentId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeDocument", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeDocument>();
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

        public async Task<int> CreateOrUpdateEmployeeDocument(EmployeeDocument employeeDocument)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeDocumentID", employeeDocument.EmployeeDocumentID)
                    ,new SqlParameter("@EmployeeID", employeeDocument.EmployeeID)
                    ,new SqlParameter("@DocumentName", employeeDocument.DocumentName)
                    ,new SqlParameter("@DocumentExtension", employeeDocument.DocumentExtension)
                    ,new SqlParameter("@DocumentTypeID", employeeDocument.DocumentTypeID)
                    //,new SqlParameter("@IsDeleted", employeeDocument.IsDeleted)
                    ,new SqlParameter("@Description", employeeDocument.Description)
                    ,new SqlParameter("@DocumentPath", employeeDocument.DocumentPath)
                    ,new SqlParameter("@IsActive", employeeDocument.IsActive)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeDocument", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> ActiveInActiveEmployeeDocument(EmployeeDocument employeeDocument)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeDocumentID", employeeDocument.EmployeeDocumentID)
                    ,new SqlParameter("@EmployeeID", employeeDocument.EmployeeID)
                    ,new SqlParameter("@CreatedOrModifiedBy", employeeDocument.CreatedOrModifiedBy)
                    ,new SqlParameter("@IsActive", employeeDocument.IsActive == true ? 1 : 0)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_U_EmployeeDocumentIsActive", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion Employee document
    }
}
