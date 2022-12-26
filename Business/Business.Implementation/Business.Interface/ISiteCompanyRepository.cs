using Business.Entities;
using Business.SQL;

using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface ISiteCompanyRepository
    {
        #region "Compnay Master"
        Task<int> CreateOrUpdateCompanyAsync(CompanyMasterMetadata compnay);
        Task<CompanyMasterMetadata> GetCompnayAsync(string companyID);
        Task<PagedDataTable<CompanyMasterMetadata>> GetAllCompanyAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "CompanyName", string sortBy = "ASC");
        Task<CompanyLogoMasterMetadata> GetCompanyLogoMaster(int companyID);
        #endregion 

        #region "Compnay contact"
        Task<PagedDataTable<CompanyContactTxnMetadata>> GetAllCompanyContactAsync(int companyID);
        Task<int> CreateOrUpdateCompanyContactAsync(CompanyContactTxnMetadata item);
        Task<CompanyContactTxnMetadata> GetCompnayContactAsync(int companyID, int companyContactPersonsID);
        #endregion

        #region "Compnay Address"
        Task<PagedDataTable<CompanyAddressTxnMetadata>> GetAllCompanyAddressAsync(int companyID);
        Task<int> CreateOrUpdateCompanyAddressAsync(CompanyAddressTxnMetadata item);
        Task<CompanyAddressTxnMetadata> GetCompnayAddressAsync(int companyID, int companyAddressTxnID);
        #endregion

        #region "Company Registration"
        Task<int> CreateOrUpdateCompanyRegistrationAsync(CompanyMasterMetadata item);
        #endregion

        #region "Company Banking"
        Task<CompanyBankingMetadata> GetCompanyBankingAsync(int companyID, int companyBankingID);
        Task<int> CreateOrUpdateCompanyBankingAsync(CompanyBankingMetadata item);
        Task<PagedDataTable<CompanyBankingMetadata>> GetAllCompanyBankingAsync(int companyID);
        #endregion

        #region "Company Document"
        Task<CompanyDocumentMetadata> GetDocumentAsync(int companyID, int companyDocumentsID);
        Task<int> CreateOrUpdateCompanyDocumentAsync(CompanyDocumentMetadata item);
        Task<PagedDataTable<CompanyDocumentMetadata>> GetAllCompanyDocumentAsync(int companyID);
        #endregion
    }
}
