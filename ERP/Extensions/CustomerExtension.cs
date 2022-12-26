using Business.Interface;
using Microsoft.AspNetCore.Http;
using Business.Interface.ICustomer;
using System.Collections.Generic;
using System.Linq;
using Business.Entities.Customer;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;

namespace ERP.Extensions
{
    public class CustomerExtension
    {
        private static HttpContext Current => new HttpContextAccessor().HttpContext;
        public static IMasterService _masterService => (IMasterService)Current.RequestServices.GetService(typeof(IMasterService));
        public static ICustomerService _customerService => (ICustomerService)Current.RequestServices.GetService(typeof(ICustomerService));

        public static SelectList GetAllIndustryTypeMaster()
        {
            try
            {
                var listIndustryType = _masterService.GetAllIndustryTypeMaster();
                return new SelectList(listIndustryType, "IndustryTypeID", "IndustryTypeText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList GetAllBusinessTypeMaster()
        {
            try
            {
                var listBusinessType = _masterService.GetAllBusinessTypeMaster();
                return new SelectList(listBusinessType, "BusinessTypeID", "BusinessTypeText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList GetAllDepartments()
        {
            try
            {
                var listDepartment = _masterService.GetAllDepartments();
                return new SelectList(listDepartment, "DepartmentID", "DepartmentName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static SelectList GetAllDesignations()
        {
            try
            {
                var listDesignation = _masterService.GetAllDesignations();
                return new SelectList(listDesignation, "DesignationID", "DesignationText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        public static List<CustomerContactTxn> ListOfCustomerContactPerson(int customerId)
        {
            try
            {
                List<CustomerContactTxn> pds = _customerService.GetCustomerAllContactPerson(1, 10, "", "CustomerID", "1", customerId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }

        #region "AddressList"
        //public static List<EmployeeAddressTxn> ListOfEmployeeAddress(int employeeId)
        //{
        //    try
        //    {
        //        List<EmployeeAddressTxn> pds = _employeeService.GetEmployeesAllAddressAsync(1, 10, "", "EmployeeID", "1", employeeId).Result;
        //        return pds;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        ////}

        #endregion "AddressList"
    }
}
