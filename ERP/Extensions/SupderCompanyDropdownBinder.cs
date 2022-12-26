using Business.Entities;
using Business.Interface;
using Business.Interface.Dynamic;
using Business.Service.Dynamic;
using Business.SQL;
using ERP.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
namespace ERP.Extensions
{
    public static class SuperCompanyDropdownBinder
    {
        private static HttpContext Current => new HttpContextAccessor().HttpContext;
        private static ISiteRoleRepository roleService => (ISiteRoleRepository)Current.RequestServices.GetService(typeof(ISiteRoleRepository));
        private static ISuperAdminService superAdmin => (ISuperAdminService)Current.RequestServices.GetService(typeof(ISuperAdminService));
        private static ISiteCompanyRepository compnayService => (ISiteCompanyRepository)Current.RequestServices.GetService(typeof(ISiteCompanyRepository));

        private static IMasterEntity masterEntity => (IMasterEntity)Current.RequestServices.GetService(typeof(IMasterEntity));
        private static IRequestTypeControl requestTypeControl => (IRequestTypeControl)Current.RequestServices.GetService(typeof(IRequestTypeControl));
        public static SelectList Prefix()
        {
            try
            {
                //Mr.,” “Mrs.,” “Ms.,” and “Miss.
                var _lst = new PagedDataTable<GeneralMetadata>();
                _lst.Add(new GeneralMetadata() {Name="Mr." });
                _lst.Add(new GeneralMetadata() { Name = "Mrs." });
                _lst.Add(new GeneralMetadata() { Name = "Ms." });
                _lst.Add(new GeneralMetadata() { Name = "Miss." });
                return new SelectList(_lst, "Name", "Name");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList Companies()
        {
            try
            {
                var role = superAdmin.GetAllCountryAsync().Result;
                return new SelectList(role, "CompanyID", "CompanyName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList Roles()
        {
            var role = roleService.GetAllRolesAsync(SettingsController.COMPANYID).Result;
            return new SelectList(role, "RoleID", "Name");
        }
        public static SelectList AddressType()
        {
            try
            {
                var role = superAdmin.GetAllAddressTypeAsync().Result;
                return new SelectList(role, "AddressTypeID", "AddressTypeText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList BusinessType()
        {
            try
            {
                var role = superAdmin.GetAllBusinessTypeAsync().Result;
                return new SelectList(role, "BusinessTypeID", "BusinessTypeText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList IndustryType()
        {
            try
            {
                var role = superAdmin.GetAllIndustryTypeAsync().Result;
                return new SelectList(role, "IndustryTypeID", "IndustryTypeText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        public static SelectList Departments()
        {
            try
            {
                var role = superAdmin.GetAllDepartmentAsync().Result;
                return new SelectList(role, "DepartmentID", "DepartmentName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList Designations()
        {
            try
            {
                var role = superAdmin.GetAllDesignationAsync().Result;
                return new SelectList(role, "DesignationID", "DesignationText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList EmailGroups()
        {
            try
            {
                var role = superAdmin.GetAllEmailGroupAsync().Result;
                return new SelectList(role, "EmailGroupID", "EmailGroupName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        public static SelectList Countries()
        {
            try
            {
                var role = superAdmin.GetAllCountryAsync().Result;
                return new SelectList(role, "CountryID", "CountryName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList State(int countryID)
        {
            try
            {
                var role = superAdmin.GetAllStateAsync(1,0,"","StateName","ASC",countryID).Result;
                return new SelectList(role, "StateID", "StateName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList District(int stateID)
        {
            try
            {
                var role = superAdmin.GetAllDistrictAsync(1, 0, "", "DistrictName", "ASC", stateID).Result;
                return new SelectList(role, "DistrictID", "DistrictName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList Taluka(int districtID)
        {
            try
            {
                var role = superAdmin.GetAllTalukaAsync(1, 0, "", "TalukaName", "ASC", districtID).Result;
                return new SelectList(role, "TalukaID", "TalukaName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList City(int stateID)
        {
            try
            {
                var role = superAdmin.GetAllCityAsync(1, 0, "", "CityName", "ASC", stateID).Result;
                return new SelectList(role, "CityID", "CityName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        #region "Entity Type"
        public static SelectList EntryType()
        {
            try
            {
                var role = masterEntity.GetMasterEntryTypeList();
                return new SelectList(role, "MasterListEntryTypeID", "Name");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList ControlDataType()
        {
            try
            {
                var role = requestTypeControl.GetControlTypes();
                return new SelectList(role, "Value", "Text");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList ValidationRules()
        {
            try
            {
                var role = requestTypeControl.GetValidationRules();
                return new SelectList(role, "ControlValidationRuleID", "Name");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        #endregion

        public static SelectList DocumentType()
        {
            try
            {
                var role = superAdmin.GetAllDocumentTypeAsync().Result;
                return new SelectList(role, "DocumentTypeID", "DocumentTypeName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
    }
}
