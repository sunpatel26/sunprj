using Business.Entities;
using Business.Interface;
using Business.SQL;
using MailKit.Search;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Tsp;
using System;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;

namespace Business.Service
{
    public class CompanyService : ISiteCompanyRepository
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public CompanyService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        #region "Company Master"
        public async Task<PagedDataTable<CompanyMasterMetadata>> GetAllCompanyAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "CompanyName", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<CompanyMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CompanyMaster", param))
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
                    lst = table.ToPagedDataTableList<CompanyMasterMetadata>(pageNo, pageSize, totalItemCount);
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

        public async Task<int> CreateOrUpdateCompanyAsync(CompanyMasterMetadata compnay)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@CompanyID", compnay.CompanyID)
                ,new SqlParameter("@CompanyCode", compnay.CompanyCode)
                ,new SqlParameter("@CompanyName", compnay.CompanyName)
                ,new SqlParameter("@IsActive", compnay.IsActive)
                ,new SqlParameter("@CompanyWebsiteText", compnay.CompanyWebsiteText)
                ,new SqlParameter("@CompanyLogoName", compnay.CompanyLogoName)
                ,new SqlParameter("@CompanyLogoImagePath", compnay.CompanyLogoImagePath)
                ,new SqlParameter("@CompanyGroupName", compnay.CompanyGroupName)
                ,new SqlParameter("@UnitName", compnay.UnitName)
                ,new SqlParameter("@IndustryTypeID", compnay.IndustryTypeID)
                ,new SqlParameter("@BusinessTypeID", compnay.BusinessTypeID)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }

        public async Task<CompanyMasterMetadata> GetCompnayAsync(string companyID)
        {
            CompanyMasterMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CompanyID", companyID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CompanyMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CompanyMasterMetadata>();
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
        public async Task<CompanyLogoMasterMetadata> GetCompanyLogoMaster(int companyID)
        {
            DataTable table = new DataTable();
            CompanyLogoMasterMetadata item = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CompanyID", companyID) };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "usp_Get_CompanyLogoMaster", param))
                {
                    if (ds.Tables.Count > 0)
                    {
                        table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            DataRow dr = table.Rows[0];
                            item = dr.ToPagedDataTableList<CompanyLogoMasterMetadata>();
                        }
                    }
                }
                return item;
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

        #region "Compnay contact"
        public async Task<PagedDataTable<CompanyContactTxnMetadata>> GetAllCompanyContactAsync(int companyID)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<CompanyContactTxnMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@CompanyID",companyID)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CompanyContactPersons", param))
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
                    lst = table.ToPagedDataTableList<CompanyContactTxnMetadata>();
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

        public async Task<int> CreateOrUpdateCompanyContactAsync(CompanyContactTxnMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@CompanyContactPersonsID", item.CompanyContactPersonsID)
                ,new SqlParameter("@CompanyID", item.CompanyID)
                        ,new SqlParameter("@DepartmentID", item.DepartmentID)
                        ,new SqlParameter("@DesignationID", item.DesignationID)
                        ,new SqlParameter("@Prefix", item.Prefix)
                        ,new SqlParameter("@PersonName", item.PersonName)
                        ,new SqlParameter("@PersonalMobileNo", item.PersonalMobileNo)
                        ,new SqlParameter("@OfficeMobileNo", item.OfficeMobileNo)
                        ,new SqlParameter("@AlternetMobileNo", item.AlternetMobileNo)
                        ,new SqlParameter("@EmailGroupID", item.EmailGroupID)
                        ,new SqlParameter("@PersonEmail", item.PersonEmail)
                        ,new SqlParameter("@OfficeEmail", item.OfficeEmail)
                        ,new SqlParameter("@Birthdate", item.Birthdate)
                        ,new SqlParameter("@Religion", item.Religion)
                        ,new SqlParameter("@IsActive", item.IsActive)
                        ,new SqlParameter("@IsResigned", item.IsResigned)
                        ,new SqlParameter("@Note", item.Note)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyContactPersons", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }

        public async Task<CompanyContactTxnMetadata> GetCompnayContactAsync(int companyID,int companyContactPersonsID)
        {
            CompanyContactTxnMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@companyContactPersonsID", companyContactPersonsID)
                ,new SqlParameter("@CompanyID", companyID)};
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CompanyContactPersons", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CompanyContactTxnMetadata>();
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
        #endregion

        #region "Compnay Address"
        public async Task<PagedDataTable<CompanyAddressTxnMetadata>> GetAllCompanyAddressAsync(int companyID)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<CompanyAddressTxnMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@CompanyID",companyID)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CompanyAddressTxn", param))
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
                    lst = table.ToPagedDataTableList<CompanyAddressTxnMetadata>();
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

        public async Task<int> CreateOrUpdateCompanyAddressAsync(CompanyAddressTxnMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CompanyAddressTxnID", item.CompanyAddressTxnID)
                    ,new SqlParameter("@CompanyID", item.CompanyID)
                    ,new SqlParameter("@Address1", item.Address1)
                    ,new SqlParameter("@Address2", item.Address2)
                    ,new SqlParameter("@Address3", item.Address3)
                    ,new SqlParameter("@Area", item.Area)
                    ,new SqlParameter("@ZIPCodeID", item.ZIPCodeID)
                    ,new SqlParameter("@IsActive", item.IsActive)
                    ,new SqlParameter("@CityID", item.CityID)
                    ,new SqlParameter("@DistrictID", item.DistrictID)
                    ,new SqlParameter("@TalukaID", item.TalukaID)
                    ,new SqlParameter("@AddressTypeID", item.AddressTypeID)
                    ,new SqlParameter("@CreatedOrModifiedBy",item.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyAddressTxn", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CompanyAddressTxnMetadata> GetCompnayAddressAsync(int companyID, int companyAddressTxnID)
        {
            CompanyAddressTxnMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CompanyAddressTxnID", companyAddressTxnID)
                ,new SqlParameter("@CompanyID", companyID)};
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CompanyAddressTxn", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CompanyAddressTxnMetadata>();
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
        #endregion

        #region "Company Registration"
        public async Task<int> CreateOrUpdateCompanyRegistrationAsync(CompanyMasterMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@CompanyRegistrationID", item.CompanyRegistrationID)
                ,new SqlParameter("@CompanyID", item.CompanyID)           
                ,new SqlParameter("@PANNo", item.PANNo)               
                ,new SqlParameter("@GSTINNo", item.GSTINNo)              
                ,new SqlParameter("@GSTINType", item.GSTINType)            
                ,new SqlParameter("@FactoryLicenseNo", item.FactoryLicenseNo)     
                ,new SqlParameter("@FactoryRegNo", item.FactoryRegNo)         
                ,new SqlParameter("@ARNNo", item.ARNNo)                
                ,new SqlParameter("@ECCNo", item.ECCNo)                
                ,new SqlParameter("@MSMENo", item.MSMENo)               
                ,new SqlParameter("@SSINo", item.SSINo)                
                ,new SqlParameter("@TANNo", item.TANNo)                
                ,new SqlParameter("@ExportNo", item.ExportNo)             
                ,new SqlParameter("@ImportNo", item.ImportNo)             
                ,new SqlParameter("@TaxRange", item.TaxRange)             
                ,new SqlParameter("@TaxDivisio", item.TaxDivisio)           
                ,new SqlParameter("@TaxCommisionerate", item.TaxCommisionerate) 
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyRegistration", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region "Company Banking"
        public async Task<CompanyBankingMetadata> GetCompanyBankingAsync(int companyID, int companyBankingID)
        {
            CompanyBankingMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CompanyBankingID", companyBankingID)
                ,new SqlParameter("@CompanyID", companyID)};
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CompanyBanking", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CompanyBankingMetadata>();
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
        public async Task<int> CreateOrUpdateCompanyBankingAsync(CompanyBankingMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@CompanyBankingID", item.CompanyBankingID)
                ,new SqlParameter("@CompanyID", item.CompanyID)
                ,new SqlParameter("@BankName", item.BankName)
                ,new SqlParameter("@BankCode", item.BankCode)
                ,new SqlParameter("@AccountNo", item.AccountNo)
                ,new SqlParameter("@BIC_SWIFTCode", item.BIC_SWIFTCode)
                ,new SqlParameter("@AccountName", item.AccountName)
                ,new SqlParameter("@IFCICode", item.IFCICode)
                ,new SqlParameter("@Branch", item.Branch)
                ,new SqlParameter("@CityID", item.CityID)
                ,new SqlParameter("@CountryID", item.CountryID)
                ,new SqlParameter("@IsActive", item.IsActive)
                ,new SqlParameter("@CreatedOrModifyBy", item.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyBanking", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }
        public async Task<PagedDataTable<CompanyBankingMetadata>> GetAllCompanyBankingAsync(int companyID)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<CompanyBankingMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@CompanyID",companyID)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CompanyBanking", param))
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
                    lst = table.ToPagedDataTableList<CompanyBankingMetadata>();
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

        #region "Company Document"
        public async Task<CompanyDocumentMetadata> GetDocumentAsync(int companyID, int companyDocumentsID)
        {
            CompanyDocumentMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CompanyDocumentsID", companyDocumentsID)
                ,new SqlParameter("@CompanyID", companyID)};
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CompanyDocuments", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CompanyDocumentMetadata>();
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
        public async Task<int> CreateOrUpdateCompanyDocumentAsync(CompanyDocumentMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@CompanyDocumentsID", item.CompanyDocumentsID)
                ,new SqlParameter("@CompanyID", item.CompanyID)
                ,new SqlParameter("@DocumentTypeID", item.DocumentTypeID)
                ,new SqlParameter("@DocumentName", item.DocumentName)
                ,new SqlParameter("@DocumentDesc", item.DocumentDesc)
                ,new SqlParameter("@IsActive", item.IsActive)
                ,new SqlParameter("@DocumentPath", item.DocumentPath)
               
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyDocuments", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }
        public async Task<PagedDataTable<CompanyDocumentMetadata>> GetAllCompanyDocumentAsync(int companyID)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<CompanyDocumentMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@CompanyID",companyID)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CompanyDocuments", param))
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
                    lst = table.ToPagedDataTableList<CompanyDocumentMetadata>();
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
