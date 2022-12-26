using Business.Entities;
using Business.Interface;
using Business.Interface.Dynamic;
using Business.SQL;
using ERP.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
namespace ERP.Extensions
{
    public static class CompanyDropdownBinder
    {
        private static HttpContext Current => new HttpContextAccessor().HttpContext;
        private static ISiteRoleRepository roleService => (ISiteRoleRepository)Current.RequestServices.GetService(typeof(ISiteRoleRepository));
        private static ISuperAdminService superAdmin => (ISuperAdminService)Current.RequestServices.GetService(typeof(ISuperAdminService));
        private static ISiteCompanyRepository compnayService => (ISiteCompanyRepository)Current.RequestServices.GetService(typeof(ISiteCompanyRepository));
        private static IEntity _entity => (IEntity)Current.RequestServices.GetService(typeof(IEntity));
        private static IRequestType _requestType => (IRequestType)Current.RequestServices.GetService(typeof(IRequestType));
        private static IMasterEntity masterEntity => (IMasterEntity)Current.RequestServices.GetService(typeof(IMasterEntity));

        #region "Contact list"
        public static PagedDataTable<CompanyContactTxnMetadata> ListOfCompnayContact(int companyID)
        {
            try
            {
                PagedDataTable<CompanyContactTxnMetadata> pds= compnayService.GetAllCompanyContactAsync(companyID).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region "Address list"
        public static PagedDataTable<CompanyAddressTxnMetadata> ListOfCompnayAddress(int companyID)
        {
            try
            {
                PagedDataTable<CompanyAddressTxnMetadata> pds = compnayService.GetAllCompanyAddressAsync(companyID).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region "Bank list"
        public static PagedDataTable<CompanyBankingMetadata> ListOfCompnayBank(int companyID)
        {
            try
            {
                PagedDataTable<CompanyBankingMetadata> pds = compnayService.GetAllCompanyBankingAsync(companyID).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region "Document list"
        public static PagedDataTable<CompanyDocumentMetadata> ListOfCompnayDocuments(int companyID)
        {
            try
            {
                PagedDataTable<CompanyDocumentMetadata> pds = compnayService.GetAllCompanyDocumentAsync(companyID).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        public static SelectList EntityType(int CompanyID)
        {
            try
            {
                var pds = _entity.GetList(CompanyID).Where(a => a.IsActive).ToList();
                return new SelectList(pds, "MasterListID", "Value");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList RequestSection(int CompanyID)
        {
            try
            {
                var pds = _requestType.GetList(CompanyID).Where(p => p.IsActive).ToList();
                return new SelectList(pds, "RequestTypeID", "Name");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList MasterDataKey(int CompanyID)
        {
            try
            {
                var pds = masterEntity.GetDropdownKeys(CompanyID);
                return new SelectList(pds, "Value", "Text");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
       
    }
}
