using Business.Entities.Customer;
using Business.Entities.Employee;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.ICustomer
{
    public interface ICustomerService
    {
        Task<int> AddUpdateCustomer(CustomerMaster customerMaster);
        Task<int> AddUpdateCustomerContactPerson(CustomerContactTxn customerContactTxn);
        Task<PagedDataTable<CustomerMaster>> GetAllCustomerAsync(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "CustomerID", string sortBy = "ASC");
        Task<PagedDataTable<CustomerContactTxn>> GetCustomerAllContactPerson(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0);
        Task<CustomerMaster> GetCustomerAsync(int customerId);
        Task<CustomerContactTxn> GetCustomerContactPerson(int customerContactID, int customerId);
        Task<int> UpdateCustomerLogoImage(CustomerLogoImage customerLogoImage);
    }
}
