using Business.Entities.Employee;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.IEmployee
{
    public interface IEmployeeService
    {
        Task<PagedDataTable<EmployeeMaster>> GetAllEmployeesAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "EmployeeID", string sortBy = "ASC");
        #region Basic detail
        Task<AddUpdateEmployee> GetEmployeeAsync(int employeeId);
        Task<int> AddUpdateEmployee(AddUpdateEmployee addUpdateEmployee);
        Task<int> UpdateEmployeeProfilePhoto(EmployeeProfileImage employeeProfileImage);
        #endregion Basic detail

        #region Address detail

        Task<PagedDataTable<EmployeeAddressTxn>> GetEmployeesAllAddressAsync(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "EmployeeID", string sortBy = "ASC", int employeeId = 0);
        Task<int> CreateOrUpdateEmployeeAddressAsync(EmployeeAddressTxn addressMaster);
        Task<EmployeeAddressTxn> GetEmployeeAddressTxn(int employeeAddressTxnId, int employeeId);

        #endregion Address detail

        #region Family Background detail
        Task<int> CreateOrUpdateEmployeeFamilyBackgroundAsync(EmployeeFamilyDetail employeeFamilyDetail);
        Task<EmployeeFamilyDetail> GetEmployeeFamily(int employeeId);

        #endregion Family Background detail

        #region Employee banking detail
        Task<PagedDataTable<EmployeeBankDetails>> GetEmployeesAllBankAccount(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0);
        Task<int> CreateOrUpdateEmployeeBankDetail(EmployeeBankDetails employeeBankDetails);
        Task<EmployeeBankDetails> GetEmployeeBankAccount(int employeeBankDetailId, int employeeId);
        #endregion Employee banking detail

        #region Employee document
        Task<PagedDataTable<EmployeeDocument>> GetEmployeesAllDocuments(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0);
        Task<EmployeeDocument> GetEmployeeDocument(int employeeDocumentId, int employeeId);
        Task<int> CreateOrUpdateEmployeeDocument(EmployeeDocument employeeDocument);
        Task<int> ActiveInActiveEmployeeDocument(EmployeeDocument employeeDocument);
        #endregion Employee document
    }
}