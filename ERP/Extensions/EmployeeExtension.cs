using Business.Entities.Employee;
using Business.Interface;
using Business.Interface.IEmployee;
using Business.Service.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Extensions
{
    public class EmployeeExtension
    {
        private static HttpContext Current => new HttpContextAccessor().HttpContext;
        public static IEmployeeService _employeeService => (IEmployeeService)Current.RequestServices.GetService(typeof(IEmployeeService));
        public static IMasterService _masterService => (IMasterService)Current.RequestServices.GetService(typeof(IMasterService));
        #region "AddressList"
        public static List<EmployeeAddressTxn> ListOfEmployeeAddress(int employeeId)
        {
            try
            {
                List<EmployeeAddressTxn> pds = _employeeService.GetEmployeesAllAddressAsync(1, 10, "", "EmployeeID", "1", employeeId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }

        #endregion "AddressList"

        #region Employee Family Detail
        public static EmployeeFamilyDetail GetEmployeeFamilyDetail(int employeeId)
        {
            try
            {
                EmployeeFamilyDetail employeeFamily = new EmployeeFamilyDetail();
                employeeFamily.EmployeeID = employeeId;

                var employeeFamilyDetail = _employeeService.GetEmployeeFamily(employeeId).Result;

                if (employeeFamilyDetail != null)
                    employeeFamily = employeeFamilyDetail;

                return employeeFamily;
            }
            catch
            {
                return null;
            }
        }
        #endregion Employee Family Detail

        #region Employee banking detail
        public static List<EmployeeBankDetails> GetEmployeesAllBankAccount(int employeeId)
        {
            try
            {
                List<EmployeeBankDetails> pds = _employeeService.GetEmployeesAllBankAccount(1, 10, "", "EmployeeID", "1", employeeId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }
        #endregion Employee banking detail

        #region Employee document
        public static SelectList GetAllDocumentType()
        {
            try
            {
                var role = _masterService.GetAllDocumentTypeAsync().Result;
                return new SelectList(role, "DocumentTypeID", "DocumentTypeName");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public static List<EmployeeDocument> GetEmployeesAllDocuments(int employeeId)
        {
            try
            {
                List<EmployeeDocument> pds = _employeeService.GetEmployeesAllDocuments(1, 10, "", "EmployeeID", "1", employeeId).Result;
                return pds;
            }
            catch
            {
                return null;
            }
        }
        #endregion Employee document

        public static SelectList GetMaritalStatusMaster()
        {
            try
            {
                var role = _masterService.GetMaritalStatusMaster();
                return new SelectList(role, "MaritalStatusID", "MaritalStatusText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        public static SelectList GetAllBloodGroupMaster()
        {
            try
            {
                var bloodGroupMaster = _masterService.GetAllBloodGroupMaster().Result;
                return new SelectList(bloodGroupMaster, "BloodGroupID", "BloodGroupText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
    }
}
